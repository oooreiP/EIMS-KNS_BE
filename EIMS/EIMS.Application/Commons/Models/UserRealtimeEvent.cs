using System;

namespace EIMS.Application.Commons.Models
{
    public class UserRealtimeEvent
    {
        public int UserId { get; set; }
        public string ChangeType { get; set; } = "Updated";
        public string? RoleName { get; set; }
        public bool? IsActive { get; set; }
        public string[]? Roles { get; set; }
        public DateTime OccurredAt { get; set; } = DateTime.UtcNow;
    }
}
