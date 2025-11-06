using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoiceTemplateRepository : IBaseRepository<InvoiceTemplate>
    {
        Task<List<InvoiceTemplate>> GetTemplatesWithDetailsAsync();
        Task<InvoiceTemplate?> GetTemplateDetailsAsync(int id);
    }
}