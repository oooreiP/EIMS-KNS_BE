using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.DTOs.InvoicePayment
{
    public class PaymentHistoryDto
    {
        public int PaymentId { get; set; }
        public int InvoiceId { get; set; }
        public string InvoiceNumber { get; set; }
        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionCode { get; set; }
        public string Note { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
    }
}