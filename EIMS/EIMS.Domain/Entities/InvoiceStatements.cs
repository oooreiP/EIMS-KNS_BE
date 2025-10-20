using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities;

public class InvoiceStatements
{
    [Key]
    public int StatementId { get; set; }
    public string StatementCode { get; set; }
    public DateTime StatementDate { get; set; }
    public int? CreatedBy { get; set; }
    public int? CustomerId { get; set; }
    public int TotalInvoices { get; set; }
    public decimal TotalAmount { get; set; }
    public Status Status { get; set; }
    public string Notes { get; set; }

    // Navigation
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual Users Creator { get; set; }
    public virtual Customers Customer { get; set; }

}