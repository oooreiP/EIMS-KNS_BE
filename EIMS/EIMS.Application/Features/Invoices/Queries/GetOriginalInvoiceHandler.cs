using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Invoices;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Queries
{
    public class GetOriginalInvoiceHandler : IRequestHandler<GetOriginalInvoiceQuery, Result<InvoiceDTO>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetOriginalInvoiceHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<InvoiceDTO>> Handle(GetOriginalInvoiceQuery request, CancellationToken cancellationToken)
        {
            // 1. Tìm hóa đơn con (Child) và Include luôn hóa đơn cha (Original)
            // Cần Include sâu vào Original để lấy đủ Customer, Items...
            var childInvoice = await _uow.InvoicesRepository.GetAllQueryable()
                .AsNoTracking()
                .Include(x => x.OriginalInvoice)
                    .ThenInclude(o => o.Customer)
                .Include(x => x.OriginalInvoice)
                    .ThenInclude(o => o.InvoiceItems)
                        .ThenInclude(i => i.Product)
                .Include(x => x.OriginalInvoice)
                    .ThenInclude(o => o.Template)
                        .ThenInclude(t => t.Serial)
                .FirstOrDefaultAsync(x => x.InvoiceID == request.ChildInvoiceId, cancellationToken);

            // 2. Validate
            if (childInvoice == null)
            {
                return Result.Fail<InvoiceDTO>("Không tìm thấy hóa đơn này.");
            }

            // Kiểm tra xem nó có cha không
            if (childInvoice.OriginalInvoiceID == null || childInvoice.OriginalInvoice == null)
            {
                // Trường hợp user truyền vào ID của chính hóa đơn gốc -> Báo lỗi hoặc trả về chính nó tùy logic
                return Result.Fail<InvoiceDTO>($"Hóa đơn #{childInvoice.InvoiceNumber} là hóa đơn gốc, không có hóa đơn cha.");
            }

            // 3. Map hóa đơn CHA sang DTO
            var originalDto = _mapper.Map<InvoiceDTO>(childInvoice.OriginalInvoice);

            return Result.Ok(originalDto);
        }
    }
}

