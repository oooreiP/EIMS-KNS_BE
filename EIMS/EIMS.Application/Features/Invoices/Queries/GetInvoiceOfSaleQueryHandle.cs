using AutoMapper;
using AutoMapper.QueryableExtensions;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Models;
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
    internal class GetInvoiceOfSaleQueryHandle : IRequestHandler<GetSaleInvoicesQuery, Result<PaginatedList<InvoiceDTO>>>
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public GetInvoiceOfSaleQueryHandle(IUnitOfWork uow, IMapper mapper)
        {
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<Result<PaginatedList<InvoiceDTO>>> Handle(GetSaleInvoicesQuery request, CancellationToken cancellationToken)
        {
            var allowedStatuses = new List<int> { 2, 4, 5 };
            var query = _uow.InvoicesRepository.GetAllQueryable()
                            .AsNoTracking() 
                            .Include(x => x.Customer) 
                            .Include(x => x.Sales) 
                            .Where(x => x.SalesID != null)
                            .Where(x => allowedStatuses.Contains(x.InvoiceStatusID));

            // 2. Các bộ lọc bổ sung (Filter)

            // Nếu muốn lọc theo 1 Sale cụ thể
            if (request.SpecificSaleId.HasValue)
            {
                query = query.Where(x => x.SalesID == request.SpecificSaleId);
            }

            // Tìm kiếm theo mã hóa đơn hoặc tên khách
            if (!string.IsNullOrEmpty(request.SearchTerm))
            {
                string term = request.SearchTerm.ToLower();
                query = query.Where(x => x.InvoiceNumber.ToString().Contains(term)
                                      || x.Customer.CustomerName.ToLower().Contains(term)
                                      || x.LookupCode.ToLower().Contains(term));
            }

            // Lọc theo ngày (nếu có)
            if (request.FromDate.HasValue)
                query = query.Where(x => x.IssuedDate >= request.FromDate.Value);

            if (request.ToDate.HasValue)
                query = query.Where(x => x.IssuedDate <= request.ToDate.Value);

            // 3. Sắp xếp (Mới nhất lên đầu)
            query = query.OrderByDescending(x => x.IssuedDate)
                         .ThenByDescending(x => x.InvoiceID);
            var result = await PaginatedList<InvoiceDTO>.CreateAsync(
                query.ProjectTo<InvoiceDTO>(_mapper.ConfigurationProvider), 
                request.PageIndex,
                request.PageSize
            );
            return Result.Ok(result);
        }
    }
}
