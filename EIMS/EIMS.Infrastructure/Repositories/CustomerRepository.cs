using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

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
        public async Task<List<Customer>> SearchAsync(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return new List<Customer>();
            }

            var term = searchTerm.Trim().ToLower();

            return await _context.Customers
                .Where(c => c.TaxCode.Contains(term) || 
                            c.CustomerName.ToLower().Contains(term) ||
                            (c.ContactPhone != null && c.ContactPhone.Contains(term)))
                .Take(20) // Limit results for performance
                .ToListAsync();
        }
    }
}
