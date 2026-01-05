using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs
{
    public class InvoiceItemDto
    {
        public int InvoiceItemID { get; set; }
        public int InvoiceID { get; set; }
        public string InvoiceNumber { get; set; } // [Tiện ích] Để biết item này thuộc hóa đơn số mấy
        public int ProductId { get; set; }
        public string ProductName { get; set; }   // [Tiện ích] Tên sản phẩm
        public string? Unit { get; set; }          // [Tiện ích] Đơn vị tính
        public double Quantity { get; set; }     // Convert double -> decimal cho đồng bộ
        public decimal? UnitPrice { get; set; }
        public decimal? Amount { get; set; }
        public decimal? VATAmount { get; set; }
        public bool IsAdjustmentItem { get; set; }
    }
}
