using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceTemplateRepository : BaseRepository<InvoiceTemplate>, IInvoiceTemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceTemplateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
        private IQueryable<InvoiceTemplate> GetFullTemplateQuery()
        {
            return _db.InvoiceTemplates
                .Include(t => t.Serial)
                    .ThenInclude(s => s.Prefix)
                .Include(t => t.Serial)
                    .ThenInclude(s => s.SerialStatus)
                .Include(t => t.Serial)
                    .ThenInclude(s => s.InvoiceType)
                .Include(t => t.TemplateType)
                .Include(t => t.TemplateFrame)
                .OrderBy(t => t.TemplateName);
        }

        public async Task<List<InvoiceTemplate>> GetTemplatesWithDetailsAsync()
        {
            return await GetFullTemplateQuery().ToListAsync();
        }
        public async Task<InvoiceTemplate?> GetTemplateDetailsAsync(int id)
        {
            return await GetFullTemplateQuery().FirstOrDefaultAsync(t => t.TemplateID == id);
        }
    }
}