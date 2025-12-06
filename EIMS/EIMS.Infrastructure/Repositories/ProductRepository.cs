using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetByCodeAsync(string code)
        {
            // USE Contains() for partial match
            // We also include the Category for display purposes if needed
            return await _context.Products
                .Where(p => p.Code.Contains(code))
                .Include(p => p.Category)
                .ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Products
                .Where(p => p.CategoryID == categoryId)
                .Include(p => p.Category)
                .ToListAsync();
        }
    }
}
