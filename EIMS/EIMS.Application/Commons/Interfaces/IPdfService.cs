using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IPdfService
    {
        Task<byte[]> ConvertHtmlToPdfAsync(string htmlContent);
    }
}