using EIMS.Application.Commons.Helpers;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class SendErrorNotificationHandler : IRequestHandler<SendErrorNotificationCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileService;
        private readonly IInvoiceXMLService _invoiceXmlService;
        private readonly ITaxApiClient _taxClient; 

        public SendErrorNotificationHandler(IUnitOfWork uow, IFileStorageService fileService, ITaxApiClient taxClient, IInvoiceXMLService invoiceXmlService)
        {
            _uow = uow;
            _fileService = fileService;
            _taxClient = taxClient;
            _invoiceXmlService = invoiceXmlService;
        }

            public async Task<Result<string>> Handle(SendErrorNotificationCommand request, CancellationToken cancellationToken)
            {
               var noti = await _uow.ErrorNotificationRepository.GetByIdAsync(request.NotificationID, "Details.Invoice");
                if (noti == null) return Result.Fail("Notification not found");
                if (noti.Status != 1) return Result.Fail("Chỉ gửi được tờ khai ở trạng thái Nháp/Lỗi.");

                try
                {
                    var rawXml = await _invoiceXmlService.DownloadStringAsync(noti.XMLPath);
                    var certResult = await _invoiceXmlService.GetCertificateAsync(1);
                    if (certResult.IsFailed) return Result.Fail(certResult.Errors);
                    var cert = certResult.Value;
                    var signedXml = XmlHelpers.SignElectronicDocument(rawXml, cert, false);
                    string signedXmlPayload = signedXml.SignedXml;
                    var taxResponse = await _taxClient.SendTaxMessageAsync(signedXmlPayload, noti.MTDiep);
                    string apiStatusCode = taxResponse.MLTDiep == "301" ? "KQ01" :
                                          taxResponse.MLTDiep == "204" ? "TBxx" :
                                          "TB01";
                var responseLog = new TaxApiLog
                {
                    RequestPayload = signedXmlPayload,
                    ResponsePayload = taxResponse.RawResponse,
                    MTDiep = taxResponse.MTDiep,
                    SoTBao = taxResponse.SoTBao,
                    MCCQT = taxResponse.MCCQT,
                    TaxApiStatusID = XmlHelpers.MapApiCodeToStatusId(apiStatusCode),
                    Timestamp = DateTime.UtcNow
                };              
                await _uow.TaxApiLogRepository.CreateAsync(responseLog);
                await _uow.SaveChanges();
                string xmlContent = taxResponse.RawResponse;
                byte[] byteArray = System.Text.Encoding.UTF8.GetBytes(xmlContent);
                byte[] SignedbyteArray = System.Text.Encoding.UTF8.GetBytes(signedXmlPayload);
                using (var stream = new MemoryStream(byteArray))
                {
                    string fileName = $"TaxResponse_{taxResponse.MTDiep}_{DateTime.Now:yyyyMMddHHmmss}.xml";
                    var uploadResult = await _fileService.UploadFileAsync(stream, fileName, "tax-responses"); 

                    if (uploadResult.IsSuccess)
                    {
                        noti.TaxResponsePath = uploadResult.Value.Url;                       
                    }
                }
                using (var stream = new MemoryStream(SignedbyteArray))
                {
                    string fileName = $"04SS_{Guid.NewGuid()}.xml";
                    var uploadResult = await _fileService.UploadFileAsync(stream, fileName, "tax-responses");

                    if (uploadResult.IsSuccess)
                    {
                        noti.XMLPath = uploadResult.Value.Url;
                    }
                }
                if (taxResponse.IsSuccess)
                {
                    noti.Status = 4; 
                    noti.SignedData = signedXml.SignatureValue;
                    if (noti.Details != null)
                    {
                        foreach (var detail in noti.Details)
                        {
                            var invoice = detail.Invoice;
                            if (invoice == null) continue; 
                            switch (detail.ErrorType)
                            {
                                case 1: 
                                    invoice.InvoiceStatusID = 3; 
                                    break;

                                case 2:
                                    invoice.InvoiceStatusID = 10; 
                                    break;

                                case 3: 
                                    invoice.InvoiceStatusID = 11; // Replacement_In_Progress
                                    break;

                                case 4: 
                                    break;
                            }
                            await _uow.ErrorNotificationRepository.UpdateAsync(noti);
                            await _uow.InvoicesRepository.UpdateAsync(invoice);
                        }
                    }
                    await _uow.SaveChanges();

                    return Result.Ok(taxResponse.MTDiep);
                }
                else
                {
                    noti.Status = 5; 
                    await _uow.SaveChanges();

                    return Result.Fail($"Gửi thất bại: {taxResponse.RawResponse} - {taxResponse.SoTBao}");
                }
            }
            catch (Exception ex)
                {
                    return Result.Fail($"Lỗi hệ thống: {ex.Message}");
                }
            }
        }
    }
