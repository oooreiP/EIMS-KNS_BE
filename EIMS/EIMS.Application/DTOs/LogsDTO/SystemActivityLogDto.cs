using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.LogsDTO
{
    public class SystemActivityLogDto
    {
        public int LogId { get; set; }
        public string UserId { get; set; }
        public string ActionName { get; set; }
        public string Description { get; set; }
        public string IpAddress { get; set; }
        public string Status { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
