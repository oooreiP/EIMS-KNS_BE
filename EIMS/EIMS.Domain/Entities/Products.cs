using System.ComponentModel.DataAnnotations;

namespace EIMS.Domain.Entities;

public class Products
{
    [Key]
    public int ProductId { get; set; }
    public string Name { get; set; }
    public string Type { get; set; }
    public string Description { get; set; }
    public decimal BasePrice { get; set; }
    public string BillingPeriod { get; set; }
    public bool IsActive { get; set; }

    // Navigation
    public virtual ICollection<Invoices> Invoices { get; set; }
}