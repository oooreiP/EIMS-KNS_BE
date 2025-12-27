using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.File;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetInvoicePdfHandler : IRequestHandler<GetInvoicePdfQuery, Result<InvoicePdfDto>>
    {
        private readonly IPdfService _pdfService;
        private readonly IUnitOfWork _uow; // Dùng để lấy số hóa đơn làm tên file
        private readonly IFileStorageService _fileStorageService;
        public GetInvoicePdfHandler(IPdfService pdfService, IUnitOfWork uow, IFileStorageService fileStorageService)
        {
            _pdfService = pdfService;
            _uow = uow;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<InvoicePdfDto>> Handle(GetInvoicePdfQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
                if (invoice == null)
                    return Result.Fail("Không tìm thấy hóa đơn.");
                byte[] pdfBytes = await _pdfService.ConvertXmlToPdfAsync(request.InvoiceId, request.RootPath);
                string invoiceNumber = invoice.InvoiceNumber.ToString() ?? $"Draft_{invoice.InvoiceID}";
                string fileName = $"Invoice_{invoiceNumber}.pdf";
                using (var pdfStream = new MemoryStream(pdfBytes))
                {
                    var pdfUpload = await _fileStorageService.UploadFileAsync(pdfStream, fileName, "invoices/pdf");

                    if (pdfUpload.IsSuccess)
                    {
                        invoice.FilePath = pdfUpload.Value.Url;
                        await _uow.InvoicesRepository.UpdateAsync(invoice);
                        await _uow.SaveChanges();
                    }
                }
                return Result.Ok(new InvoicePdfDto
                {
                    FileContent = pdfBytes,
                    FileName = fileName
                });
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi tạo PDF: {ex.Message}");
            }
        }
    }
}

