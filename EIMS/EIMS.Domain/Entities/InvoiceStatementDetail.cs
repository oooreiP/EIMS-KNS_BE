using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceStatementDetail
    {
        [Key]
        public int DetailID { get; set; }

        [ForeignKey("StatementID")]
        public int StatementID { get; set; }

        [ForeignKey("InvoiceID")]
        public int InvoiceID { get; set; }
        [Column(TypeName = "decimal(18, 2)")]
        public decimal OutstandingAmount { get; set; } // Snapshot of debt at this specific time
        // Navigation Properties
        [InverseProperty("StatementDetails")]
        public virtual InvoiceStatement Statement { get; set; }

        [InverseProperty("StatementDetails")]
        public virtual Invoice Invoice { get; set; }
    }
}