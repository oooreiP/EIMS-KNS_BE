using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.CQT.NotifyInvoiceError
{
    public class CreateErrorNotificationCommand : IRequest<Result<int>>
    {
        public string? TaxAuthorityCode { get; set; } = "10500"; // Mã CQT (VD: 10500)
        public string? Place { get; set; } = "TP. Hồ Chí Minh"; // Địa danh (VD: TP.HCM)
        public List<ErrorDetailDto> ErrorItems { get; set; }
    }
}
