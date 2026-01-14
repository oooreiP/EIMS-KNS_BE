using EIMS.Application.DTOs.TaxAPIDTO;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.ErrorNotifications.Queries
{
    public class GetErrorNotificationByIdQuery : IRequest<Result<ErrorNotificationDto>>
    {
        public int Id { get; set; }
        public GetErrorNotificationByIdQuery(int id) => Id = id;
    }
}
