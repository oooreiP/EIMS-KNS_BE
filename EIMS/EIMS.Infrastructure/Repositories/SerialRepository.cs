using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Repositories
{
    public class SerialRepository : BaseRepository<Serial>, ISerialRepository
    {
        private readonly ApplicationDbContext _context;
        public SerialRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        private IQueryable<Serial> GetSerialsWithDetails()
        {
            return _context.Serials
                    .Include(s => s.Prefix)
                    .Include(s => s.InvoiceType)
                    .Include(s => s.SerialStatus)
                    .OrderBy(s => s.Year);
        }

        public async Task<List<Serial>> GetSerialsWithDetailsAsync()
        {
            return await GetSerialsWithDetails().ToListAsync();
        }

        public async Task<Serial?> GetSerialWithDetailsAsync(int id)
        {
            return await GetSerialsWithDetails().FirstOrDefaultAsync(s => s.SerialID == id);
        }
    }
}