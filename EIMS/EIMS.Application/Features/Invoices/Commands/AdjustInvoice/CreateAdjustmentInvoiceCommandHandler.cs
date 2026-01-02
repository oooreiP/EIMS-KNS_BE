using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.Features.Invoices.Commands.ReplaceInvoice;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.AdjustInvoice
{
    public class CreateAdjustmentInvoiceCommandHandler : IRequestHandler<CreateAdjustmentInvoiceCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileStorageService;
        private readonly IEmailService _emailService;
        private readonly INotificationService _notiService;
        public CreateAdjustmentInvoiceCommandHandler(IUnitOfWork uow, IFileStorageService fileStorageService, IEmailService emailService, INotificationService notiService)
        {
            _uow = uow;
            _fileStorageService = fileStorageService;
            _emailService = emailService;
            _notiService = notiService;
        }
        public async Task<Result<int>> Handle(CreateAdjustmentInvoiceCommand request, CancellationToken cancellationToken)
        {
            // =========================================================================
            // BƯỚC 1: LẤY HÓA ĐƠN GỐC & VALIDATE
            // =========================================================================
            var originalInvoice = await _uow.InvoicesRepository.GetByIdAsync(request.OriginalInvoiceId, "Customer,Company");
            var newCustomer = new Customer();
            if (originalInvoice == null)
                return Result.Fail("Không tìm thấy hóa đơn gốc.");

            // Validate Trạng thái: 
            // Chỉ được điều chỉnh hóa đơn Đã Phát hành (6) hoặc Đang điều chỉnh (11).
            // Tuyệt đối không điều chỉnh hóa đơn Nháp (1), Đã hủy (9), Bị thay thế (10).
            if (originalInvoice.InvoiceStatusID != 2 && originalInvoice.InvoiceStatusID != 10)
            {
                return Result.Fail($"Trạng thái hóa đơn gốc (ID: {originalInvoice.InvoiceStatusID}) không hợp lệ để điều chỉnh. Chỉ hỗ trợ hóa đơn Đã phát hành.");
            }
            // =========================================================================
            // BƯỚC 3: KHỞI TẠO HÓA ĐƠN ĐIỀU CHỈNH (HEADER)
            // =========================================================================
            var adjInvoice = new Invoice
            {
                // 1: Gốc, 2: Điều chỉnh, 3: Thay thế
                InvoiceType = 2,
                OriginalInvoiceID = originalInvoice.InvoiceID,
                AdjustmentReason = request.AdjustmentReason,
                TaxAuthorityCode = null,
                DigitalSignature = null,
                SignDate = null,
                IssuedDate = null,
                TemplateID = originalInvoice.TemplateID,
                CompanyId = originalInvoice.CompanyId,
                IssuerID = originalInvoice.IssuerID,
                CustomerID = originalInvoice.CustomerID,
                SalesID = originalInvoice.SalesID,
                InvoiceCustomerName = originalInvoice.InvoiceCustomerName,
                InvoiceCustomerAddress = originalInvoice.InvoiceCustomerAddress,
                InvoiceCustomerTaxCode = originalInvoice.InvoiceCustomerTaxCode,
                InvoiceStatusID = 1,
                CreatedAt = DateTime.UtcNow,
                PaymentDueDate = DateTime.UtcNow.AddDays(30),
                PaymentStatusID = 1
            };
            decimal totalSubtotal = 0;
            decimal totalVAT = 0;

            // Nếu danh sách items rỗng -> Đây là điều chỉnh thông tin thuần túy (Tiền = 0)
            if (request.AdjustmentItems != null && request.AdjustmentItems.Any())
            {
                foreach (var itemDto in request.AdjustmentItems)
                {
                    var product = await _uow.ProductRepository.GetByIdAsync(itemDto.ProductID);
                    if (product == null) return Result.Fail($"Sản phẩm ID {itemDto.ProductID} không tồn tại.");

                    // Tính toán: Chấp nhận số ÂM cho điều chỉnh giảm
                    decimal quantity = (decimal)itemDto.Quantity;
                    decimal unitPrice = itemDto.UnitPrice ?? 0;
                    decimal vatRate = itemDto.OverrideVATRate ?? product.VATRate ?? 0;
                    if (request.AdjustmentType == EAdjustmentType.Decrease)
                    {
                        if (unitPrice > 0) unitPrice = -unitPrice; 
                    }
                    decimal amount = quantity * unitPrice;
                    decimal vatAmount = amount * (vatRate / 100m);

                    var invoiceItem = new InvoiceItem
                    {
                        ProductID = itemDto.ProductID,
                        Quantity = itemDto.Quantity,
                        UnitPrice = itemDto.UnitPrice ?? 0,
                        Amount = amount,         // Có thể < 0
                        VATAmount = vatAmount    // Có thể < 0
                    };

                    adjInvoice.InvoiceItems.Add(invoiceItem);

                    totalSubtotal += amount;
                    totalVAT += vatAmount;
                }
            }
            adjInvoice.SubtotalAmount = totalSubtotal;
            adjInvoice.VATAmount = totalVAT;
            adjInvoice.TotalAmount = totalSubtotal + totalVAT;
            if (!adjInvoice.InvoiceItems.Any())
            {
                adjInvoice.VATRate = originalInvoice.VATRate;
            }
            else
            {
                adjInvoice.VATRate = request.AdjustmentItems.First().OverrideVATRate
                                     ?? (await _uow.ProductRepository.GetByIdAsync(request.AdjustmentItems.First().ProductID)).VATRate ?? 0;
            }
            // =========================================================================
            // BƯỚC 6: XỬ LÝ LOGIC TRẠNG THÁI & LƯU DB
            // =========================================================================
            if (originalInvoice.InvoiceStatusID == 2)
            {
                originalInvoice.InvoiceStatusID = 10;
                await _uow.InvoicesRepository.UpdateAsync(originalInvoice);
                await _uow.SaveChanges();
            }
            await _uow.InvoicesRepository.CreateAsync(adjInvoice);
            await _uow.SaveChanges();
            var logForOld = new InvoiceHistory
            {
                InvoiceID = originalInvoice.InvoiceID,        // 100
                ActionType = InvoiceActionTypes.Adjusted, // "Bị thay thế"
                ReferenceInvoiceID = adjInvoice.InvoiceID, // Trỏ đến 200
                PerformedBy = request.PerformedBy,
                Date = DateTime.UtcNow
            };

            // Log 2: Gắn cho Hóa đơn MỚI
            var logForNew = new InvoiceHistory
            {
                InvoiceID = adjInvoice.InvoiceID,
                ActionType = InvoiceActionTypes.Adjustment, // "Là bản thay thế"
                ReferenceInvoiceID = originalInvoice.InvoiceID,
                PerformedBy = request.PerformedBy,
                Date = DateTime.UtcNow
            };

            // 3. Thêm vào DB
            await _uow.InvoiceHistoryRepository.CreateAsync(logForOld);
            await _uow.InvoiceHistoryRepository.CreateAsync(logForNew);
            await _uow.SaveChanges();
            var fullInvoice = await _uow.InvoicesRepository
                   .GetByIdAsync(adjInvoice.InvoiceID, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
            var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(fullInvoice);

            var serializer = new XmlSerializer(typeof(HDon));
            var fileName = $"Invoice_{fullInvoice.InvoiceNumber}.xml";
            var xmlPath = Path.Combine(Path.GetTempPath(), fileName);
            await using (var fs = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                serializer.Serialize(fs, xmlModel);
            }
            await using var xmlStream = File.OpenRead(xmlPath);
            var uploadResult = await _fileStorageService.UploadFileAsync(xmlStream, Path.GetFileName(xmlPath), "invoices");
            if (uploadResult.IsFailed)
                return Result.Fail(uploadResult.Errors);
            fullInvoice.XMLPath = uploadResult.Value.Url;
            await _uow.InvoicesRepository.UpdateAsync(fullInvoice);
            await _uow.SaveChanges();
            var notificationsToCreate = new List<Notification>();
            if (originalInvoice.Customer != null)
            {
                var users = await _uow.UserRepository.GetUsersByCustomerIdAsync(originalInvoice.CustomerID);
                string content = $"Hóa đơn #{originalInvoice.InvoiceNumber} đã bị ĐIỀU CHỈNH. Lý do: {request.AdjustmentReason}";
                foreach (var user in users)
                {
                    notificationsToCreate.Add(new Notification
                    {
                        UserID = user.UserID,
                        Content = content,
                        NotificationStatusID = 1,
                        NotificationTypeID = 2,
                        Time = DateTime.UtcNow
                    });
                    await _notiService.SendRealTimeAsync(
                        user.UserID,
                        content,
                        2
                    );
                }
                if (notificationsToCreate.Any())
                {
                    await _uow.NotificationRepository.CreateRangeAsync(notificationsToCreate);
                    await _uow.SaveChanges();
                }
            }
            await _emailService.SendStatusUpdateNotificationAsync(adjInvoice.InvoiceID, 10);
            return Result.Ok(adjInvoice.InvoiceID);
        }
    }
}

