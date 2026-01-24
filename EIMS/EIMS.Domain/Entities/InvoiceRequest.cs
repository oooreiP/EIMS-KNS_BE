using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities
{
    public class InvoiceRequest
    {
        [Key]
        public int RequestID { get; set; }
        [ForeignKey("RequestStatusID")]

        public int RequestStatusID { get; set; }
        [ForeignKey("CompanyID")]

        public int? CompanyID { get; set; }
        [ForeignKey("CustomerID")]
        public int CustomerID { get; set; }
        public int? SaleID { get; set; }
        [ForeignKey("CreatedInvoiceID")]
        public int? CreatedInvoiceID { get; set; }
        public string? PaymentMethod { get; set; }
        public EInvoiceCustomerType InvoiceCustomerType { get; set; } = EInvoiceCustomerType.Business;

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

        [StringLength(2000)]
        public string? Notes { get; set; }
        public string? EvidenceFilePath { get; set; }
        public int MinRows { get; set; } = 5;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string InvoiceCustomerName { get; set; }
        public string InvoiceCustomerAddress { get; set; }
        public string InvoiceCustomerTaxCode { get; set; }

        // --- Navigation Properties ---
        [JsonIgnore]
        [InverseProperty("InvoiceRequests")]
        public virtual InvoiceRequestStatus RequestStatus { get; set; }
        [JsonIgnore]
        [InverseProperty("InvoiceRequests")]
        public virtual Company Company { get; set; }
        [JsonIgnore]
        [ForeignKey("SaleID")]
        [InverseProperty("InvoiceRequests")]
        public virtual User? Sales { get; set; }
        [JsonIgnore]
        [InverseProperty("InvoiceRequests")]
        public virtual Customer Customer { get; set; }
        [JsonIgnore]
        [InverseProperty("InvoiceRequests")]
        public virtual ICollection<InvoiceRequestItem> InvoiceRequestItems { get; set; } = new List<InvoiceRequestItem>();
        [ForeignKey("CreatedInvoiceID")]
        public virtual Invoice? CreatedInvoice { get; set; }
    }
}

