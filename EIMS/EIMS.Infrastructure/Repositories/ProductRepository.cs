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
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Product?> GetByCodeAsync(string code)
        {
            return await _context.Products.FirstOrDefaultAsync(p => p.Name == code);
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
