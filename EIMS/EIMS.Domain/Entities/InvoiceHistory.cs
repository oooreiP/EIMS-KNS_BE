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

        public int InvoiceID { get; set; }
        [ForeignKey("InvoiceID")]

        public string? ActionType { get; set; } // Adjustment, Replacement, Cancellation

        public int? ReferenceInvoiceID { get; set; }
        [ForeignKey("ReferenceInvoiceID")]

        public DateTime Date { get; set; }

        public int PerformedBy { get; set; }
        [ForeignKey("PerformedBy")]

        //Navigations
        [InverseProperty("Performer")]
        public virtual User Performer { get; set; }
        [InverseProperty("ReferenceInvoice")]
        public virtual Invoice? ReferenceInvoice { get; set; }
        [InverseProperty("Invoice")]
        public virtual Invoice Invoice { get; set; }

    }
}