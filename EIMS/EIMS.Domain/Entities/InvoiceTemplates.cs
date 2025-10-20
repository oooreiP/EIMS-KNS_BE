using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities;

public class InvoiceTemplates
{
    [Key]
    public int TemplateId { get; set; }
    public string Name { get; set; }
    public InvoiceType Type { get; set; }
    public string InvoiceTitle { get; set; }
    public string InvoiceForm { get; set; }
    public string InvoiceSerial { get; set; }
    public string LogoPath { get; set; }
    public string LayoutConfig { get; set; }
    public bool ShowTaxCode { get; set; }
    public bool ShowBuyerInfo { get; set; }
    public string LineItemFields { get; set; }
    public string CurrencySymbol { get; set; }
    public string Language { get; set; }
    public string Notes { get; set; }
    public bool HasSignatureArea { get; set; }
    public int? NumberingRuleId { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual NumberingRules NumberingRule { get; set; }

}