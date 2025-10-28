using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EIMS.Domain.Entities
{
    public class NotificationStatus
    {
        [Key]
        public int StatusID { get; set; }
        [StringLength(255)]
        public string? StatusName { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("NotificationStatus")]
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}