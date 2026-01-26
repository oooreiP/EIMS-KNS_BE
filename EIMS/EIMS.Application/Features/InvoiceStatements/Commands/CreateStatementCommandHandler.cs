using AutoMapper;
using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatement;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EIMS.Application.Features.InvoiceStatements.Commands
{
    public class CreateStatementCommandHandler : IRequestHandler<CreateStatementCommand, Result<StatementDetailResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        private readonly IServiceScopeFactory _scopeFactory;
        public CreateStatementCommandHandler(IUnitOfWork uow, IMapper mapper, IServiceScopeFactory scopeFactory)
        {
            _uow = uow;
            _mapper = mapper;
            _scopeFactory = scopeFactory;
        }

        public async Task<Result<StatementDetailResponse>> Handle(CreateStatementCommand request, CancellationToken cancellationToken)
        {
            //find the date boundary
            var startOfMonth = new DateTime(request.Year, request.Month, 1, 0, 0, 0, DateTimeKind.Utc);
            var endOfMonth = startOfMonth.AddMonths(1);
            var allowedStatuses = new List<int> {
                (int)EInvoiceStatus.Issued,
                (int)EInvoiceStatus.Adjusted,
            };
            var existedStatement = await _uow.InvoiceStatementRepository.IsDuplicated(request.CustomerID, request.Month, request.Year);
            if (existedStatement)
                return Result.Fail(
                    new Error($"The statement for cusotmer {request.CustomerID} for {request.Month}/{request.Year} is existed."));
            var rawInvoices = await _uow.InvoicesRepository
                .GetAllQueryable()
                .Include(i => i.Payments)
                .Include(i => i.Customer)
                .Where(i => i.CustomerID == request.CustomerID)
                .Where(i => i.IssuedDate != null)
                .Where(i => i.IssuedDate < endOfMonth)
                .Where(i => allowedStatuses.Contains(i.InvoiceStatusID))
                .ToListAsync(cancellationToken);

            // debt from before the request period
            decimal openingBalance = rawInvoices
            .Where(inv => inv.IssuedDate < startOfMonth)
            .Sum(inv =>
                inv.TotalAmount - inv.Payments
                    .Where(p => p.PaymentDate < startOfMonth)
                    .Sum(p => p.AmountPaid)
            );

            // debt this month
            decimal newCharges = rawInvoices
        .Where(inv => inv.IssuedDate >= startOfMonth && inv.IssuedDate < endOfMonth)
        .Sum(inv => inv.TotalAmount);
            // money payed this month
            decimal paymentsInPeriod = rawInvoices
        .SelectMany(i => i.Payments)
        .Where(p => p.PaymentDate >= startOfMonth && p.PaymentDate < endOfMonth)
        .Sum(p => p.AmountPaid);
            // summary the total money you must pay this period + before
            decimal closingBalance = openingBalance + newCharges - paymentsInPeriod;

            // list of invoices which is partially paid and issued this month
            var debtItems = rawInvoices
        .Select(inv =>
        {
            var paidTotal = inv.Payments.Where(p => p.PaymentDate < endOfMonth).Sum(p => p.AmountPaid);
            var remaining = inv.TotalAmount - paidTotal;
            return new { Invoice = inv, Remaining = remaining };
        })
        .Where(x => x.Remaining > 0 || (x.Invoice.IssuedDate >= startOfMonth))
        .ToList();

            if (closingBalance <= 0 && !debtItems.Any())
                return Result.Fail(new Error($"Customer {request.CustomerID} has no outstanding debt for this period."));
            string statementCode = $"ST-{request.CustomerID}-{request.Month:D2}{request.Year}";
            decimal totalDue = openingBalance + newCharges;

            // var existingStatement = await _uow.InvoiceStatementRepository
            //     .GetAllQueryable()
            //     .Include(s => s.Customer)
            //     .Include(s => s.StatementDetails)
            //     .Include(s => s.StatementStatus)
            //     .FirstOrDefaultAsync(s => s.StatementCode == statementCode, cancellationToken);

            InvoiceStatement statementToReturn;

            // if (existingStatement != null)
            // {
            //     existingStatement.TotalInvoices = debtItems.Count;
            //     existingStatement.PeriodMonth = request.Month;
            //     existingStatement.PeriodYear = request.Year;
            //     existingStatement.OpeningBalance = openingBalance;
            //     existingStatement.NewCharges = newCharges;
            //     existingStatement.TotalAmount = closingBalance;
            //     existingStatement.PaidAmount = paymentsInPeriod;
            //     existingStatement.StatusID = newStatusId;
            //     existingStatement.StatementDate = DateTime.UtcNow; // Update timestamp
            //     existingStatement.StatementDetails.Clear();
            //     foreach (var item in debtItems)
            //     {
            //         existingStatement.StatementDetails.Add(new InvoiceStatementDetail
            //         {
            //             InvoiceID = item.Invoice.InvoiceID,
            //             OutstandingAmount = item.Remaining,
            //             Invoice = item.Invoice // Important for Mapper
            //         });
            //     }

            //     // Attach customer for response mapping
            //     existingStatement.Customer = rawInvoices.FirstOrDefault()?.Customer;

            //     statementToReturn = existingStatement;
            // }
            // else
            // {
            // === CREATE LOGIC ===
            var statement = new InvoiceStatement
            {
                CustomerID = request.CustomerID,
                StatementCode = statementCode,
                PeriodMonth = request.Month,    // <--- Mới
                PeriodYear = request.Year,      // <--- Mới
                OpeningBalance = openingBalance,// <--- Mới
                NewCharges = newCharges,
                StatementDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14),
                CreatedBy = request.AuthenticatedUserId,
                TotalInvoices = debtItems.Count,
                Notes = $"Statement for {request.Month}/{request.Year}",
                TotalAmount = closingBalance,
                PaidAmount = paymentsInPeriod,
                StatusID = 1, // Draft
                Customer = rawInvoices.FirstOrDefault()?.Customer // For Mapper
            };

            foreach (var item in debtItems)
            {
                statement.StatementDetails.Add(new InvoiceStatementDetail
                {
                    InvoiceID = item.Invoice.InvoiceID,
                    OutstandingAmount = item.Remaining,
                    Invoice = item.Invoice // Important for Mapper
                });
            }
            await _uow.InvoiceStatementRepository.CreateAsync(statement);
            statementToReturn = statement;
            // 3. SAVE AND RETURN
            await using var transaction = await _uow.BeginTransactionAsync();
            try
            {
                // SaveChanges will execute the INSERT or UPDATE
                await _uow.SaveChanges();
                await _uow.CommitAsync();
                Task.Run(() =>
            ProcessStatementPdfInBackground(
                statement.StatementID,
                request.RootPath
            )
        );
                var response = _mapper.Map<StatementDetailResponse>(statementToReturn);
                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                return Result.Fail($"Failed to generate statement: {ex.Message}");
            }
        }
        private async Task ProcessStatementPdfInBackground(
        int statementId,
        string rootPath)
        {
            using var scope = _scopeFactory.CreateScope();

            var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            var pdfService = scope.ServiceProvider.GetRequiredService<IPdfService>();
            var xmlService = scope.ServiceProvider.GetRequiredService<IInvoiceXMLService>();
            var statementService = scope.ServiceProvider.GetRequiredService<IStatementService>();
            var fileStorage = scope.ServiceProvider.GetRequiredService<IFileStorageService>();
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<CreateStatementCommandHandler>>();

            try
            {
                var statement = await uow.InvoiceStatementRepository
                    .GetAllQueryable()
                    .Include(s => s.Customer)
                    .Include(s => s.StatementDetails)
                        .ThenInclude(d => d.Invoice)
                            .ThenInclude(i => i.InvoiceItems)
                                .ThenInclude(it => it.Product)
                    .FirstOrDefaultAsync(s => s.StatementID == statementId);

                if (statement == null)
                {
                    logger.LogError($"Statement {statementId} not found.");
                    return;
                }
                var certResult = await xmlService.GetCertificateAsync(1);
                if (certResult.IsFailed)
                {
                    logger.LogError($"Certificate not found for Statement {statementId}");
                    return;
                }

                var cert = certResult.Value;
                var paymentDto = await statementService.GetPaymentRequestXmlAsync(statement);

                string unsignedXml = XmlHelpers.Serialize(paymentDto);
                var signedResult = XmlHelpers.SignElectronicDocument(unsignedXml, cert, true);

                string signedXml = signedResult.SignedXml;
                string xslPath = Path.Combine(rootPath, "Templates", "PaymentTemplate.xsl");
                string htmlContent = pdfService.TransformXmlToHtml(signedXml, xslPath);
                byte[] pdfBytes = await pdfService.ConvertHtmlToPdfAsync(htmlContent);

                string fileName = $"Statement_{statement.StatementCode}_{Guid.NewGuid()}.pdf";

                using var pdfStream = new MemoryStream(pdfBytes);
                var uploadResult = await fileStorage.UploadFileAsync(
                    pdfStream,
                    fileName,
                    "statement"
                );

                if (uploadResult.IsFailed)
                {
                    logger.LogError($"Upload failed for Statement {statementId}");
                    return;
                }

                statement.FilePath = uploadResult.Value.Url;

                await uow.InvoiceStatementRepository.UpdateAsync(statement);
                await uow.SaveChanges();

                logger.LogInformation($"Statement {statementId} PDF generated successfully.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"Error generating PDF for Statement {statementId}");
            }
        }
    }
}
