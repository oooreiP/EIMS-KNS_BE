using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Products
{
    public class CreateProductRequest
    {
        public string Code { get; set; } = string.Empty;          // Mã hàng hóa, VD: HH0001
        public string Name { get; set; } = string.Empty;          // Tên hàng hóa
        public int CategoryID { get; set; }                       // Nhóm HHDV (liên kết bảng Category)
        public string? Unit { get; set; }                         // Đơn vị tính (VD: Lít, Chiếc, Gói)
        public decimal BasePrice { get; set; }                    // Giá bán
        public decimal? VATRate { get; set; }                     // Thuế GTGT (%)
        public string? Description { get; set; }                  // Mô tả hàng hóa
        public bool? IsActive { get; set; } = true;               // Trạng thái hoạt động
    }
}
