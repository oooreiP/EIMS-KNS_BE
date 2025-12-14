using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.SignInvoice
{
    public class SignInvoiceCommand : IRequest<Result>
    {
        // 1. BẮT BUỘC: ID của hóa đơn cần ký
        public int InvoiceId { get; set; }
        // 2. TÙY CHỌN: Nếu hệ thống quản lý nhiều Chứng thư số (Ví dụ: Nhiều chi nhánh)
        // Bạn cần biết dùng chữ ký nào để ký.
        public string? CertificateSerial { get; set; }
        // 3. TÙY CHỌN: Mã PIN (Nếu chứng thư số yêu cầu mở khóa mỗi lần ký)
        // Lưu ý: Chỉ dùng nếu lưu PFX có password hoặc Token cắm tại server cần PIN
        public string? SecretPin { get; set; }
    }
}
