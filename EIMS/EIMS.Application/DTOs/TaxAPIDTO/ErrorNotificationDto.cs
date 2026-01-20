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
        public int NotificationTypeCode { get; set; }
        public string TaxAuthorityName { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Status { get; set; }     
        public int StatusCode { get; set; }     
        public int? CreatedBy { get; set; }     
        public string? MTDiep { get; set; }
        public string? XMLPath { get; set; }
        public string? TaxResponsePath { get; set; }
        public string Place { get; set; }
        public List<ErrorNotificationDetailDto> Details { get; set; }
        public string? InvoiceSerial { get; set; }      // Ký hiệu
        public string? InvoiceNumber { get; set; }      // Số hóa đơn
        public DateTime? InvoiceDate { get; set; }      // Ngày hóa đơn
        public string? CustomerName { get; set; }       // Tên khách hàng
        public decimal? TotalAmount { get; set; }       // Tổng tiền
    }
}
