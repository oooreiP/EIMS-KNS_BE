using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class CreateErrorNotificationResponse
    {
        public int NotificationId { get; set; }
        public string NotificationNumber { get; set; }
        public string Status { get; set; }
    }
}
