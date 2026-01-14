using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.TaxAPIDTO
{
    public class ErrorNotificationDetailDto
    {
        public int InvoiceId { get; set; }
        public string InvoiceSerial { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public int ErrorType { get; set; }      
        public string ErrorTypeName { get; set; }
        public string Reason { get; set; }
    }
}
