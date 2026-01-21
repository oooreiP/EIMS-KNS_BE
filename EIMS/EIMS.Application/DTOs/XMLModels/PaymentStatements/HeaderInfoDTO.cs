using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.XMLModels.PaymentStatements
{
    public class HeaderInfoDTO
    {
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
        public string StatementMonth { get; set; } 
        public string StatementDate { get; set; }  
        public string AccountantName { get; set; }
        public string ContactEmail { get; set; }
    }
}
