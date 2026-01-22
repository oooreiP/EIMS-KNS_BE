using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Company
{
    public class CompanyResponse
    {
        public int CompanyID { get; set; }
        public string CompanyName { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string TaxCode { get; set; } = string.Empty;
        public string? ContactPhone { get; set; }
        public string? AccountNumber { get; set; }
        public string? BankName { get; set; }
        public string? LogoUrl { get; set; }
    }
}