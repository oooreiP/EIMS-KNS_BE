using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
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

        public int? IssuerID { get; set; }
        public int InvoiceType { get; set; } = 1;
        public int? OriginalInvoiceID { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? IssuedDate { get; set; }
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
        [StringLength(500)]
        public string? FilePath { get; set; }
        [StringLength(500)]
        public string? XMLPath { get; set; }
        public int MinRows { get; set; } = 5;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AdjustmentReason { get; set; }

        // --- Navigation Properties ---
        [JsonIgnore]
        [InverseProperty("Invoices")]
        public virtual InvoiceTemplate Template { get; set; }
        [JsonIgnore]
        [InverseProperty("Invoices")]
        public virtual InvoiceStatus InvoiceStatus { get; set; }
        [JsonIgnore]
        [InverseProperty("Invoices")]
        public virtual Company Company { get; set; }
        // [JsonIgnore]
        // [InverseProperty("SalesInvoices")]
        //public virtual User? Sales { get; set; }
        [JsonIgnore]
        [InverseProperty("Invoices")]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        [InverseProperty("IssuedInvoices")]
        public virtual User? Issuer { get; set; }
        [JsonIgnore]
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
        [JsonIgnore]
        [InverseProperty("Invoice")]
        public virtual ICollection<TaxApiLog> TaxApiLogs { get; set; } = new List<TaxApiLog>();
        [JsonIgnore]
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceHistory> HistoryEntries { get; set; } = new List<InvoiceHistory>();
        [JsonIgnore]
        [InverseProperty("ReferenceInvoice")]
        public virtual ICollection<InvoiceHistory> ReferencedByHistory { get; set; } = new List<InvoiceHistory>();
        [ForeignKey("OriginalInvoiceID")]
        public virtual Invoice? OriginalInvoice { get; set; }
        [InverseProperty("Invoices")]
        public virtual PaymentStatus PaymentStatus { get; set; }
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoiceStatementDetail> StatementDetails { get; set; } = new List<InvoiceStatementDetail>();
        [InverseProperty("Invoice")]
        public virtual ICollection<InvoicePayment> Payments { get; set; } = new List<InvoicePayment>();

        // 2. Add the Calculation Logic
        [NotMapped]
        public decimal PaidAmount => Payments?.Sum(p => p.AmountPaid) ?? 0;

        [NotMapped]
        public decimal RemainingAmount => TotalAmount - PaidAmount;
    }
}