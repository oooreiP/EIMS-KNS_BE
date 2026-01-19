    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    namespace EIMS.Domain.Entities
    {
        public class Company
        {
            [Key]
            public int CompanyID { get; set; }
            [Required]
            [StringLength(255)]
            public string CompanyName { get; set; } = string.Empty;
            [Required]
            [StringLength(255)]
            public string Address { get; set; } = string.Empty;
            [Required]
            [StringLength(20)]
            public string TaxCode { get; set; } = string.Empty;
            [StringLength(20)]
            public string? ContactPhone { get; set; }
            [StringLength(20)]
            public string? AccountNumber { get; set; }
            [StringLength(255)]
            public string? BankName { get; set; }
            public byte[]? DigitalSignature { get; set; }
            public string? DigitalSignaturePassword { get; set; }
            // --- Navigation Properties ---
            [InverseProperty("Company")]
            public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
            [InverseProperty("Company")]
            public virtual ICollection<InvoiceRequest> InvoiceRequests { get; set; }

    }
}