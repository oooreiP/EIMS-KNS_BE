using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.Sale
{
    public class SalesCustomerDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal TotalSpent { get; set; } 
        public int InvoiceCount { get; set; }   
        public DateTime? LastPurchaseDate { get; set; } 
    }
}
