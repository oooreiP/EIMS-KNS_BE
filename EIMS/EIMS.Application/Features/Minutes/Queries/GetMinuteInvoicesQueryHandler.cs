using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
using EIMS.Application.DTOs.Minutes;
using EIMS.Application.DTOs.User;
using FluentResults;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Minutes.Queries
{
    public class GetMinuteInvoicesQueryHandler : IRequestHandler<GetMinuteInvoicesQuery, Result<PaginatedList<MinuteInvoiceDto>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;
        public GetMinuteInvoicesQueryHandler(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<MinuteInvoiceDto>>> Handle(GetMinuteInvoicesQuery request, CancellationToken cancellationToken)
        {
            var query = _uow.MinuteInvoiceRepository.GetAllQueryable()
                .Include(m => m.Invoice)
                .ThenInclude(m => m.Customer)
                .AsNoTracking();

            if (request.SaleId.HasValue)
            {
                int saleId = request.SaleId.Value;
                query = query.Where(x => x.Invoice != null && x.Invoice.Customer.SaleID == saleId);
            }

            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string search = request.SearchTerm.ToLower().Trim();
                query = query.Where(x =>
                    x.MinuteCode.ToLower().Contains(search) ||
                    x.Invoice.InvoiceNumber.ToString().Contains(search) ||
                    x.Invoice.InvoiceCustomerName.ToLower().Contains(search));
            }
            if (request.FromDate.HasValue)
            {
                query = query.Where(x => x.CreatedAt >= request.FromDate.Value);
            }
            if (request.ToDate.HasValue)
            {
                var toDate = request.ToDate.Value.Date.AddDays(1);
                query = query.Where(x => x.CreatedAt < toDate);
            }

            if (request.Status.HasValue)
            {
                query = query.Where(x => x.Status == request.Status.Value);
            }
            if (request.MinuteType.HasValue)
            {
                query = query.Where(x => x.MinutesType == request.MinuteType.Value);
            }
            var totalCount = await query.CountAsync();
            var items = await query
             .OrderByDescending(x => x.CreatedAt)
             .Skip((request.PageIndex - 1) * request.PageSize)
             .Take(request.PageSize)
             .ProjectTo<MinuteInvoiceDto>(_mapper.ConfigurationProvider)
             .ToListAsync(cancellationToken);

            return Result.Ok(new PaginatedList<MinuteInvoiceDto>(items, totalCount, request.PageIndex, request.PageSize));
        }
    }
}
