using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<User?> GetByEmailAsync(string email);
        Task<List<User>> GetUsersByRoleAsync(string roleName);
        Task<bool> IsEmailUniqueAsync(string email);
        Task<List<User>> GetUsersByCustomerIdAsync(int customerId);
    }
}