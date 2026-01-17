using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Dashboard.HOD
{
    public class DebtBucketDto
    {
        public decimal Amount { get; set; }
        public int Count { get; set; } // Số khách hàng
        public string Label { get; set; }
    }
}