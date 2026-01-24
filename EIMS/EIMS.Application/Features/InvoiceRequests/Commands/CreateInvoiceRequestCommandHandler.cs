using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.Requests;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceRequests.Commands
{
    public class CreateInvoiceRequestCommandHandler : IRequestHandler<CreateInvoiceRequestCommand, Result<CreateRequestResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly INotificationService _notiService;
        private readonly IFileStorageService _fileStorageService;
        private readonly ICurrentUserService _currentUser;
        public CreateInvoiceRequestCommandHandler(IUnitOfWork unitOfWork, INotificationService notiService, ICurrentUserService currentUser, IFileStorageService fileStorageService)
        {
            _unitOfWork = unitOfWork;
            _notiService = notiService;
            _currentUser = currentUser;
            _fileStorageService = fileStorageService;
        }
        public async Task<Result<CreateRequestResponse>> Handle(CreateInvoiceRequestCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            if (userId == 0)
            {
                return Result.Fail(new Error($"Đăng nhập trước khi thực hiện.")
                    .WithMetadata("ErrorCode", "Unauthorize"));
            }
            var user = await _unitOfWork.UserRepository.GetAllQueryable()
                                        .Include(u => u.Role)
                                        .FirstOrDefaultAsync(u => u.UserID == userId, cancellationToken);
            
            if (user == null)
            {
                return Result.Fail(new Error($"Không thể tìm thấy người dùng với ID là {userId}")
                    .WithMetadata("ErrorCode", "User.NotFound"));
            }
            if (!user.Role.RoleName.Equals("Sale", StringComparison.OrdinalIgnoreCase))
                return Result.Fail(new Error("Chỉ có Sale mới được phép tạo Invoice Request"));

            if (request.Items == null || !request.Items.Any())
                return Result.Fail("Request phải có ít nhất 1 vật phẩm");          
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
                var processedItems = new List<InvoiceRequestItem>();
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

                    processedItems.Add(new InvoiceRequestItem
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
                decimal invoiceVatRate = subtotal > 0 ? Math.Round(vatAmount / subtotal * 100, 2) : 0;            
                var invoiceRequest = new InvoiceRequest
                {
                    CustomerID = customer?.CustomerID ?? request.CustomerID!.Value,
                    CreatedAt = DateTime.UtcNow,
                    SaleID = userId,
                    Notes = request.Notes,
                    CompanyID = request.CompanyID,
                    SubtotalAmount = subtotal,
                    VATAmount = vatAmount,
                    VATRate = invoiceVatRate,
                    PaymentMethod = request.PaymentMethod,
                    TotalAmount = totalAmount,
                    TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(totalAmount),
                    RequestStatusID = 1,
                    MinRows = request.MinRows ?? 5,
                    InvoiceRequestItems = processedItems,
                    InvoiceCustomerName = customer.CustomerName,
                    InvoiceCustomerAddress = customer.Address,
                    InvoiceCustomerTaxCode = customer.TaxCode
                };
                await _unitOfWork.InvoiceRequestRepository.CreateAsync(invoiceRequest);
                await _unitOfWork.SaveChanges();
                await _unitOfWork.CommitAsync();
                if (request.AccountantId.HasValue)
                {
                    await _notiService.SendToUserAsync(request.AccountantId.Value,
                $"Có yêu cầu hóa đơn mới với id là {invoiceRequest.RequestID} đã được khởi tạo bởi {user.FullName}. Vui lòng xử lý.",
                typeId: 2);
                }
                var entity = await _unitOfWork.InvoiceRequestRepository.GetAllQueryable()
                .Include(x => x.RequestStatus)
                .Include(x => x.Sales)
                .Include(x => x.InvoiceRequestItems)
                .FirstOrDefaultAsync(x => x.RequestID == invoiceRequest.RequestID, cancellationToken);
                var response = new CreateRequestResponse
                {
                    RequestID = entity.RequestID,
                    CustomerID = entity.CustomerID,
                    TotalAmount = entity.TotalAmount,
                    TotalAmountInWords = entity.TotalAmountInWords,
                    PaymentMethod = entity.PaymentMethod,
                    Status = entity.RequestStatus.StatusName
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
        }
    }
}
