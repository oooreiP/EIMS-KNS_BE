using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class CompleteInvoiceSigningCommand : IRequest<Result>
    {
        // 1. ID của hóa đơn đang ký dở
        public int InvoiceId { get; set; }

        // 2. CHỮ KÝ SỐ (Dạng Base64)
        // Đây là kết quả sau khi User cắm USB -> Nhập PIN -> Bấm Ký
        public string SignatureBase64 { get; set; }

        // 3. PUBLIC KEY (Dạng Base64)
        // Để Server biết ai vừa ký và điền vào thẻ <X509Certificate>
        public string CertificateBase64 { get; set; }
    }
}
