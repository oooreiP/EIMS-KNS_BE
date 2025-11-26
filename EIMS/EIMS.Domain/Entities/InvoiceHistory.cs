using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class InvoiceHistory
    {
        [Key]
        public int HistoryID { get; set; }
        [ForeignKey("InvoiceID")]

        public int InvoiceID { get; set; }

        public string? ActionType { get; set; } // Adjustment, Replacement, Cancellation

        public int? ReferenceInvoiceID { get; set; }
        [ForeignKey("ReferenceInvoiceID")]

        public DateTime Date { get; set; }

        [ForeignKey("PerformedBy")]
        public int? PerformedBy { get; set; }
        
        //Navigations
        [InverseProperty("HistoryActions")]
        public virtual User? Performer { get; set; }
        [InverseProperty("ReferencedByHistory")]
        public virtual Invoice? ReferenceInvoice { get; set; }
        [InverseProperty("HistoryEntries")]
        public virtual Invoice? Invoice { get; set; }

    }
}