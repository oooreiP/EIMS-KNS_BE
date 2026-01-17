using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Requests
{
    public class GetInvoiceRequestDto
    {
        public int RequestID { get; set; }
        public string CustomerName { get; set; }
        public string TaxCode { get; set; }
        public decimal TotalAmount { get; set; }
        public string StatusName { get; set; }
        public int StatusId { get; set; } // Để FE tô màu (Pending=Vàng, Approved=Xanh)
        public string SaleName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
