using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class StatementStatus
    {
        [Key]
        public int StatusID { get; set; }
        [Required]
        [StringLength(20)]
        public string StatusName { get; set; } = string.Empty;
        // --- Navigation Properties ---
        [InverseProperty("InvoiceStatements")]
        public virtual ICollection<InvoiceStatement> InvoiceStatements { get; set; } = new List<InvoiceStatement>();
    }
}