using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class AuditLog
    {
        [Key]
        public int AuditID { get; set; }
        public int? UserID { get; set; }
        [ForeignKey("UserID")]
        public string? Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Details { get; set; }
        //Navigation
        [InverseProperty("User")]
        public virtual User? User { get; set; }

    }
}