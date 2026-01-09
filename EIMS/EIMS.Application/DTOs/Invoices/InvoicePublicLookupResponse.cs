using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Invoices
{
    public class InvoicePublicLookupResponse
    {
        public string InvoiceNumber { get; set; } // Số hóa đơn
        public string SerialNumber { get; set; }  // Ký hiệu
        public DateTime IssueDate { get; set; }   // Ngày lập
        public string SellerName { get; set; }    // Tên người bán
        public string BuyerName { get; set; }     // Tên người mua
        public decimal TotalAmount { get; set; }  // Tổng tiền
        public string Status { get; set; }        // Trạng thái (Text)
        public string PdfUrl { get; set; }        // Link PDF (Signed URL)
    }
}