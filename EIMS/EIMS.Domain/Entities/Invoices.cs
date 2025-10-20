using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities;

public class Invoices
{
    [Key]
    public int Id { get; set; }
    public string InvoiceNumber { get; set; }
    public int TemplateId { get; set; }
    public int CustomerId { get; set; }
    public int ProductId { get; set; }
    public int StatementId { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime IssuedDate { get; set; }
    public DateTime SignedDate { get; set; }
    public decimal Amount { get; set; }
    public decimal TaxAmount { get; set; }
    public decimal TotalAmount { get; set; }
    public int SignedBy { get; set; }
    public Status Status { get; set; }
    public PaymentStatus PaymentStatus { get; set; }
    public TaxAuthorityStatus TaxAuthorityStatus { get; set; }

    //Navigation
    public virtual InvoiceTemplates Template { get; set; }
    public virtual Customers Customer { get; set; }
    public virtual Products Products { get; set; }
    public virtual InvoiceStatements Statements { get; set; }
    public virtual Users SignedUser { get; set; }
    public virtual ICollection<InvoiceHistory> InvoiceHistories { get; set; }
    public virtual ICollection<TaxAuthorityLogs> TaxAuthorityLogs { get; set; }

}