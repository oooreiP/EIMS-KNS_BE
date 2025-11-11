using EIMS.Application.DTOs;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IExternalCompanyLookupService
    {
        Task<Result<CompanyLookupDto>> LookupByTaxCodeAsync(string taxCode);
    }
}
