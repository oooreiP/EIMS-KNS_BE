using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EIMS.Application.DTOs.Invoices
{
    public class InvoiceDTO
    {
        [Key]
        public int InvoiceID { get; set; }
        [ForeignKey("TemplateID")]

        public int TemplateID { get; set; }

        public long InvoiceNumber { get; set; }
        [ForeignKey("InvoiceStatusID")]

        public int InvoiceStatusID { get; set; }
        [ForeignKey("CompanyId")]

        public int CompanyId { get; set; }
        [ForeignKey("CustomerID")]

        public int CustomerID { get; set; }
        [ForeignKey("IssuerID")]

        public int IssuerID { get; set; }

        public DateTime SignDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal SubtotalAmount { get; set; }

        [Column(TypeName = "decimal(5, 2)")]
        public decimal VATRate { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal VATAmount { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public string PaymentMethod { get; set; }
        [Required]
        [StringLength(1000)]
        public string TotalAmountInWords { get; set; } = string.Empty;

        public string? DigitalSignature { get; set; }

        [StringLength(100)]
        public string? TaxAuthorityCode { get; set; }

        [StringLength(500)]
        public string? QRCodeData { get; set; }

        [StringLength(2000)]
        public string? Notes { get; set; }

        // public int? SalesID { get; set; }
        // [ForeignKey("SalesID")]
        [StringLength(500)]
        public string? FilePath { get; set; }
        public string? XMLPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public List<InvoiceItemDto> InvoiceItems { get; set; } = new List<InvoiceItemDto>();
    }
}
