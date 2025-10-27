using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Domain.Entities
{
    public class Notification
    {
        [Key]
        public int NotificationID { get; set; }
        public int UserID { get; set; }
        [ForeignKey("UserID")]
        [StringLength(255)]
        public string? Content { get; set; }
        public int NotificationStatusID { get; set; }
        [ForeignKey("NotificationStatusID")]
        public int NotificationTypeID { get; set; }
        [ForeignKey("NotificationTypeID")]

        public DateTime Time { get; set; }
        //Navigation
        [InverseProperty("NotificationStatus")]
        public virtual NotificationStatus NotificationStatus { get; set; }
        [InverseProperty("User")]
        public virtual User User { get; set; }
        [InverseProperty("NotificationType")]
        public virtual NotificationType NotificationType { get; set; }

    }
}