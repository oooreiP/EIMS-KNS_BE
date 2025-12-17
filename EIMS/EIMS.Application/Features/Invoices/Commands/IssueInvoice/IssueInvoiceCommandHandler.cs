using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
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
        private readonly IEmailService _emailService;

        public IssueInvoiceCommandHandler(IUnitOfWork uow, IInvoiceXMLService xmlService, IEmailService emailService)
        {
            _uow = uow;
            _xmlService = xmlService;
            _emailService = emailService;
        }

        public async Task<Result> Handle(IssueInvoiceCommand request, CancellationToken cancellationToken)
        {
            // 1. Lấy thông tin hóa đơn từ DB
            var invoice = await _uow.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice.OriginalInvoiceID != null)
            {
                var original = await _uow.InvoicesRepository.GetAllQueryable()
               .Include(x => x.InvoiceItems)
               .Include(x => x.Customer)
               .Include(x => x.Company)
               .OrderByDescending(x => x.InvoiceID)
               .FirstOrDefaultAsync(x => x.InvoiceID == invoice.OriginalInvoiceID);
                if(original.InvoiceType == 3) original.InvoiceStatusID = 5;
                else if(original.InvoiceType == 2) original.InvoiceStatusID = 4;
            }
            if (invoice == null) return Result.Fail("Không tìm thấy hóa đơn.");
            bool hasSignature = !string.IsNullOrEmpty(invoice.DigitalSignature);
            bool hasMccqt = !string.IsNullOrEmpty(invoice.TaxAuthorityCode);
            if (!hasSignature)
                return Result.Fail("Hóa đơn chưa có chữ ký số.");

            if (!hasMccqt)
                return Result.Fail("Hóa đơn chưa được cấp Mã CQT.");
            if (invoice.InvoiceStatusID != 2) // Nếu chưa phải là Issued
            {
                invoice.InvoiceStatusID = 2;
                invoice.IssuedDate = DateTime.UtcNow;
                invoice.IssuerID = request.IssuerId;
                await _uow.InvoicesRepository.UpdateAsync(invoice);
                await _uow.SaveChanges();
            }
                await _emailService.SendStatusUpdateNotificationAsync(invoice.InvoiceID, 2);
            return Result.Ok();
        }
    }
}
