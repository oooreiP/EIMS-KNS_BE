using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbSet
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            return !await dbSet.AnyAsync(u => u.Email == email);
        }
        public async Task<List<User>> GetUsersByRoleAsync(string roleName)
        {
            return await dbSet
                .Include(u => u.Role) 
                .Where(u => u.Role.RoleName == roleName)
                .AsNoTracking() // Dùng AsNoTracking cho nh? v? ch? l?y ra ð? g?i noti
                .ToListAsync();
        }
        public async Task<List<User>> GetUsersByCustomerIdAsync(int customerId)
        {
            return await dbSet
                .Where(u => u.CustomerID == customerId && u.IsActive) 
                .AsNoTracking()
                .ToListAsync();
        }
    }
}