using System.Text.Json.Serialization;
using EIMS.Application.DTOs.Customer;

public class CustomerDebtDetailDto
{
    [JsonPropertyName("customer")]
    public CustomerInfoDto Customer { get; set; }

    [JsonPropertyName("summary")]
    public CustomerDebtSummaryDto Summary { get; set; }

    [JsonPropertyName("unpaidInvoices")]
    public PaginatedResult<UnpaidInvoiceItemDto> UnpaidInvoices { get; set; }

    [JsonPropertyName("paymentHistory")]
    public PaginatedResult<PaymentHistoryItemDto> PaymentHistory { get; set; }
}

// Class Generic cho phân trang (items, pageIndex, pageSize...)
public class PaginatedResult<T>
{
    [JsonPropertyName("items")]
    public List<T> Items { get; set; }

    [JsonPropertyName("pageIndex")]
    public int PageIndex { get; set; }

    [JsonPropertyName("pageSize")]
    public int PageSize { get; set; }

    [JsonPropertyName("totalCount")]
    public int TotalCount { get; set; }

    [JsonPropertyName("totalPages")]
    public int TotalPages { get; set; }
    [JsonPropertyName("hasPreviousPage")]
    public bool HasPreviousPage => PageIndex > 1;
    [JsonPropertyName("hasNextPage")]
    public bool HasNextPage => PageIndex < TotalPages;

    public PaginatedResult(List<T> items, int count, int pageIndex, int pageSize)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = count;
        TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        Items = items;
    }
}

// Item hóa đơn chưa thanh toán (Đúng tên trường bạn yêu cầu)
public class UnpaidInvoiceItemDto
{
    [JsonPropertyName("invoiceId")]
    public int InvoiceId { get; set; }

    [JsonPropertyName("invoiceNumber")]
    public string InvoiceNumber { get; set; } // Ví dụ: INV-001 hoặc số 123

    [JsonPropertyName("invoiceDate")]
    public DateTime InvoiceDate { get; set; } // Ngày ký hoặc ngày tạo

    [JsonPropertyName("dueDate")]
    public DateTime? DueDate { get; set; }

    [JsonPropertyName("totalAmount")]
    public decimal TotalAmount { get; set; }

    [JsonPropertyName("paidAmount")]
    public decimal PaidAmount { get; set; }

    [JsonPropertyName("remainingAmount")]
    public decimal RemainingAmount { get; set; }

    [JsonPropertyName("paymentStatus")]
    public string PaymentStatus { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("isOverdue")]
    public bool IsOverdue { get; set; }
    [JsonPropertyName("invoiceStatusID")]
    public int InvoiceStatusID { get; set; }

    [JsonPropertyName("invoiceStatus")]
    public string InvoiceStatus { get; set; }
}

// Item lịch sử thanh toán
public class PaymentHistoryItemDto
{
    [JsonPropertyName("paymentId")]
    public int PaymentId { get; set; }

    [JsonPropertyName("paymentDate")]
    public DateTime PaymentDate { get; set; }

    [JsonPropertyName("amountPaid")]
    public decimal AmountPaid { get; set; }

    [JsonPropertyName("paymentMethod")]
    public string PaymentMethod { get; set; }

    [JsonPropertyName("transactionCode")]
    public string TransactionCode { get; set; }

    [JsonPropertyName("invoiceNumber")] // Để biết trả cho hóa đơn nào
    public string InvoiceNumber { get; set; }
    [JsonPropertyName("invoiceId")]
    public int InvoiceId { get; set; }

    [JsonPropertyName("note")]
    public string Note { get; set; }

    [JsonPropertyName("userId")]
    public int? UserId { get; set; } // Giả sử CreatedBy là int?

    [JsonPropertyName("userName")]
    public string UserName { get; set; }
}
