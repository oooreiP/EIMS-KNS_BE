using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IAuthenticatedCommand
    {
        public int AuthenticatedUserId { get; set; }
        public int? CustomerId { get; set; }
    }
}