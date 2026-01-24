using EIMS.Application.Commons;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Application.Features.Invoices.Commands.ReplaceInvoice;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using EIMS.Domain.Enums;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.AdjustInvoice
{
    public class CreateAdjustmentInvoiceCommandHandler : IRequestHandler<CreateAdjustmentInvoiceCommand, Result<AdjustmentInvoiceDetailDto>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileStorageService;
        private readonly IInvoiceXMLService _invoiceXMLService;
        private readonly IPdfService _pdfService;
        private readonly INotificationService _notiService;
        private readonly ICurrentUserService _currentUser;
        public CreateAdjustmentInvoiceCommandHandler(IUnitOfWork uow, IFileStorageService fileStorageService, INotificationService notiService, IPdfService pdfService, IInvoiceXMLService invoiceXMLService, ICurrentUserService currentUser)
        {
            _uow = uow;
            _fileStorageService = fileStorageService;
            _notiService = notiService;
            _pdfService = pdfService;
            _invoiceXMLService = invoiceXMLService;
            _currentUser = currentUser;
        }
        public async Task<Result<AdjustmentInvoiceDetailDto>> Handle(CreateAdjustmentInvoiceCommand request, CancellationToken cancellationToken)
        {
            var userId = int.Parse(_currentUser.UserId);
            // =========================================================================
            // BƯỚC 1: LẤY HÓA ĐƠN GỐC & VALIDATE
            // =========================================================================
            var originalInvoice = await _uow.InvoicesRepository.GetByIdAsync(request.OriginalInvoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,InvoiceStatus,Company");
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
                InvoiceType = 2,
                OriginalInvoiceID = originalInvoice.InvoiceID,
                AdjustmentReason = request.AdjustmentReason,
                TaxAuthorityCode = null,
                DigitalSignature = null,
                SignDate = null,
                IssuedDate = null,
                TemplateID = request.TemplateId ?? originalInvoice.TemplateID,
                CompanyId = originalInvoice.CompanyId,
                IssuerID = originalInvoice.IssuerID,
                CustomerID = originalInvoice.CustomerID,
                SalesID = originalInvoice.SalesID,
                InvoiceCustomerName = originalInvoice.InvoiceCustomerName,
                InvoiceCustomerAddress = originalInvoice.InvoiceCustomerAddress,
                InvoiceCustomerTaxCode = originalInvoice.InvoiceCustomerTaxCode,
                InvoiceStatusID = request.InvoiceStatusId ?? 1,
                CreatedAt = DateTime.UtcNow,
                PaymentDueDate = DateTime.UtcNow.AddDays(30),
                PaymentStatusID = 1
            };
            decimal totalAdjSubtotal = 0;
            decimal totalAdjVAT = 0;
            if (request.AdjustmentItems != null)
            {
                foreach (var itemDto in request.AdjustmentItems)
                {
                    // Tìm sản phẩm gốc để đối chiếu số lượng (Validation)
                    var originalItem = originalInvoice.InvoiceItems
                        .FirstOrDefault(x => x.ProductID == itemDto.ProductID);
                    var product = await _uow.ProductRepository.GetByIdAsync(itemDto.ProductID);
                    if (product == null)
                    {
                        return Result.Fail($"Không tìm thấy sản phẩm ID = {itemDto.ProductID}");
                    }

                    if (originalItem != null)
                    {
                        var origQty = originalItem?.Quantity ?? 0;
                        decimal origPrice = originalItem?.UnitPrice ?? 0;
                        var finalQty = origQty + itemDto.Quantity;

                        // Rule: Số lượng cuối không được Âm
                        if (finalQty < 0)
                        {
                            return Result.Fail($"Sản phẩm {product?.Name ?? itemDto.ProductID.ToString()}: Số lượng điều chỉnh giảm quá lớn. (Gốc: {origQty}, Giảm: {itemDto.Quantity} -> Còn: {finalQty})");
                        }

                        // Rule: Đơn giá cuối không được Âm (trừ trường hợp chiết khấu đặc biệt, nhưng thường là không)
                        decimal adjPrice = itemDto.UnitPrice ?? 0; // Giá chênh lệch
                        decimal finalPrice = origPrice + adjPrice;
                        if (finalPrice < 0)
                        {
                            return Result.Fail($"Sản phẩm {product?.Name}: Đơn giá sau điều chỉnh bị âm.");
                        }

                        decimal vatRate = itemDto.OverrideVATRate ?? product?.VATRate ?? originalItem?.Product?.VATRate ?? 0;
                        decimal originalAmount = (decimal)origQty * origPrice;
                        decimal finalAmount = (decimal)finalQty * finalPrice;
                        decimal adjustmentAmount = finalAmount - originalAmount;

                        decimal adjustmentVAT = adjustmentAmount * (vatRate / 100m);

                        // Tạo Item (Lưu số chênh lệch)
                        var newItem = new InvoiceItem
                        {
                            IsAdjustmentItem = true,
                            OriginalInvoiceItemID = originalItem.InvoiceItemID,
                            ProductID = itemDto.ProductID,
                            Quantity = itemDto.Quantity, // Lưu phần chênh lệch (VD: -2)
                            UnitPrice = adjPrice,                  // Lưu phần chênh lệch giá (VD: 0 hoặc -1000)
                            Amount = adjustmentAmount,             // Lưu tiền chênh lệch (Có thể Âm)
                            VATAmount = adjustmentVAT              // Lưu thuế chênh lệch (Có thể Âm)
                        };

                        adjInvoice.InvoiceItems.Add(newItem);

                        totalAdjSubtotal += adjustmentAmount;
                        totalAdjVAT += adjustmentVAT;
                    }
                    else
                    {
                        if (itemDto.Quantity <= 0)
                        {
                            return Result.Fail(
                                $"Sản phẩm {product.Name}: Không thể thêm mới với số lượng <= 0."
                            );
                        }

                        decimal unitPrice = itemDto.UnitPrice ?? product.BasePrice;
                        if (unitPrice < 0)
                        {
                            return Result.Fail($"Sản phẩm {product.Name}: Đơn giá không hợp lệ.");
                        }

                        decimal vatRate = itemDto.OverrideVATRate ?? product.VATRate ?? 0;

                        decimal amount = (decimal)itemDto.Quantity * unitPrice;
                        decimal vatAmount = amount * (vatRate / 100m);

                        var newItem = new InvoiceItem
                        {
                            IsAdjustmentItem = true,
                            OriginalInvoiceItemID = null, 
                            ProductID = itemDto.ProductID,
                            Quantity = itemDto.Quantity,
                            UnitPrice = unitPrice,
                            Amount = amount,
                            VATAmount = vatAmount
                        };

                        adjInvoice.InvoiceItems.Add(newItem);

                        totalAdjSubtotal += amount;
                        totalAdjVAT += vatAmount;
                    }
                }
            }

            adjInvoice.SubtotalAmount = totalAdjSubtotal;
            adjInvoice.VATAmount = totalAdjVAT;
            adjInvoice.TotalAmount = totalAdjSubtotal + totalAdjVAT;
            adjInvoice.TotalAmountInWords = NumberToWordsConverter.ChuyenSoThanhChu(totalAdjSubtotal + totalAdjVAT);

            var adjustmentType = (adjInvoice.TotalAmount >= 0) ? 1 : 2;
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
            }
            string typeText = adjInvoice.TotalAmount >= 0 ? "tăng" : "giảm";
            var template = originalInvoice.Template;
            var serial = template.Serial;
            var prefix = serial.Prefix;
            string khmsHDon = prefix.PrefixID.ToString();
            string khHDon =
                $"{serial.SerialStatus.Symbol}" +
                $"{serial.Year}" +
                $"{serial.InvoiceType.Symbol}" +
                $"{serial.Tail}";
            string soHoaDon = originalInvoice.InvoiceNumber.Value.ToString("D7");
            DateTime ngayGoc = originalInvoice.IssuedDate ?? originalInvoice.SignDate ?? originalInvoice.CreatedAt;
            string autoReferenceText = $"Điều chỉnh {typeText} cho hóa đơn Mẫu số {khmsHDon} Ký hiệu {khHDon} Số {soHoaDon} ngày {ngayGoc.Day:00} tháng {ngayGoc.Month:00} năm {ngayGoc.Year}";         
            adjInvoice.ReferenceNote = autoReferenceText;
            await _uow.InvoicesRepository.CreateAsync(adjInvoice);
            try
            {
                await _uow.SaveChanges();
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                var msg = ex.Message;
                var inner = ex.InnerException;
                while (inner != null)
                {
                    msg += " | INNER: " + inner.Message;
                    inner = inner.InnerException;
                }
                // Dòng này sẽ in toang hoác lỗi ra màn hình cho bạn thấy
                throw new Exception($"🔥 LỖI THỰC SỰ LÀ: {msg}");
            }
            var logForOld = new InvoiceHistory
            {
                InvoiceID = originalInvoice.InvoiceID,        // 100
                ActionType = InvoiceActionTypes.Adjusted, // "Bị thay thế"
                ReferenceInvoiceID = adjInvoice.InvoiceID, // Trỏ đến 200
                PerformedBy = userId,
                Date = DateTime.UtcNow
            };

            // Log 2: Gắn cho Hóa đơn MỚI
            var logForNew = new InvoiceHistory
            {
                InvoiceID = adjInvoice.InvoiceID,
                ActionType = InvoiceActionTypes.Adjustment, // "Là bản thay thế"
                ReferenceInvoiceID = originalInvoice.InvoiceID,
                PerformedBy = userId,
                Date = DateTime.UtcNow
            };

            // 3. Thêm vào DB
            await _uow.InvoiceHistoryRepository.CreateAsync(logForOld);
            await _uow.InvoiceHistoryRepository.CreateAsync(logForNew);
            var fullInvoice = await _uow.InvoicesRepository
                   .GetByIdAsync(adjInvoice.InvoiceID, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus,Template.Serial.InvoiceType,InvoiceStatus,Company");
            var fulltemplate = originalInvoice.Template;
            var fullserial = fulltemplate.Serial;
            var fullprefix = fullserial.Prefix;
            string fullkhmsHDon = fullprefix.PrefixID.ToString();
            string fullkhHDon =
                $"{fullserial.SerialStatus.Symbol}" +
                $"{fullserial.Year}" +
                $"{fullserial.InvoiceType.Symbol}" +
                $"{fullserial.Tail}";
            string originalAutoReferenceText = $"Bị điều chỉnh bởi hóa đơn Mẫu số {fullkhmsHDon} Ký hiệu {fullkhHDon} Số {soHoaDon} ngày {ngayGoc.Day:00} tháng {ngayGoc.Month:00} năm {ngayGoc.Year}";
            string newXmlUrl = await _invoiceXMLService.GenerateAndUploadXmlAsync(fullInvoice);
            fullInvoice.XMLPath = newXmlUrl;
            originalInvoice.ReferenceNote = originalAutoReferenceText;
            await _uow.InvoicesRepository.UpdateAsync(fullInvoice);
            await _uow.InvoicesRepository.UpdateAsync(originalInvoice);
            await _uow.SaveChanges();
            try
            {
                byte[] pdfBytes = await _pdfService.ConvertXmlToPdfAsync(fullInvoice.InvoiceID, request.RootPath);
                using (var pdfStream = new MemoryStream(pdfBytes))
                {
                    string filePdfName = $"Invoice_{fullInvoice.InvoiceNumber}_{Guid.NewGuid()}.pdf";
                    var uploadPdfResult = await _fileStorageService.UploadFileAsync(pdfStream, filePdfName, "invoices");

                    if (uploadPdfResult.IsSuccess)
                    {
                        fullInvoice.FilePath = uploadPdfResult.Value.Url;
                        await _uow.InvoicesRepository.UpdateAsync(fullInvoice);
                        await _uow.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
            }
            var notificationsToCreate = new List<Notification>();
            if (originalInvoice.Customer != null)
            {
                var users = await _uow.UserRepository.GetUsersByCustomerIdAsync(originalInvoice.CustomerID);
                string content = $"Hóa đơn #{originalInvoice.InvoiceNumber} đã bị ĐIỀU CHỈNH. Lý do: Sai Số tiền";
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
                    try
                    {
                        await _uow.SaveChanges();
                    }
                    catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
                    {
                        var msg = ex.Message;
                        var inner = ex.InnerException;
                        while (inner != null)
                        {
                            msg += " | INNER: " + inner.Message;
                            inner = inner.InnerException;
                        }
                        // Dòng này sẽ in toang hoác lỗi ra màn hình cho bạn thấy
                        throw new Exception($"🔥 LỖI THỰC SỰ LÀ: {msg}");
                    }
                }
            }
            var responseDto = new AdjustmentInvoiceDetailDto
            {
                InvoiceId = adjInvoice.InvoiceID,
                CreatedAt = adjInvoice.CreatedAt,
                ReferenceText = adjInvoice.AdjustmentReason,
                AdjustmentType = adjustmentType,
                Items = new List<AdjustmentItemDto>(),
                FinancialSummary = new FinancialSummaryDto()
            };
            decimal sumOrigAmount = 0;
            decimal sumOrigVAT = 0;

            decimal sumAdjAmount = 0;
            decimal sumAdjVAT = 0;

            decimal sumFinalAmount = 0;
            decimal sumFinalVAT = 0;
            // A. Map Items (Tính toán 3 cột)
            foreach (var adjItem in adjInvoice.InvoiceItems)
            {
                var orgItem = originalInvoice.InvoiceItems.FirstOrDefault(x => x.ProductID == adjItem.ProductID);
                var product = adjItem.Product; // Đã gán ở trên

                // 1. Tính toán số liệu từng dòng
                // GỐC
                double itemOrigQty = orgItem?.Quantity ?? 0;
                decimal itemOrigPrice = orgItem?.UnitPrice ?? 0;
                decimal itemOrigAmount = orgItem?.Amount ?? 0;
                decimal itemOrigVAT = orgItem?.VATAmount ?? 0; // Lấy VAT gốc từ DB cũ

                // ĐIỀU CHỈNH
                double itemAdjQty = adjItem.Quantity;
                decimal itemAdjPrice = adjItem.UnitPrice;
                decimal itemAdjAmount = adjItem.Amount;
                decimal itemAdjVAT = adjItem.VATAmount;

                // CUỐI
                double itemFinalQty = itemOrigQty + itemAdjQty;
                decimal itemFinalPrice = (itemAdjPrice != 0) ? itemOrigPrice + itemAdjPrice : itemOrigPrice;
                decimal itemFinalAmount = itemOrigAmount + itemAdjAmount;
                decimal itemFinalVAT = itemOrigVAT + itemAdjVAT; // Cộng thuế

                // 2. Cộng dồn vào biến tổng (QUAN TRỌNG)
                sumOrigAmount += itemOrigAmount;
                sumOrigVAT += itemOrigVAT;

                sumAdjAmount += itemAdjAmount;
                sumAdjVAT += itemAdjVAT;

                sumFinalAmount += itemFinalAmount;
                sumFinalVAT += itemFinalVAT;

                // 3. Add vào list items
                responseDto.Items.Add(new AdjustmentItemDto
                {
                    ProductName = product?.Name ?? "Unknown",
                    Unit = product?.Unit ?? "",

                    // Gốc
                    OriginalQuantity = itemOrigQty,
                    OriginalUnitPrice = itemOrigPrice,
                    OriginalAmount = itemOrigAmount,

                    // Điều chỉnh
                    AdjustmentQuantity = itemAdjQty,
                    AdjustmentUnitPrice = itemAdjPrice,
                    AdjustmentAmount = itemAdjAmount,

                    // Cuối
                    FinalQuantity = itemFinalQty,
                    FinalUnitPrice = itemFinalPrice,
                    FinalAmount = itemFinalAmount
                });
            }

            // B. Map Summary (Dùng biến đã cộng dồn -> Đảm bảo khớp 100% với Items)
            responseDto.FinancialSummary = new FinancialSummaryDto
            {
                // Cột 1: Gốc
                OriginalTotalAmount = sumOrigAmount + sumOrigVAT, // Tổng tiền thanh toán gốc
                OriginalVATAmount = sumOrigVAT,

                // Cột 2: Điều chỉnh
                AdjustmentTotalAmount = sumAdjAmount + sumAdjVAT, // Tổng tiền điều chỉnh
                AdjustmentVATAmount = sumAdjVAT,

                // Cột 3: Cuối
                FinalTotalAmount = sumFinalAmount + sumFinalVAT,  // Tổng tiền phải thanh toán cuối cùng
                FinalVATAmount = sumFinalVAT
            };

            return Result.Ok(responseDto);
        }
    }
}

