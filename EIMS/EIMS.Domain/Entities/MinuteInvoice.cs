using EIMS.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class MinuteInvoice
    {
        [Key]
        public int MinutesInvoiceId { get; set; }
        [Required]
        public int? InvoiceId { get; set; }
        [ForeignKey(nameof(InvoiceId))]
        public Invoice? Invoice { get; set; }
        public string MinuteCode { get; set; } // Số biên bản (VD: BB-001)
        public MinutesType MinutesType { get; set; }
        public EMinuteStatus Status { get; set; }
        public string? Description { get; set; } // Lý do lập biên bản (VD: Sai tên, sai tiền...)
        public string FilePath { get; set; } = null!;
        public bool IsSellerSigned { get; set; } = false;
        public DateTime? SellerSignedAt { get; set; }
        public bool IsBuyerSigned { get; set; } = false;
        public DateTime? BuyerSignedAt { get; set; }
        public int CreatedBy { get; set; }
        [ForeignKey(nameof(CreatedBy))]
        public User? Creator { get; set; } // Navigation tới bảng User (giả sử bạn có class User)
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
