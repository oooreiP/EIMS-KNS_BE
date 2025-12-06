using EIMS.Application.DTOs.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface ITaxApiClient
    {
        Task<TaxApiResponse> SendTaxMessageAsync(string xmlPayload, string? referenceId);
    }
}
