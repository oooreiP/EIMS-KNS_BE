using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IErrorNotificationRepository : IBaseRepository<InvoiceErrorNotification>
    {
        Task<InvoiceErrorNotification?> GetWithDetailsAsync(int id);

        Task<InvoiceErrorNotification?> GetByNotificationNumberAsync(string notificationNumber);

        Task<InvoiceErrorNotification?> GetByMTDiepAsync(string mtDiep);
    }
}
