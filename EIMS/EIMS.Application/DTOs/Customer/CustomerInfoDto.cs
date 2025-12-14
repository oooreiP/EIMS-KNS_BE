using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Customer
{
    public class CustomerInfoDto
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}