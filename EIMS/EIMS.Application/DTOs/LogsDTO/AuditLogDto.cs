using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.LogsDTO
{
    public class AuditLogDto
    {
        public int AuditID { get; set; }
        public string TraceId { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; } 
        public string Action { get; set; }
        public string TableName { get; set; }
        public string RecordId { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
