using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.DTOs.InvoicePayment;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.DTOs.InvoiceStatement;

namespace EIMS.Application.DTOs.Customer
{
    public class CustomerDebtDetailDto
    {
        public CustomerInfoDto Customer { get; set; }
        public DebtSummaryDto Summary { get; set; }
        public PaginatedResult<StatementInvoiceDto> Invoices { get; set; }
        public PaginatedResult<PaymentHistoryDto> Payments { get; set; }
    }
        public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int TotalCount { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }

        public PaginatedResult(List<T> items, int count, int pageIndex, int pageSize)
        {
            Items = items;
            TotalCount = count;
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }
    }
}