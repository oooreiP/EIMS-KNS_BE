using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IInvoiceBackgroundService
    {
        void ProcessInvoiceCreation(int invoiceId, int userId, int? requestId);
    }
}
