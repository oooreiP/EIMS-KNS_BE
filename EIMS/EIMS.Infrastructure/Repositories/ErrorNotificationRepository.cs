using EIMS.Application.Commons.Interfaces;
using EIMS.Domain.Entities;
using EIMS.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Infrastructure.Repositories
{
    public class ErrorNotificationRepository : BaseRepository<InvoiceErrorNotification>, IErrorNotificationRepository
    {
        public ErrorNotificationRepository(ApplicationDbContext context) : base(context) { }

        public async Task<InvoiceErrorNotification?> GetWithDetailsAsync(int id)
        {
            return await dbSet
                .Include(x => x.Details) 
                .ThenInclude(d => d.Invoice)
                .FirstOrDefaultAsync(x => x.InvoiceErrorNotificationID == id);
        }

        public async Task<InvoiceErrorNotification?> GetByNotificationNumberAsync(string notificationNumber)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.NotificationNumber == notificationNumber);
        }

        public async Task<InvoiceErrorNotification?> GetByMTDiepAsync(string mtDiep)
        {
            return await dbSet.FirstOrDefaultAsync(x => x.MTDiep == mtDiep);
        }
    }
}
