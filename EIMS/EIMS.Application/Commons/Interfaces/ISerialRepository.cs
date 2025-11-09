using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Domain.Entities;

namespace EIMS.Application.Commons.Interfaces
{
    public interface ISerialRepository : IBaseRepository<Serial>
    {
        Task<Serial?> GetByIdAndLockAsync(int id);
        Task<List<Serial>> GetSerialsWithDetailsAsync();
        Task<Serial?> GetSerialWithDetailsAsync(int id);
    }
}