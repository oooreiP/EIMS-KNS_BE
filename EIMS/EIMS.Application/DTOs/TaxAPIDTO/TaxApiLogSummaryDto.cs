using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class TaxApiLogSummaryDto
    {
        public int TaxLogID { get; set; }
        public int? InvoiceID { get; set; }
        public long? InvoiceNumber { get; set; }
        public DateTime Timestamp { get; set; }
        public string TaxApiStatusName { get; set; } 
        public int TaxApiStatusID { get; set; } 
        public string MTDiep { get; set; }
        public string MTDiepPhanHoi { get; set; }
        public string MCCQT { get; set; }
        public string SoTBao { get; set; }
    }
}
