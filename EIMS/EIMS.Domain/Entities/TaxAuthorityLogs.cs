using System.ComponentModel.DataAnnotations;

namespace EIMS.Domain.Entities;

public class TaxAuthorityLogs
{
    [Key]
    public int LogId { get; set; }
    public int InvoiceId { get; set; }
    public DateTime SentDate { get; set; }
    public string ResponseCode { get; set; }
    public string ResponseMessage { get; set; }
    public string Status { get; set; }

    //navigation
    public Invoices Invoice { get; set; }

}