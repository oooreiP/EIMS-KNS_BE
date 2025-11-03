using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using EIMS.Infrastructure.Repositories.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Repositories
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        public async Task<Customer> CreateCustomerAsync(Customer customer)
        {
            await CreateAsync(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        public async Task<Customer?> GetByTaxCodeAsync(string taxCode)
        {
            if (string.IsNullOrWhiteSpace(taxCode)) return null;
            return await dbSet.FirstOrDefaultAsync(c => c.TaxCode == taxCode);
        }
    }
}
