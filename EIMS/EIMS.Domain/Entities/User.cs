using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities
{
    public class User
    {
        [Key]
        public int UserID { get; set; }

        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [StringLength(255)]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        [StringLength(20)]
        public string? PhoneNumber { get; set; }

        [ForeignKey("RoleID")]
        public int RoleID { get; set; }
        [ForeignKey("CustomerID")]
        public int? CustomerID { get; set; }
        public string? EvidenceStoragePath { get; set; } // URL or path to the evidence image
        public UserAccountStatus Status { get; set; }
        public bool IsPasswordChangeRequired { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        // --- Navigation Properties ---
        [InverseProperty("CreatedBy")]
        public virtual ICollection<InvoiceTemplate> CreatedTemplates { get; set; } = new List<InvoiceTemplate>();

        [InverseProperty("Issuer")]
        public virtual ICollection<Invoice> IssuedInvoices { get; set; } = new List<Invoice>();

        // [InverseProperty("Sales")]
        // public virtual ICollection<Invoice> SalesInvoices { get; set; } = new List<Invoice>();

        [InverseProperty("Creator")]
        public virtual ICollection<InvoiceStatement> CreatedStatements { get; set; } = new List<InvoiceStatement>();

        [InverseProperty("Performer")]
        public virtual ICollection<InvoiceHistory> HistoryActions { get; set; } = new List<InvoiceHistory>();

        [InverseProperty("User")]
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
        [InverseProperty("Users")]
        public virtual Role Role { get; set; }
        public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public virtual Customer? Customer { get; set; }
    }
}