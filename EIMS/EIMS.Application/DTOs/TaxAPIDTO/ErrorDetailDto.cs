using System;
using System.Collections.Generic;
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
        public int ErrorType { get; set; }

        /// <summary>
        /// Lý do sai sót (Mô tả chi tiết lỗi)
        /// </summary>
        public string Reason { get; set; } = string.Empty;
    }
}
