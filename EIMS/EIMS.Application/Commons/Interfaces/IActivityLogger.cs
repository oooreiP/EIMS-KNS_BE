using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IActivityLogger
    {
        Task LogAsync(string action, string description, bool isSuccess = true);
    }
}
