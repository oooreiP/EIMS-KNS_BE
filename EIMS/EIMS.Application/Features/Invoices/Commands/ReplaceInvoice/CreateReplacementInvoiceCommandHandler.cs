using AutoMapper;
using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.ReplaceInvoice
{
    public class CreateReplacementInvoiceHandler : IRequestHandler<CreateReplacementInvoiceCommand, Result<CreateInvoiceResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileStorageService;
        private readonly IEmailService _emailService;
        private readonly IInvoiceXMLService _invoiceXMLService;
        private readonly INotificationService _notiService;
        private readonly IMapper _mapper;

        public CreateReplacementInvoiceHandler(
            IUnitOfWork uow,
            IMapper mapper,
            IFileStorageService fileStorageService,
            IEmailService emailService,
            IInvoiceXMLService invoiceXMLService,
            INotificationService notiService)
        {
            _uow = uow;
            _mapper = mapper;
            _fileStorageService = fileStorageService;
            _emailService = emailService;
            _invoiceXMLService = invoiceXMLService;
            _notiService = notiService;
        }

        public async Task<Result<CreateInvoiceResponse>> Handle(CreateReplacementInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                return Result.Fail(new Error("Invoice must has at least one item"));

            if (request.TemplateID == null || request.TemplateID == 0)
                return Result.Fail(new Error("Invoice must has a valid template id"));

            var template = await _uow.InvoiceTemplateRepository.GetByIdAsync(request.TemplateID.Value);
            if (template == null) return Result.Fail($"Template {request.TemplateID} not found");
            var originalInvoice = await _uow.InvoicesRepository.GetAllQueryable()
                .Include(x => x.Template)
        .ThenInclude(t => t.Serial)
            .ThenInclude(s => s.Prefix)

        // Nhánh 2: Lấy InvoiceType (Lặp lại đường dẫn đến Serial)
        .Include(x => x.Template)
            .ThenInclude(t => t.Serial)
                .ThenInclude(s => s.InvoiceType) // <--- Thêm dòng này
        .Include(x => x.Template)
            .ThenInclude(t => t.Serial)
              .ThenInclude(s => s.SerialStatus)
        .Include(x => x.Customer)
        .FirstOrDefaultAsync(x => x.InvoiceID == request.OriginalInvoiceId);

            if (originalInvoice == null) return Result.Fail("Không tìm thấy hóa đơn gốc.");

            // Chỉ thay thế hóa đơn Đã phát hành (2) hoặc Đã điều chỉnh (11)
            if (originalInvoice.InvoiceStatusID != 2 && originalInvoice.InvoiceStatusID != 11)
                return Result.Fail("Chỉ được thay thế hóa đơn đã phát hành.");

            // Không thay thế hóa đơn đã bị Thay thế (3) hoặc Hủy (5)
            if (originalInvoice.InvoiceStatusID == 3 || originalInvoice.InvoiceStatusID == 5)
                return Result.Fail("Hóa đơn này đã bị thay thế hoặc hủy bỏ trước đó.");

            string? xmlPath = null;
            await using var transaction = await _uow.BeginTransactionAsync();

            try
            {
                Customer? customer = null;
                if (request.CustomerID == null || request.CustomerID == 0)
                {
                    // Tạo khách hàng mới nếu user chọn "Khách lẻ/Mới"
                    customer = new Customer
                    {
                        CustomerName = request.CustomerName ?? "Khách hàng chưa đặt tên",
                        TaxCode = request.TaxCode ?? "",
                        Address = request.Address ?? "Chưa cập nhật",
                        ContactEmail = request.ContactEmail ?? "noemail@system.local",
                        ContactPerson = request.ContactPerson,
                        ContactPhone = request.ContactPhone ?? ""
                    };
                    customer = await _uow.CustomerRepository.CreateCustomerAsync(customer);
                    await _uow.SaveChanges();
                }
                else
                {
                    // Lấy khách hàng cũ nhưng có thể update thông tin tạm thời trên hóa đơn
                    // (Lưu ý: Code này đang lấy entity Customer gốc. Nếu muốn sửa trên hóa đơn mà ko sửa gốc,
                    // cần gán vào các trường InvoiceCustomerName...)
                    customer = await _uow.CustomerRepository.GetByIdAsync(request.CustomerID.Value);
                    if (customer == null) return Result.Fail($"Customer {request.CustomerID} not found");
                }

                // =========================================================================
                // 4. TÍNH TOÁN ITEM & TIỀN (Logic từ CreateInvoice - Làm lại bảng mới)
                // =========================================================================
                var productIds = request.Items.Select(i => i.ProductId).Distinct().ToList();
                var products = await _uow.ProductRepository.GetAllQueryable()
                    .Where(p => productIds.Contains(p.ProductID))
                    .ToListAsync(cancellationToken);

                var productDict = products.ToDictionary(p => p.ProductID);
                if (products.Count != productIds.Count) return Result.Fail("One or more products not found");

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
                        UnitPrice = productInfo.BasePrice, // Hoặc itemReq.UnitPrice nếu cho phép sửa giá gốc
                        IsAdjustmentItem = false // Thay thế là sinh ra item mới hoàn toàn, không phải item điều chỉnh
                    });
                }

                decimal subtotal = processedItems.Sum(i => i.Amount);
                decimal vatAmount = processedItems.Sum(i => i.VATAmount);
                decimal totalAmount = subtotal + vatAmount;

                // Override nếu request có gửi tổng (Logic cũ)
                if ((request.Amount ?? 0) <= 0) request.Amount = subtotal;
                if ((request.TaxAmount ?? 0) <= 0) request.TaxAmount = vatAmount;
                if ((request.TotalAmount ?? 0) <= 0) request.TotalAmount = totalAmount;

                decimal invoiceVatRate = (subtotal > 0) ? Math.Round((vatAmount / subtotal) * 100, 2) : 0;

                // =========================================================================
                // 5. CHUẨN BỊ SỐ HÓA ĐƠN & DÒNG THAM CHIẾU (Đặc trưng Replace)
                // =========================================================================
                var serial = await _uow.SerialRepository.GetByIdAndLockAsync(template.SerialID);
                var oldSerial = originalInvoice.Template.Serial;
                var prefix = oldSerial.Prefix;
                string khmsHDon = prefix.PrefixID.ToString();
                string khHDon =
                    $"{oldSerial.SerialStatus.Symbol}" +
                    $"{oldSerial.Year}" +
                    $"{oldSerial.InvoiceType.Symbol}" +
                    $"{oldSerial.Tail}";
                string soHoaDon = originalInvoice.InvoiceNumber.Value.ToString("D7");
                string refText = $"(Thay thế cho hóa đơn Mẫu số {khmsHDon} Ký hiệu {khHDon} Số {originalInvoice.InvoiceNumber:D7} ngày {originalInvoice.IssuedDate:dd/MM/yyyy})";
                var replacementInvoice = new Invoice
                {
                    InvoiceType = 3, // 3 = Replacement
                    OriginalInvoiceID = originalInvoice.InvoiceID,
                    AdjustmentReason = refText,
                    CreatedAt = DateTime.UtcNow, // Thời gian hiện tại
                    InvoiceStatusID = 1, // Draft
                    TemplateID = request.TemplateID.Value,
                    CustomerID = customer.CustomerID,
                    SalesID = originalInvoice.SalesID,
                    Notes = request.Notes, // Ghi chú user nhập (Lý do thay thế ngắn gọn)
                    CompanyId = request.CompanyID,
                    PaymentMethod = request.PaymentMethod,
                    ReferenceNote = refText,
                    SubtotalAmount = subtotal,
                    VATAmount = vatAmount,
                    VATRate = invoiceVatRate,
                    TotalAmount = totalAmount,
                    TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(totalAmount),
                    PaymentStatusID = 1,
                    PaymentDueDate = DateTime.UtcNow.AddDays(30),
                    MinRows = originalInvoice.MinRows,
                    PaidAmount = 0,
                    RemainingAmount = totalAmount,
                    InvoiceItems = processedItems,
                    InvoiceCustomerName = request.CustomerName ?? customer.CustomerName,
                    InvoiceCustomerAddress = request.Address ?? customer.Address,
                    InvoiceCustomerTaxCode = request.TaxCode ?? customer.TaxCode,
                };
                originalInvoice.InvoiceStatusID = 3; // Replaced
                await _uow.InvoicesRepository.UpdateAsync(originalInvoice);
                await _uow.InvoicesRepository.CreateInvoiceAsync(replacementInvoice);
                await _uow.SaveChanges();
                var history = new InvoiceHistory
                {
                    InvoiceID = replacementInvoice.InvoiceID,
                    ActionType = "Replaced",
                    ReferenceInvoiceID = originalInvoice.InvoiceID,
                    Date = DateTime.UtcNow,
                    PerformedBy = originalInvoice.IssuerID,
                };
                await _uow.InvoiceHistoryRepository.CreateAsync(history);
                await _uow.SaveChanges();

                await _uow.CommitAsync(); // Commit Transaction

                // --- Sau khi commit mới sinh XML và gửi Noti ---

                // Load lại full data để sinh XML
                var fullInvoice = await _uow.InvoicesRepository.GetByIdAsync(
                    replacementInvoice.InvoiceID,                   "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus,Template.Serial.InvoiceType,InvoiceStatus,Company"
                );
                string newXmlUrl = await _invoiceXMLService.GenerateAndUploadXmlAsync(fullInvoice);
                fullInvoice.XMLPath = newXmlUrl;
                await _uow.InvoicesRepository.UpdateAsync(fullInvoice);
                await _uow.SaveChanges();
                var linkedUsers = await _uow.UserRepository.GetUsersByCustomerIdAsync(fullInvoice.CustomerID);

                if (linkedUsers != null && linkedUsers.Any())
                {
                    // Chỉ gửi nếu có User liên kết
                    foreach (var user in linkedUsers)
                    {
                        await _notiService.SendToUserAsync(user.UserID, 
                            $"Hóa đơn #{originalInvoice.InvoiceNumber} đã bị thay thế bởi hóa đơn mới {fullInvoice.InvoiceNumber}.",
                            typeId: 2);
                    }
                }
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

                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                await _uow.RollbackAsync();
                Console.WriteLine(ex.ToString());
                return Result.Fail(new Error($"Failed to replace invoice: {ex.Message}").CausedBy(ex));
            }
            finally
            {
                if (!string.IsNullOrEmpty(xmlPath) && File.Exists(xmlPath)) File.Delete(xmlPath);
            }
        }
    }
}