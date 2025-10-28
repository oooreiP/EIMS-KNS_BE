using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class TaxApiLog
    {
        [Key]
        public int TaxLogID { get; set; }
        [ForeignKey("InvoiceID")]
        public int InvoiceID { get; set; }
        [Required]
        public string RequestPayload { get; set; } = string.Empty;
        public string? ResponsePayload { get; set; }
        public int TaxApiStatusID { get; set; }
        [ForeignKey("TaxApiStatusID")]

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        //Navigation
        [InverseProperty("TaxApiLogs")]
        public virtual Invoice Invoice { get; set; }
        [InverseProperty("TaxApiLogs")]
        public virtual TaxApiStatus TaxApiStatus { get; set; }

    }
}