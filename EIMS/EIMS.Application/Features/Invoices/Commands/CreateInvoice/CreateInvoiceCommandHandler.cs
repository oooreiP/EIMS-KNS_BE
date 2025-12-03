using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<CreateInvoiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<CreateInvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                return Result.Fail(new Error("Invoice must has at least one item").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            if (request.TemplateID == null || request.TemplateID == 0)
                return Result.Fail(new Error("Invoice must has a valid template id").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            string? xmlPath = null;
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1️ Xử lý Customer
                Customer? customer = null;
                if (request.CustomerID == null || request.CustomerID == 0)
                {
                    customer = new Customer
                    {
                        CustomerName = request.CustomerName ?? "Khách hàng chưa đặt tên",
                        TaxCode = request.TaxCode ?? "",
                        Address = request.Address ?? "Chưa cập nhật",
                        ContactEmail = request.ContactEmail ?? "noemail@system.local",
                        ContactPerson = request.ContactPerson,
                        ContactPhone = request.ContactPhone ?? ""
                    };
                    customer = await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer);
                }

                var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(request.TemplateID.Value);
                if (template == null)
                    return Result.Fail(new Error($"Template {request.TemplateID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
                var user = _unitOfWork.UserRepository.GetByIdAsync(request.SalesID);
                if (user == null)
                    return Result.Fail(new Error($"User {request.SalesID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
                var serial = await _unitOfWork.SerialRepository.GetByIdAndLockAsync(template.SerialID);
                if (serial == null)
                    return Result.Fail(new Error($"Template {serial.SerialID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
                serial.CurrentInvoiceNumber += 1;
                long newInvoiceNumber = serial.CurrentInvoiceNumber;
                await _unitOfWork.SerialRepository.UpdateAsync(serial);

                decimal subtotal = request.Items.Sum(i => i.Amount);
                decimal vatAmount = request.Items.Sum(i => i.VATAmount);

                if (request.Amount <= 0) request.Amount = subtotal;
                if (request.TaxAmount <= 0) request.TaxAmount = vatAmount;
                if (request.TotalAmount <= 0) request.TotalAmount = request.Amount + request.TaxAmount;

                decimal vatRate = (request.Amount > 0 && request.TaxAmount > 0)
                    ? Math.Round((request.TaxAmount / request.Amount) * 100, 2)
                    : 0;

                var invoice = new Invoice
                {
                    InvoiceNumber = newInvoiceNumber,
                    TemplateID = request.TemplateID.Value,
                    CustomerID = customer?.CustomerID ?? request.CustomerID!.Value,
                    CreatedAt = DateTime.UtcNow,
                    SalesID = request.SalesID,
                    CompanyId = request.CompanyID,
                    SubtotalAmount = request.Amount,
                    VATAmount = request.TaxAmount,
                    VATRate = vatRate,
                    TotalAmount = request.TotalAmount,
                    TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(request.TotalAmount),
                    InvoiceStatusID = 1,
                    PaymentStatusID = 1,
                    IssuerID = request.SignedBy ?? 1,
                    MinRows = request.MinRows ?? 5,
                    InvoiceItems = request.Items.Select(i => new InvoiceItem
                    {
                        ProductID = i.ProductId,
                        Quantity = i.Quantity,
                        Amount = i.Amount,
                        VATAmount = i.VATAmount
                    }).ToList()
                };
                await _unitOfWork.InvoicesRepository.CreateInvoiceAsync(invoice);
                await _unitOfWork.SaveChanges();

                var history = new InvoiceHistory
                {
                    InvoiceID = invoice.InvoiceID,
                    ActionType = "Created",
                    ReferenceInvoiceID = null,
                    Date = DateTime.UtcNow,
                    PerformedBy = request.SignedBy,
                };

                await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);
                await _unitOfWork.SaveChanges();

                var fullInvoice = await _unitOfWork.InvoicesRepository
                    .GetByIdAsync(invoice.InvoiceID, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
                var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(fullInvoice);

                var serializer = new XmlSerializer(typeof(HDon));
                var fileName = $"Invoice_{fullInvoice.InvoiceNumber}.xml";
                xmlPath = Path.Combine(Path.GetTempPath(), fileName);
                await using (var fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
                {
                    serializer.Serialize(fs, xmlModel);
                }

                await using var xmlStream = File.OpenRead(xmlPath);
                var uploadResult = await _fileStorageService.UploadFileAsync(xmlStream, Path.GetFileName(xmlPath), "invoices");

                if (uploadResult.IsFailed)
                    return Result.Fail(uploadResult.Errors);

                fullInvoice.XMLPath = uploadResult.Value.Url;
                await _unitOfWork.InvoicesRepository.UpdateAsync(fullInvoice);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                var response = new CreateInvoiceResponse
                {
                    InvoiceID = fullInvoice.InvoiceID,
                    InvoiceNumber = fullInvoice.InvoiceNumber,
                    CustomerID = fullInvoice.CustomerID,
                    TotalAmount = fullInvoice.TotalAmount,
                    TotalAmountInWords = fullInvoice.TotalAmountInWords,
                    Status = "Draft",
                    XMLPath = fullInvoice.XMLPath
                };
                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                // If anything fails, roll back everything.
                await _unitOfWork.RollbackAsync();

                Console.WriteLine(ex.ToString());

                return Result.Fail(new Error($"Failed to create invoice: {ex.Message}").WithMetadata("ErrorCode", "Invoice.Create.Exception").CausedBy(ex));
            }
            finally
            {
                // 12. Clean up temp file
                if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath))
                {
                    File.Delete(xmlPath);
                }
            }
        }
    }
}