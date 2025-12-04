using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.IssueInvoice
{
    public class IssueInvoiceCommandHandler : IRequestHandler<IssueInvoiceCommand, Result>
    {
        private readonly IUnitOfWork _uow;
        private readonly IInvoiceXMLService _xmlService;

        public IssueInvoiceCommandHandler(IUnitOfWork uow, IInvoiceXMLService xmlService)
        {
            _uow = uow;
            _xmlService = xmlService;
        }

        public async Task<Result> Handle(IssueInvoiceCommand request, CancellationToken cancellationToken)
        {
            // 1. Lấy thông tin hóa đơn từ DB
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null) return Result.Fail("Không tìm thấy hóa đơn.");

            // 2. KIỂM TRA ĐIỀU KIỆN TRONG DB (Nhanh và Hiệu quả)
            bool hasSignature = !string.IsNullOrEmpty(invoice.DigitalSignature);
            bool hasMccqt = !string.IsNullOrEmpty(invoice.TaxAuthorityCode);

            if (!hasSignature)
                return Result.Fail("Hóa đơn chưa có chữ ký số.");

            if (!hasMccqt)
                return Result.Fail("Hóa đơn chưa được cấp Mã CQT.");

            // 3. Nếu đủ điều kiện -> CHUYỂN TRẠNG THÁI PHÁT HÀNH
            // (Lúc này không cần thao tác file nữa vì file trên Cloud đã được cập nhật ở các bước trước rồi)

            if (invoice.InvoiceStatusID != 6) // Nếu chưa phải là Issued
            {
                invoice.InvoiceStatusID = 6; // Issued
                invoice.IssuedDate = DateTime.Now;

                await _uow.InvoicesRepository.UpdateAsync(invoice);
                await _uow.SaveChanges();
            }

            return Result.Ok();
        }
    }
}
