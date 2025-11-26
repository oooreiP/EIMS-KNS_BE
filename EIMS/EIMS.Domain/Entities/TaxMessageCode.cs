using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    [Table("tax_message_codes")]
    public class TaxMessageCode
    {
        [Key]
        [Column("stt")]
        public int Id { get; set; }  // SERIAL PRIMARY KEY

        [Required]
        [Column("message_code")]
        [StringLength(5)]
        public string MessageCode { get; set; } = string.Empty;   //Mã loại thông điệp (MLTDiep): Sử dụng kiểu VARCHAR để lưu trữ cả số dương (100, 201) và số âm (-1, -2)

        [Required]
        [Column("message_name")]
        [StringLength(255)]
        public string MessageName { get; set; } = string.Empty;   // Tên thông điệp: Mô tả ngắn gọn

        [Column("description")]
        public string? Description { get; set; }                  // Mô tả chi tiết

        [Required]
        [Column("category")]
        [StringLength(100)]
        public string Category { get; set; } = string.Empty;      // Phân loại nhóm thông điệp (ví dụ: 'Đăng ký', 'Hóa đơn', 'Sai sót')

        [Required]
        [Column("flow_type")]
        public int FlowType { get; set; }   // Mã loại thông điệp là GỬI đi (1) hay PHẢN HỒI (2)
    }
}
