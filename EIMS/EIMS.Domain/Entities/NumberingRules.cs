using System.ComponentModel.DataAnnotations;

namespace EIMS.Domain.Entities;

public class NumberingRules
{
    [Key]
    public int NumberingRuleId { get; set; }
    public string Prefix { get; set; }
    public int Sequence { get; set; }
    public string ResetPolicy { get; set; }
    public int CurrentNumber { get; set; }

    // Navigation
    public ICollection<InvoiceTemplates> InvoiceTemplates { get; set; }
}