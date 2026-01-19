using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<CreateInvoiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IFileStorageService _fileStorageService;
        private readonly IEmailService _emailService;
        private readonly IPdfService _pdfService;
        private readonly IInvoiceXMLService _invoiceXMLService;
        private readonly INotificationService _notiService;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IFileStorageService fileStorageService, IEmailService emailService, IInvoiceXMLService invoiceXMLService, INotificationService notiService, IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _emailService = emailService;
            _invoiceXMLService = invoiceXMLService;
            _notiService = notiService;
            _pdfService = pdfService;
        }

        public async Task<Result<CreateInvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                return Result.Fail(new Error("Invoice must has at least one item").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            if (request.TemplateID == null || request.TemplateID == 0)
                return Result.Fail(new Error("Invoice must has a valid template id").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            var invoiceStatus = await _unitOfWork.InvoiceStatusRepository.GetByIdAsync(request.InvoiceStatusID);
            if (invoiceStatus == null)
                return Result.Fail(new Error("Invoice Status Id not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            if (request.SalesID.HasValue)
            {
                var salesUser = await _unitOfWork.UserRepository.GetByIdAsync(request.SalesID.Value,"Role");
                if (salesUser == null)
                {
                    return Result.Fail($"Salesperson with ID {request.SalesID} not found.");
                }
                if (salesUser.Role.RoleName != "Sale") return Result.Fail("Selected user is not a salesperson.");
            }
            var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(request.TemplateID.Value);
            if (template == null)
                return Result.Fail(new Error($"Template {request.TemplateID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            string? xmlPath = null;
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
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
                    await _unitOfWork.SaveChanges();
                }
                else
                {
                    customer = await _unitOfWork.CustomerRepository.GetByIdAsync(request.CustomerID.Value);
                    if (customer == null)
                    {
                        return Result.Fail(new Error($"Customer {request.CustomerID} not found"));
                    }
                }
                var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
                var products = await _unitOfWork.ProductRepository.GetAllQueryable()
                .Where(p => productIds.Contains(p.ProductID))
                .ToListAsync(cancellationToken);
                var productDict = products.ToDictionary(p => p.ProductID);
                if (products.Count != productIds.Count)
                    return Result.Fail(new Error("One or more products not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
                var processedItems = new List<InvoiceItem>();
                foreach (var itemReq in request.Items)
                {
                    var productInfo = productDict[itemReq.ProductId];

                    decimal finalAmount = (itemReq.Amount ?? 0) > 0
                        ? itemReq.Amount!.Value
                        : productInfo.BasePrice * (decimal)itemReq.Quantity;

                    decimal finalVatAmount;
                    if ((itemReq.VATAmount ?? 0) > 0)
                    {
                        finalVatAmount = itemReq.VATAmount!.Value;
                    }
                    else
                    {
                        decimal vatRate = productInfo.VATRate ?? 0;
                        finalVatAmount = Math.Round(finalAmount * (vatRate / 100m), 2);
                    }

                    processedItems.Add(new InvoiceItem
                    {
                        ProductID = itemReq.ProductId,
                        Quantity = itemReq.Quantity,
                        Amount = finalAmount,
                        VATAmount = finalVatAmount,
                        UnitPrice = productInfo.BasePrice
                    });
                }
                decimal subtotal = processedItems.Sum(i => i.Amount);
                decimal vatAmount = processedItems.Sum(i => i.VATAmount);
                decimal totalAmount = subtotal + vatAmount;
                if (request.Amount <= 0) request.Amount = subtotal;
                if (request.TaxAmount <= 0) request.TaxAmount = vatAmount;
                if (request.TotalAmount <= 0) request.TotalAmount = request.Amount + request.TaxAmount;
                decimal invoiceVatRate = (subtotal > 0) ? Math.Round((vatAmount / subtotal) * 100, 2) : 0;
                var invoice = new Invoice
                {
                    TemplateID = request.TemplateID.Value,
                    CustomerID = customer?.CustomerID ?? request.CustomerID!.Value,
                    CreatedAt = DateTime.UtcNow,
                    SalesID = request.SalesID,
                    Notes = request.Notes,
                    CompanyId = request.CompanyID,
                    SubtotalAmount = subtotal,
                    VATAmount = vatAmount,
                    VATRate = invoiceVatRate,
                    PaymentMethod = request.PaymentMethod,
                    TotalAmount = totalAmount,
                    TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(totalAmount),
                    InvoiceStatusID = request.InvoiceStatusID,
                    PaymentStatusID = 1,
                    PaymentDueDate = DateTime.UtcNow.AddDays(30),
                    IssuerID = null,
                    CreatedBy = request.PerformedBy,
                    MinRows = request.MinRows ?? 5,
                    PaidAmount = 0,
                    RemainingAmount = totalAmount,
                    InvoiceItems = processedItems,
                    InvoiceCustomerName = customer.CustomerName,
                    InvoiceCustomerAddress = customer.Address,
                    InvoiceCustomerTaxCode = customer.TaxCode,
                };
                await _unitOfWork.InvoicesRepository.CreateInvoiceAsync(invoice);
                await _unitOfWork.SaveChanges();
                var history = new InvoiceHistory
                {
                    InvoiceID = invoice.InvoiceID,
                    ActionType = "Created",
                    ReferenceInvoiceID = null,
                    Date = DateTime.UtcNow,
                    PerformedBy = request.PerformedBy,
                };
                await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                var fullInvoice = await _unitOfWork.InvoicesRepository
                    .GetByIdAsync(invoice.InvoiceID, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus,Company");
                string newXmlUrl = await _invoiceXMLService.GenerateAndUploadXmlAsync(fullInvoice);
                fullInvoice.XMLPath = newXmlUrl;
                await _unitOfWork.InvoicesRepository.UpdateAsync(fullInvoice);
                try
                {
                    string rootPath = AppDomain.CurrentDomain.BaseDirectory;
                    byte[] pdfBytes = await _pdfService.ConvertXmlToPdfAsync(fullInvoice.InvoiceID, rootPath);
                    using (var pdfStream = new MemoryStream(pdfBytes))
                    {
                        string fileName = $"Invoice_{fullInvoice.InvoiceNumber}_{Guid.NewGuid()}.pdf";
                        var uploadResult = await _fileStorageService.UploadFileAsync(pdfStream, fileName, "invoices");

                        if (uploadResult.IsSuccess)
                        {
                            fullInvoice.FilePath = uploadResult.Value.Url;
                            await _unitOfWork.InvoicesRepository.UpdateAsync(fullInvoice);
                            await _unitOfWork.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                await _notiService.SendToRoleAsync("HOD",
                $"Có hóa đơn đã được khởi tạo. Vui lòng xác nhận.",
                typeId: 2);
                if(request.RequestID != null && request.RequestID != 0)
                {
                   var invoiceRequest = await _unitOfWork.InvoiceRequestRepository.GetByIdAsync(request.RequestID ?? 1);
                    invoiceRequest.RequestStatusID = 3;
                    invoiceRequest.CreatedInvoiceID = fullInvoice.InvoiceID;
                    await _unitOfWork.InvoiceRequestRepository.UpdateAsync(invoiceRequest);
                }
                await _unitOfWork.SaveChanges();
                var response = new CreateInvoiceResponse
                {
                    InvoiceID = fullInvoice.InvoiceID,
                    CustomerID = fullInvoice.CustomerID,
                    TotalAmount = fullInvoice.TotalAmount,
                    TotalAmountInWords = fullInvoice.TotalAmountInWords,
                    PaymentMethod = fullInvoice.PaymentMethod,
                    Status = fullInvoice.InvoiceStatus.StatusName,
                    XMLPath = fullInvoice.XMLPath
                };
                // await _emailService.SendStatusUpdateNotificationAsync(invoice.InvoiceID, 1);
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