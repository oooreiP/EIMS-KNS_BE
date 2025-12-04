using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class TaxApiLogDetailDto : TaxApiLogSummaryDto
    {
        public string RequestPayload { get; set; }
        public string ResponsePayload { get; set; }
    }
}
