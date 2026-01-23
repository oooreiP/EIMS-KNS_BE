using System.Collections.Generic;

namespace EIMS.Application.DTOs.InvoiceStatement
{
    public class StatementPaymentsResponseDto
    {
        public int StatementId { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal BalanceDue { get; set; }
        public int StatusId { get; set; }
        public string Status { get; set; } = "Unknown";
        public List<StatementPaymentItemDto> Payments { get; set; } = new();
    }
}
