using System.ComponentModel.DataAnnotations;

namespace EIMS.Domain.Entities;

public class Customers
{
    [Key]
    public int CustomerId { get; set; }
    public string Name { get; set; }
    public string TaxCode { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }

    // Navigation
    public virtual ICollection<Invoices> Invoices { get; set; }
    public virtual ICollection<InvoiceStatements> InvoiceStatements { get; set; }  
}