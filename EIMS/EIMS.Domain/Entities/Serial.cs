using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class Serial
    {
        [Key]
        public int SerialID { get; set; }
        public int PrefixID { get; set; }
        [ForeignKey("PrefixID")]
        public int InvoiceTypeID { get; set; }
        [ForeignKey("InvoiceTypeID")]
        [StringLength(2)]
        public string? Year { get; set; }
        [StringLength(2)]
        public string? Tail { get; set; }
        public int SerialStatusID { get; set; }
        [ForeignKey("SerialStatusID")]
        // --- Navigation Properties ---
        [InverseProperty("InvoiceTemplates")]
        public virtual ICollection<InvoiceTemplate> InvoiceTemplates { get; set; } = new List<InvoiceTemplate>();
        [InverseProperty("Prefix")]
        public virtual Prefix Prefix { get; set; }
        [InverseProperty("InvoiceType")]
        public virtual InvoiceType InvoiceType { get; set; }
        [InverseProperty("SerialStatus")]
        public virtual SerialStatus SerialStatus { get; set; }
    }
}