using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<Customer?> GetByTaxCodeAsync(string taxCode);
        Task<List<Customer>> SearchAsync(string searchTerm);
    }
}
