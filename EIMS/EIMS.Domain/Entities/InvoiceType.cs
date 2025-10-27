using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceType
    {
        [Key]
        public int InvoiceTypeID { get; set; }

        [StringLength(1)]
        public string? Symbol { get; set; }

        [StringLength(255)]
        public string? TypeName { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("Serials")]
        public virtual ICollection<Serial> Serials { get; set; } = new List<Serial>();
    }
}