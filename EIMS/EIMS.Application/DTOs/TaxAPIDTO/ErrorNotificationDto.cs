using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class ErrorNotificationDto
    {
        public int Id { get; set; }
        public string NotificationNumber { get; set; }
        public string NotificationType { get; set; }
        public string TaxAuthorityName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }     
        public int StatusCode { get; set; }     
        public string? MTDiep { get; set; }
        public string? XMLPath { get; set; }
        public string Place { get; set; }

        // Danh sách chi tiết
        public List<ErrorNotificationDetailDto> Details { get; set; }
    }
}
