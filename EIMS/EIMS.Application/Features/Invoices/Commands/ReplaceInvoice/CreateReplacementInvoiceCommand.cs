using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.ReplaceInvoice
{
    public class CreateReplacementInvoiceCommand : IRequest<Result<CreateInvoiceResponse>>
    {
        // --- [ĐẶC TRƯNG REPLACE] ---
        public int OriginalInvoiceId { get; set; }
        public string? Reason { get; set; } // Lý do thay thế do user nhập

        // --- [GIỐNG CREATE INVOICE - CHO PHÉP SỬA] ---
        public int? TemplateID { get; set; }
        public int? CustomerID { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public int CompanyID { get; set; } = 1;

        // Thông tin khách hàng (Cho phép sửa lại toàn bộ nếu sai)
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? Notes { get; set; } // Ghi chú thêm
        public string PaymentMethod { get; set; }

        // Danh sách hàng hóa (Làm lại bảng mới hoàn toàn)
        public List<InvoiceItemDto>? Items { get; set; }

        // Các số liệu tổng (Nếu FE tính sẵn thì gửi, ko thì BE tính)
        public decimal? Amount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? TotalAmount { get; set; }
    }
}
