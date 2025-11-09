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
        public async Task<Serial?> GetByIdAndLockAsync(int id)
        {
            // This SQL command tells PostgreSQL to find the row ("SerialID" = {0})
            // and apply an exclusive lock to it ("FOR UPDATE").
            //
            // Any other user trying to "SELECT...FOR UPDATE" this *same row*
            // will be forced to wait until your current database transaction
            // is finished (either committed or rolled back).
            //
            // This 100% prevents a race condition.
            var serial = await _db.Serials
                .FromSqlRaw("SELECT * FROM \"Serials\" WHERE \"SerialID\" = {0} FOR UPDATE", id)
                .FirstOrDefaultAsync();
            
            return serial;
        }
    }
}