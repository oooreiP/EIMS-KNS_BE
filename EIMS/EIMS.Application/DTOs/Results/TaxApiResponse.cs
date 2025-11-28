using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Results
{
    public class TaxApiResponse
    {
        public bool IsSuccess { get; set; }
        public string MTDiep { get; set; } = string.Empty;
        public string MLTDiep { get; set; } = string.Empty;
        public string? SoTBao { get; set; }
        public string? MCCQT { get; set; }
        public string RawResponse { get; set; } = string.Empty;
    }
}
