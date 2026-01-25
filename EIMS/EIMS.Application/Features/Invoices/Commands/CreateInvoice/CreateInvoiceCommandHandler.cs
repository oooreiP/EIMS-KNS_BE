using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<CreateInvoiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;
        private readonly IInvoiceBackgroundService _invoiceBackgroundService; // Inject service mới

        public CreateInvoiceCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUser,
            IInvoiceBackgroundService invoiceBackgroundService)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
            _invoiceBackgroundService = invoiceBackgroundService;
        }

        public async Task<Result<CreateInvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var userId = int.Parse(_currentUser.UserId);
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
                    RequestID = request.RequestID,
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
                    CreatedBy = request.PerformedBy ?? userId,
                    InvoiceCustomerType = request.InvoiceCustomerType,
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
                    PerformedBy = userId,
                };
                await _unitOfWork.InvoiceHistoryRepository.CreateAsync(history);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                _invoiceBackgroundService.ProcessInvoiceCreation(invoice.InvoiceID, userId, request.RequestID);

                // --- PHẦN 4: TRẢ VỀ KẾT QUẢ NGAY LẬP TỨC ---
                var response = new CreateInvoiceResponse
                {
                    InvoiceID = invoice.InvoiceID,
                    CustomerID = invoice.CustomerID,
                    TotalAmount = invoice.TotalAmount,
                    TotalAmountInWords = invoice.TotalAmountInWords,
                    PaymentMethod = invoice.PaymentMethod,
                    Status = "Processing", // Báo cho UI biết là đang xử lý file
                    XMLPath = null // Chưa có file ngay lập tức
                };

                return Result.Ok(response);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
        return Result.Fail(new Error($"Failed: {ex.Message}"));
    }
}
    }
}