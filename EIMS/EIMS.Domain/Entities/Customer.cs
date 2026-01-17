using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required]
        [StringLength(255)]
        public string CustomerName { get; set; } = string.Empty;
        [StringLength(20)]
        public string TaxCode { get; set; } = string.Empty;
        [Required]
        [StringLength(500)]
        public string Address { get; set; } = string.Empty;
        [Required]
        [StringLength(255)]
        public string ContactEmail { get; set; } = string.Empty;
        [StringLength(100)]
        public string? ContactPerson { get; set; }
        [StringLength(20)]
        public string? ContactPhone { get; set; }
        public bool IsActive { get; set; } = true;
        // --- Navigation Properties ---
        [JsonIgnore]
        [InverseProperty("Customer")]
        public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
        [InverseProperty("Customer")]
        public virtual ICollection<InvoiceStatement> Statements { get; set; } = new List<InvoiceStatement>();
        [InverseProperty("Customer")]
        public virtual ICollection<User> Users { get; set; } = new List<User>();
        [InverseProperty("Customer")]
        public virtual ICollection<InvoiceRequest> InvoiceRequests { get; set; }
    }
}