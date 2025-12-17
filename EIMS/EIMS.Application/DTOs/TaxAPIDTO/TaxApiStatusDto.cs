using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class TaxApiStatusDto
    {
        public int TaxApiStatusID { get; set; }
        public string Code { get; set; }
        public string? StatusName { get; set; }
    }
}
