using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities;

public class Users
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public Roles Role { get; set; }
    public string PasswordHash { get; set; }
    public Status Status { get; set; }

    // Navigation
    public ICollection<AuditLogs> AuditLogs { get; set; }
    public ICollection<InvoiceStatements> CreatedStatements { get; set; } 
    public ICollection<Invoices> SignedInvoices { get; set; } 
    public ICollection<InvoiceHistory> PerformedHistories { get; set; } 
}