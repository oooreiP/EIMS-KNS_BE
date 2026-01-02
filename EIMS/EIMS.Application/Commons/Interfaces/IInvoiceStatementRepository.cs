using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoiceStatementRepository : IBaseRepository<InvoiceStatement>
    {
        Task<InvoiceStatement?> GetByIdWithInvoicesAsync(int id);
        Task<List<InvoiceStatement>> GetStatementsContainingInvoiceAsync(int invoiceId);
        Task<List<InvoiceStatement>> GetUnpaidStatementsInMonthAsync(int month, int year);
    }
}