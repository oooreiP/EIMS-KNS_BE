using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class ErrorDetailDto
    {
        public int InvoiceId { get; set; }
        /// <summary>
        /// Tính chất sai sót: 
        /// 1: Hủy, 2: Điều chỉnh, 3: Thay thế, 4: Giải trình
        /// </summary>
        [Range(1, 2, ErrorMessage = "Loại sai sót (ErrorType) không hợp lệ. Chỉ chấp nhận: 1 (Thông báo), 2 (Giải trình).")]
        public int ErrorType { get; set; }

        /// <summary>
        /// Lý do sai sót (Mô tả chi tiết lỗi)
        /// </summary>
        [Required(ErrorMessage = "Lý do sai sót là bắt buộc.")]
        [MinLength(10, ErrorMessage = "Lý do sai sót phải có tối thiểu 10 ký tự.")]
        public string Reason { get; set; } = string.Empty;
    }
}
