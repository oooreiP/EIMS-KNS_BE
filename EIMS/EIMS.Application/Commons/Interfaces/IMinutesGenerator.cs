using EIMS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Commons.Interfaces
{
    public interface IMinutesGenerator
    {
        Task<byte[]> GenerateReplacementMinutesAsync(Invoice invoice, string reason, string contentBefore, string contentAfter, string adjustmentNumber,  DateTime agreementDate);
        Task<byte[]> GenerateAdjustmentMinutesAsync(Invoice invoice, string reason, string contentBefore, string contentAfter, string adjustmentNumber, DateTime agreementDate);

    }
}
