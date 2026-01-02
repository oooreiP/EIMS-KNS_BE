using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using EIMS.Domain.Constants;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Invoices.Commands.ReplaceInvoice
{
    public class CreateReplacementInvoiceCommandHandler : IRequestHandler<CreateReplacementInvoiceCommand, Result<int>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IEmailService _emailService;
        private readonly IFileStorageService _fileStorageService;
        private readonly INotificationService _notiService;
        private readonly IInvoiceXMLService _xMLService;
        public CreateReplacementInvoiceCommandHandler(IUnitOfWork uow, IEmailService emailService, IFileStorageService fileStorageService, INotificationService notiService, IInvoiceXMLService xMLService)
        {
            _uow = uow;
            _emailService = emailService;
            _fileStorageService = fileStorageService;
            _notiService = notiService;
            _xMLService = xMLService;
        }

        public async Task<Result<int>> Handle(CreateReplacementInvoiceCommand request, CancellationToken cancellationToken)
        {
            var originalInvoice = await _uow.InvoicesRepository.GetByIdAsync(
                request.OriginalInvoiceId,
                includeProperties: "InvoiceItems,InvoiceItems.Product,Customer,Company"
            );
            if (originalInvoice == null) return Result.Fail("Không tìm thấy hóa đơn gốc.");
            if (originalInvoice.InvoiceStatusID != 2 && originalInvoice.InvoiceStatusID != 11)
                return Result.Fail("Chỉ được thay thế hóa đơn đã phát hành.");
            HDon? xmlData = null;
            try
            {
                if (!string.IsNullOrEmpty(originalInvoice.XMLPath))
                {
                    // Tải file XML về stream
                    var xmlContentString = await _xMLService.DownloadStringAsync(originalInvoice.XMLPath);
                    if (!string.IsNullOrEmpty(xmlContentString))
                    {
                        var OriginalSerializer = new XmlSerializer(typeof(HDon));
                        using (var reader = new StringReader(xmlContentString))
                        {
                            xmlData = (HDon)OriginalSerializer.Deserialize(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // _logger.LogWarning("Không đọc được XML gốc: " + ex.Message);
            }
            int targetCustomerId = originalInvoice.CustomerID; // Mặc định ID cũ
            string custName, custTax, custAddr;
            // Ưu tiên 1: Lấy từ Request (Nếu user chọn khách mới)
            if (request.CustomerId.HasValue && request.CustomerId != originalInvoice.CustomerID)
            {
                targetCustomerId = request.CustomerId.Value;
                var newCustomer = await _uow.CustomerRepository.GetByIdAsync(targetCustomerId);
                if (newCustomer == null) return Result.Fail("Khách hàng mới không tồn tại.");

                custName = newCustomer.CustomerName;
                custTax = newCustomer.TaxCode;
                custAddr = newCustomer.Address;
            }
            // Ưu tiên 2: Lấy từ XML gốc (Nếu có) -> Chính xác pháp lý nhất
            else if (xmlData != null && xmlData.DLHDon?.NDHDon?.NMua != null)
            {
                custName = xmlData.DLHDon.NDHDon.NMua.Ten;
                custTax = xmlData.DLHDon.NDHDon.NMua.MST;
                custAddr = xmlData.DLHDon.NDHDon.NMua.DChi;
            }
            // Ưu tiên 3: Fallback về DB của hóa đơn gốc
            else
            {
                custName = originalInvoice.InvoiceCustomerName;
                custTax = originalInvoice.InvoiceCustomerTaxCode;
                custAddr = originalInvoice.InvoiceCustomerAddress;
            }
            string targetNote = request.Note ?? originalInvoice.Notes;
            var newInvoice = new Invoice
            {
                InvoiceType = 3, // 3: Thay thế
                OriginalInvoiceID = originalInvoice.InvoiceID,
                AdjustmentReason = request.Reason,
                CustomerID = targetCustomerId,
                Notes = targetNote,
                InvoiceCustomerName = custName,
                InvoiceCustomerTaxCode = custTax,
                InvoiceCustomerAddress = custAddr,
                TemplateID = originalInvoice.TemplateID,
                CompanyId = originalInvoice.CompanyId,
                IssuerID = originalInvoice.IssuerID,
                SalesID = originalInvoice.SalesID,
                InvoiceStatusID = 1, // Draft
                CreatedAt = DateTime.UtcNow,
                PaymentStatusID = originalInvoice.PaymentStatusID,
                PaymentMethod = originalInvoice.PaymentMethod,
                SignDate = null,
                IssuedDate = null,
                DigitalSignature = null,
                TaxAuthorityCode = null,
            };
            decimal totalSubtotal = 0;
            decimal totalVAT = 0;
            var distinctVatRates = new HashSet<decimal>();
            if (request.Items != null && request.Items.Any())
            {
                foreach (var itemInput in request.Items)
                {
                    var product = await _uow.ProductRepository.GetByIdAsync(itemInput.ProductID);
                    if (product == null) return Result.Fail($"Sản phẩm {itemInput.ProductID} không tồn tại.");
                    decimal price = itemInput.UnitPrice ?? product.BasePrice;
                    decimal vatRate = itemInput.OverrideVATRate ?? product?.VATRate ?? 0;
                    distinctVatRates.Add(vatRate);
                    decimal amount = (decimal)itemInput.Quantity * price;
                    decimal vatAmount = amount * (vatRate / 100m);

                    newInvoice.InvoiceItems.Add(new InvoiceItem
                    {
                        ProductID = itemInput.ProductID,
                        Quantity = itemInput.Quantity,
                        UnitPrice = price,
                        Amount = amount,
                        VATAmount = vatAmount
                    });

                    totalSubtotal += amount;
                    totalVAT += vatAmount;
                }
            }
            else
            {
                if (originalInvoice.InvoiceItems != null)
                {
                    foreach (var oldItem in originalInvoice.InvoiceItems)
                    {
                        decimal oldItemRate = oldItem.Product?.VATRate ?? 0;
                        distinctVatRates.Add(oldItemRate);
                        var newItem = new InvoiceItem
                        {
                            ProductID = oldItem.ProductID,
                            Quantity = oldItem.Quantity,
                            UnitPrice = oldItem.UnitPrice,
                            Amount = oldItem.Amount,
                            VATAmount = oldItem.VATAmount
                        };
                        newInvoice.InvoiceItems.Add(newItem);
                        totalSubtotal += oldItem.Amount;
                        totalVAT += oldItem.VATAmount;
                    }
                }
            }
            newInvoice.SubtotalAmount = totalSubtotal;
            newInvoice.VATAmount = totalVAT;
            newInvoice.TotalAmount = totalSubtotal + totalVAT;

            if (distinctVatRates.Count == 0)
            {
                newInvoice.VATRate = 0;
            }
            else if (distinctVatRates.Count == 1)
            {
                newInvoice.VATRate = distinctVatRates.First();
            }
            else
            {
                // Trường hợp đa thuế suất (Có cả 8% và 10%)
                // Theo quy định XML CQT: 
                // Nếu hóa đơn hỗn hợp, thường để -1 (Khác) hoặc hệ thống phải chặn.
                // Ở đây ta gán -1 để biểu thị "Nhiều thuế suất"
                newInvoice.VATRate = -1;
            }
            await _uow.InvoicesRepository.CreateAsync(newInvoice);
            await _uow.SaveChanges();
            // Log 1: Gắn cho Hóa đơn CŨ
            var logForOld = new InvoiceHistory
            {
                InvoiceID = originalInvoice.InvoiceID,        // 100
                ActionType = InvoiceActionTypes.Replaced, // "Bị thay thế"
                ReferenceInvoiceID = newInvoice.InvoiceID, // Trỏ đến 200
                PerformedBy = request.PerformedBy ?? newInvoice.IssuerID,
                Date = DateTime.UtcNow
            };
            // Log 2: Gắn cho Hóa đơn MỚI
            var logForNew = new InvoiceHistory
            {
                InvoiceID = newInvoice.InvoiceID,           
                ActionType = InvoiceActionTypes.Replacement, // "Là bản thay thế"
                ReferenceInvoiceID = originalInvoice.InvoiceID,  
                PerformedBy = request.PerformedBy ?? newInvoice.IssuerID,
                Date = DateTime.UtcNow
            };

            // 3. Thêm vào DB
            await _uow.InvoiceHistoryRepository.CreateAsync(logForOld);
            await _uow.InvoiceHistoryRepository.CreateAsync(logForNew);
            await _uow.SaveChanges();
            var fullInvoice = await _uow.InvoicesRepository
                   .GetByIdAsync(newInvoice.InvoiceID, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType");
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
                string content = $"Hóa đơn #{originalInvoice.InvoiceNumber} đã bị THAY THẾ. Lý do: {request.Reason}";
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
            await _emailService.SendStatusUpdateNotificationAsync(newInvoice.InvoiceID, 11);
            return Result.Ok(newInvoice.InvoiceID);
        }
    }
}
