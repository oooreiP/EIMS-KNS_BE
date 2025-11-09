using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoicesRepository : IBaseRepository<Invoice>
    {
        // Task<string> GetNextInvoiceNumberAsync(int templateId);
        Task<Invoice> CreateInvoiceAsync(Invoice invoice);

    }
}
