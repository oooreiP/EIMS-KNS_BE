using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class NotificationType
    {
        [Key]
        public int TypeID { get; set; }
        [StringLength(255)]
        public string? TypeName { get; set; }

        // --- Navigation Properties ---
        [InverseProperty("Notifications")]
        public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}