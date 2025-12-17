using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class GetHashToSignCommandHandler : IRequestHandler<GetHashToSignCommand, Result<string>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _invoiceXmlService;

        public GetHashToSignCommandHandler(IUnitOfWork uow, IInvoiceXMLService invoiceXmlService)
        {
            _uow = uow;
            _invoiceXmlService = invoiceXmlService;
        }

        public async Task<Result<string>> Handle(GetHashToSignCommand request, CancellationToken cancellationToken)
        {
            // 1. Kiểm tra hóa đơn
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) return Result.Fail("Không tìm thấy hóa đơn.");
            if (string.IsNullOrEmpty(invoice.XMLPath)) return Result.Fail("Hóa đơn chưa có file XML gốc.");

            // 2. Tải XML gốc (QUAN TRỌNG: Phải giữ nguyên khoảng trắng)
            // Giả sử _invoiceXmlService.LoadXmlFromUrlAsync trả về XmlDocument đã set PreserveWhitespace = true
            // Nếu không, ta tự load thủ công ở đây để đảm bảo an toàn:
            var xmlContent = await _invoiceXmlService.DownloadStringAsync(invoice.XMLPath);

            var xmlDoc = new XmlDocument();
            xmlDoc.PreserveWhitespace = true; // <--- KHÔNG ĐƯỢC QUÊN
            xmlDoc.LoadXml(xmlContent);

            // 3. Tính toán Hash (Digest)
            // Hàm này sẽ thực hiện Canonicalization (C14N) rồi mới Hash
            try
            {
                string hashBase64 = XmlHelpers.CalculateDigest(xmlDoc);
                return Result.Ok(hashBase64);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi khi tạo Hash ký số: {ex.Message}");
            }
        }
    }
}
