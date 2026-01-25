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
        public int? InvoiceID { get; set; }
        [Required]
        public string RequestPayload { get; set; } = string.Empty;
        public string? ResponsePayload { get; set; }
        public string? MTDiep { get; set; }
        public string? MTDiepPhanHoi { get; set; }
        public string? MCCQT { get; set; }
        public string? SoTBao { get; set; }
        [ForeignKey("TaxApiStatusID")]
        public int TaxApiStatusID { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        //Navigation
        [InverseProperty("TaxApiLogs")]
        public virtual Invoice Invoice { get; set; }
        [InverseProperty("TaxApiLogs")]
        public virtual TaxApiStatus TaxApiStatus { get; set; }

    }
}