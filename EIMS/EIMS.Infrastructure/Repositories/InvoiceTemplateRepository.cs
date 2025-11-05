using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;

namespace EIMS.Infrastructure.Repositories
{
    public class InvoiceTemplateRepository : BaseRepository<InvoiceTemplate>, IInvoiceTemplateRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceTemplateRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}