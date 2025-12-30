using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public int AuditID { get; set; }
        [ForeignKey("UserID")]
        public string? TableName { get; set; }
        public string? RecordId { get; set; }
        public string? Action { get; set; } // Insert/Update/Delete
        public string? OldValues { get; set; } // JSON
        public string? NewValues { get; set; } // JSON
        public string? UserId { get; set; }
        public DateTime Timestamp { get; set; }
        public string TraceId { get; set; } // [QUAN TRỌNG] Để link với bảng Activity

    }
}