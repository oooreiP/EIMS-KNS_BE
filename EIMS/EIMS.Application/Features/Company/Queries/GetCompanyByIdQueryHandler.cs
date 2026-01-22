using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Company;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Company.Queries
{
    public class GetCompanyByIdQueryHandler : IRequestHandler<GetCompanyByIdQuery, Result<CompanyResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetCompanyByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<CompanyResponse>> Handle(GetCompanyByIdQuery request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.Id);

            if (company == null)
            {
                return Result.Fail(new Error($"Company with ID {request.Id} not found")
                    .WithMetadata("ErrorCode", "Company.Get.NotFound"));
            }

            var response = new CompanyResponse
            {
                CompanyID = company.CompanyID,
                CompanyName = company.CompanyName,
                Address = company.Address,
                TaxCode = company.TaxCode,
                ContactPhone = company.ContactPhone,
                AccountNumber = company.AccountNumber,
                BankName = company.BankName,
                LogoUrl = company.LogoUrl
            };

            return Result.Ok(response);
        }
    }
}
