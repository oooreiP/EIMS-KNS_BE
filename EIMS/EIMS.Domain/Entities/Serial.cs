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
        [ForeignKey("PrefixID")]
        public int PrefixID { get; set; }
        [ForeignKey("InvoiceTypeID")]
        public int InvoiceTypeID { get; set; }
        [StringLength(2)]
        public string? Year { get; set; }
        [StringLength(2)]
        public string? Tail { get; set; }
        [ForeignKey("SerialStatusID")]
        public int SerialStatusID { get; set; }
        public long CurrentInvoiceNumber { get; set; } = 1;
        // --- Navigation Properties ---
        [InverseProperty("Serial")]
        public virtual ICollection<InvoiceTemplate> InvoiceTemplates { get; set; } = new List<InvoiceTemplate>();
        [InverseProperty("Serials")]
        public virtual Prefix Prefix { get; set; }
        [InverseProperty("Serials")]
        public virtual InvoiceType InvoiceType { get; set; }
        [InverseProperty("Serials")]
        public virtual SerialStatus SerialStatus { get; set; }
    }
}