using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.SerialStatus
{
    public class SerialStatusResponse
    {
        public int SerialStatusID { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public string StatusName { get; set; } = string.Empty;
    }
}