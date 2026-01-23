using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;

namespace EIMS.Infrastructure.Repositories
{
    public class StatementPaymentRepository : BaseRepository<StatementPayment>, IStatementPaymentRepository
    {
        public StatementPaymentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
