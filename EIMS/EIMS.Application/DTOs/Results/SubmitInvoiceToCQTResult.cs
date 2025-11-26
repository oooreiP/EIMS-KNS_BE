using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Results
{
    public class SubmitInvoiceToCQTResult
    {
        public string MTDiep { get; set; } = string.Empty;
        public string MLTDiep { get; set; } = string.Empty;
        public string SoTBao { get; set; } = string.Empty;
        public string MCCQT { get; set; } = string.Empty;
        public string Status { get; set; } = "00"; 
        public string Message { get; set; } = "CQT đã tiếp nhận hóa đơn";
        public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;
    }
}
