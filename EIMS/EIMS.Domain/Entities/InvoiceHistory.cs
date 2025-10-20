using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities;

public class InvoiceHistory
{
    [Key]
    public int HistoryId { get; set; }
    public int InvoiceId { get; set; }
    public ActionType ActionType { get; set; }
    public int? ReferenceInvoiceId { get; set; }
    public DateTime Date { get; set; }
    public int PerformedBy { get; set; }

    // Navigation
    public virtual Invoices Invoice { get; set; }
    public virtual Users PerformedUser { get; set; }
}