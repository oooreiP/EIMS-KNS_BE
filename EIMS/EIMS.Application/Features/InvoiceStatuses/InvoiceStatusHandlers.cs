using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.InvoiceStatuses;
using EIMS.Application.Features.InvoiceStatuses.Commands;
using EIMS.Application.Features.InvoiceStatuses.Queries;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceStatuses
{
    public class InvoiceStatusHandlers :
        IRequestHandler<GetAllInvoiceStatusesQuery, Result<IEnumerable<InvoiceStatusDto>>>,
        IRequestHandler<GetInvoiceStatusByIdQuery, Result<InvoiceStatusDto>>,
        IRequestHandler<CreateInvoiceStatusCommand, Result<int>>,
        IRequestHandler<UpdateInvoiceStatusCommand, Result>,
        IRequestHandler<DeleteInvoiceStatusCommand, Result>
    {
        private readonly IUnitOfWork _uow;

        public InvoiceStatusHandlers(IUnitOfWork uow)
        {
            _uow = uow;
        }

        // 1. GET ALL
        public async Task<Result<IEnumerable<InvoiceStatusDto>>> Handle(GetAllInvoiceStatusesQuery request, CancellationToken cancellationToken)
        {
            var entities = await _uow.InvoiceStatusRepository.GetAllAsync();

            var dtos = entities.Select(x => new InvoiceStatusDto
            {
                InvoiceStatusID = x.InvoiceStatusID,
                StatusName = x.StatusName
            });

            return Result.Ok(dtos);
        }

        // 2. GET BY ID
        public async Task<Result<InvoiceStatusDto>> Handle(GetInvoiceStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvoiceStatusRepository.GetByIdAsync(request.Id);
            if (entity == null) return Result.Fail("Trạng thái không tồn tại.");

            return Result.Ok(new InvoiceStatusDto
            {
                InvoiceStatusID = entity.InvoiceStatusID,
                StatusName = entity.StatusName
            });
        }

        // 3. CREATE
        public async Task<Result<int>> Handle(CreateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var exists = await _uow.InvoiceStatusRepository.GetAllQueryable()
                .AnyAsync(x => x.StatusName == request.StatusName);
            if (exists) return Result.Fail($"Trạng thái '{request.StatusName}' đã tồn tại.");

            var entity = new InvoiceStatus
            {
                StatusName = request.StatusName
            };

            await _uow.InvoiceStatusRepository.CreateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok(entity.InvoiceStatusID);
        }

        // 4. UPDATE
        public async Task<Result> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvoiceStatusRepository.GetByIdAsync(request.InvoiceStatusID);
            if (entity == null) return Result.Fail("Trạng thái không tồn tại.");

            entity.StatusName = request.StatusName;

            await _uow.InvoiceStatusRepository.UpdateAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }

        // 5. DELETE
        public async Task<Result> Handle(DeleteInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var entity = await _uow.InvoiceStatusRepository.GetByIdAsync(request.Id, includeProperties: "Invoices");
            if (entity == null) return Result.Fail("Trạng thái không tồn tại.");
            if (entity.Invoices != null && entity.Invoices.Any())
            {
                return Result.Fail($"Không thể xóa trạng thái '{entity.StatusName}' vì đang có {entity.Invoices.Count} hóa đơn sử dụng nó.");
            }
            await _uow.InvoiceStatusRepository.DeleteAsync(entity);
            await _uow.SaveChanges();

            return Result.Ok();
        }
    }
}
