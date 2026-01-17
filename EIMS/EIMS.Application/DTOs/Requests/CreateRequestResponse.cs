using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Requests
{
    public class CreateRequestResponse
    {
        public int RequestID { get; set; }
        public int CustomerID { get; set; }
        public string PaymentMethod { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountInWords { get; set; }
        public string Status { get; set; }
    }
}
