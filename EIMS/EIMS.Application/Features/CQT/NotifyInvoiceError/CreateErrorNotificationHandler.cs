using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.TaxAPIDTO;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class CreateErrorNotificationHandler : IRequestHandler<CreateErrorNotificationCommand, Result<CreateErrorNotificationResponse>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _xmlService;
        private readonly IFileStorageService _fileService;

        public CreateErrorNotificationHandler(IUnitOfWork uow, IInvoiceXMLService xmlService, IFileStorageService fileService)
        {
            _uow = uow;
            _xmlService = xmlService;
            _fileService = fileService;
        }

        public async Task<Result<CreateErrorNotificationResponse>> Handle(CreateErrorNotificationCommand request, CancellationToken cancellationToken)
        {
            var exists = await _uow.ErrorNotificationRepository.GetByNotificationNumberAsync(request.NotificationNumber);
            if (exists != null)
            {
                return Result.Fail($"Số thông báo '{request.NotificationNumber}' đã tồn tại.");
            }
            var notification = new InvoiceErrorNotification
            {
                Status = 1, // Draft
                CreatedAt = request.CreatedDate ?? DateTime.UtcNow,
                NotificationNumber = request.NotificationNumber,
                NotificationTypeCode = request.NotificationTypeCode,
                TaxAuthorityName = request.TaxAuthority, 
                TaxAuthorityCode = request.TaxAuthorityCode,
                TaxpayerName = request.TaxpayerName,
                TaxCode = request.TaxCode,
                Place = request.Place,
                Details = new List<InvoiceErrorDetail>()
            };

            foreach (var item in request.ErrorItems)
            {
                var invoice = await _uow.InvoicesRepository.GetByIdAsync(item.InvoiceId, "Customer,InvoiceItems.Product,Template.Serial.Prefix,Template.Serial.SerialStatus, Template.Serial.InvoiceType,Company");

                if (invoice == null)
                    return Result.Fail("Không tìm thấy hóa đơn.");
                var validationResult = ValidateInvoiceStatus(invoice, item.ErrorType);
                if (validationResult.IsFailed)
                {
                    return validationResult;
                }
                if (string.IsNullOrEmpty(invoice.TaxAuthorityCode))
                    return Result.Fail("Hóa đơn chưa được cấp mã CQT. Không thể gửi thông báo sai sót (Mẫu 04/SS).");
                var template = invoice.Template;
                var serial = template.Serial;
                var prefix = serial.Prefix;
                string khmsHDon = prefix.PrefixID.ToString();
                string khHDon = khmsHDon +
                    $"{serial.SerialStatus.Symbol}" +
                    $"{serial.Year}" +
                    $"{serial.InvoiceType.Symbol}" +
                    $"{serial.Tail}";

                notification.Details.Add(new InvoiceErrorDetail
                {
                    InvoiceID = invoice.InvoiceID,
                    Invoice = invoice,
                    InvoiceTaxCode = invoice.TaxAuthorityCode,
                    InvoiceSerial = khHDon,
                    InvoiceNumber = invoice.InvoiceNumber.ToString(),
                    InvoiceDate = invoice.IssuedDate ?? DateTime.UtcNow,
                    ErrorType = item.ErrorType,
                    Reason = item.Reason
                });
            }
            var result = await _xmlService.Generate04SSXmlDocumentAsync(notification);
            var xmlDoc = result.XmlDoc;
            string fileName = $"04SS_{Guid.NewGuid()}.xml";
            string cloudUrl = await _xmlService.UploadXmlAsync(xmlDoc, fileName);

            // 4. Save to DB
            notification.XMLPath = cloudUrl;
            notification.MTDiep = result.MessageId;
            await _uow.ErrorNotificationRepository.CreateAsync(notification);
            await _uow.SaveChanges();

            return Result.Ok(new CreateErrorNotificationResponse
            {
                NotificationId = notification.InvoiceErrorNotificationID,
                NotificationNumber = notification.NotificationNumber,
                Status = "draft" 
            });
        }
        private Result ValidateInvoiceStatus(Invoice invoice, int errorType)
        {
            if (string.IsNullOrEmpty(invoice.TaxAuthorityCode))
            {
                return Result.Fail($"Hóa đơn hiện tại chưa được cấp Mã CQT (Trạng thái: {invoice.InvoiceStatusID}). Bạn không thể gửi thông báo sai sót. Hãy kiểm tra lại quy trình phát hành.");
            }

            switch (errorType)
            {
                case 1: 
                    if (invoice.InvoiceStatusID == 3) 
                    {
                        return Result.Fail("Hóa đơn này ĐÃ BỊ HỦY trước đó.");
                    }
                    break;
                case 2: 
                    break;

                default:
                    return Result.Fail($"Loại sai sót không hợp lệ (ErrorType: {errorType}). Chỉ chấp nhận 1, 2.");
            }
            return Result.Ok();
        }
    }
}
