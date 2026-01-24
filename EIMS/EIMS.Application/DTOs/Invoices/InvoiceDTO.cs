using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using EIMS.Domain.Enums;

namespace EIMS.Application.DTOs.Invoices
{
    public class InvoiceDTO
    {
        public int InvoiceID { get; set; }
        public int? RequestID { get; set; }
        public int TemplateID { get; set; }
        public long InvoiceNumber { get; set; }
        public int InvoiceStatusID { get; set; }
        public int PaymentStatusID { get; set; }
        public int? CompanyId { get; set; }
        public int CustomerID { get; set; }
        public int? SalesID { get; set; }
        public int? IssuerID { get; set; }
        public int InvoiceType { get; set; } = 1;
        public int? OriginalInvoiceID { get; set; }
        public long? OriginalInvoiceNumber { get; set; }
        public string? PaymentMethod { get; set; }
        public DateTime? SignDate { get; set; }
        public DateTime? IssuedDate { get; set; }
        public DateTime? PaymentDueDate { get; set; }
        public decimal SubtotalAmount { get; set; }
        public string? InvoiceCustomerType { get; set; }
        public decimal VATRate { get; set; }
        public decimal VATAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public string TotalAmountInWords { get; set; } = string.Empty;
        public string? DigitalSignature { get; set; }
        public string? TaxAuthorityCode { get; set; }
        public string? QRCodeData { get; set; }
        public string? Notes { get; set; }
        public string? FilePath { get; set; }
        public string? XMLPath { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? AdjustmentReason { get; set; }
        public List<InvoiceItemDto> InvoiceItems { get; set; } = new List<InvoiceItemDto>();
        public decimal PaidAmount { get; set; }
        public decimal RemainingAmount { get; set; }
        public string PaymentStatus { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerEmail { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactPhone { get; set; }
        public string? TaxCode { get; set; }
        public DateTime? OriginalInvoiceSignDate { get; set; }
        public string? OriginalInvoiceSymbol { get; set; }
        public string TaxStatusCode { get; set; }
        public string TaxStatusDescription { get; set; }
        public string TaxStatusColor { get; set; }
    }
}
