using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class Invoice
    {

        [Key]
        public int InvoiceID { get; set; }
        [ForeignKey("TemplateID")]

        public int TemplateID { get; set; }

        public long InvoiceNumber { get; set; }
        [ForeignKey("InvoiceStatusID")]

        public int InvoiceStatusID { get; set; }
        [ForeignKey("PaymentStatusID")]
        public int PaymentStatusID { get; set; }
        [ForeignKey("CompanyId")]

        public int? CompanyId { get; set; }
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

        public int? SalesID { get; set; }
        [ForeignKey("SalesID")]
        [StringLength(500)]
        public string? FilePath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- Navigation Properties ---
        [InverseProperty("Invoices")]
        public virtual InvoiceTemplate Template { get; set; }
        [InverseProperty("Invoices")]
        public virtual InvoiceStatus InvoiceStatus { get; set; }
        [InverseProperty("Invoices")]
        public virtual Company Company { get; set; }
        [InverseProperty("SalesInvoices")]
        public virtual User? Sales { get; set; }
        [InverseProperty("Invoices")]
        public virtual Customer Customer { get; set; }
        [InverseProperty("IssuedInvoices")]
        public virtual User Issuer { get; set; }
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        [InverseProperty("Invoice")]
        public virtual ICollection<TaxApiLog> TaxApiLogs { get; set; } = new List<TaxApiLog>();
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceHistory> HistoryEntries { get; set; } = new List<InvoiceHistory>();

        [InverseProperty("ReferenceInvoice")]
        public virtual ICollection<InvoiceHistory> ReferencedByHistory { get; set; } = new List<InvoiceHistory>();
        [InverseProperty("Invoices")]
        public virtual PaymentStatus PaymentStatus { get; set; }
    }
}