using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.Requests
{
    public class RequestDTO
    {
        public int RequestID { get; set; }
        public int RequestStatusID { get; set; }
        public int? CompanyID { get; set; }
        public int CustomerID { get; set; }
        public int? SaleID { get; set; }
        public int? CreatedInvoiceID { get; set; }
        public string? PaymentMethod { get; set; }
        public decimal SubtotalAmount { get; set; }
        public decimal VATRate { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountInWords { get; set; } = string.Empty;
        public string? Notes { get; set; }
        public int MinRows { get; set; } = 5;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string InvoiceCustomerName { get; set; }
        public string InvoiceCustomerAddress { get; set; }
        public string InvoiceCustomerTaxCode { get; set; }
    }
}
