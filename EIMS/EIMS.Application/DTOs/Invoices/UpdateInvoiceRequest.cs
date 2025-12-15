using System.Collections.Generic;
using EIMS.Application.DTOs;

namespace EIMS.Application.DTOs
{
    public class UpdateInvoiceRequest
    {
        public int? CustomerID { get; set; }
        public string TaxCode { get; set; } = string.Empty;
        public string? CustomerName { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactEmail { get; set; }
        public string? ContactPhone { get; set; }
        public string? Address { get; set; }
        public string? Notes { get; set; }
        public string? PaymentMethod { get; set; }
        public List<InvoiceItemDto>? Items { get; set; }
        
        public decimal? Amount { get; set; } 
        public decimal? TaxAmount { get; set; } 
        public decimal? TotalAmount { get; set; } 
        
        public int? MinRows { get; set; }
        public int? SignedBy { get; set; } 
    }
}