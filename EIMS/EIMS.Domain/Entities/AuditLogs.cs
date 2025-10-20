using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Domain.Entities;

public class AuditLogs
{
    [Key]
    public int AuditId { get; set; }
    public int? UserId { get; set; }
    public Act Action { get; set; }
    public DateTime Timestamp { get; set; }
    public string Details { get; set; }

    //navigation
    public virtual Users User { get; set; }

}