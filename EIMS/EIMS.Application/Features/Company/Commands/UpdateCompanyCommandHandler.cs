using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Company.Commands
{
    public class UpdateCompanyCommandHandler : IRequestHandler<UpdateCompanyCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCompanyCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<int>> Handle(UpdateCompanyCommand request, CancellationToken cancellationToken)
        {
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(request.CompanyID);
            if (company == null)
            {
                return Result.Fail(new Error($"Company with ID {request.CompanyID} not found")
                    .WithMetadata("ErrorCode", "Company.Update.NotFound"));
            }
            company.CompanyName = request.CompanyName;
            company.Address = request.Address;
            company.TaxCode = request.TaxCode;
            company.ContactPhone = request.ContactPhone;
            company.AccountNumber = request.AccountNumber;
            company.BankName = request.BankName;
            await _unitOfWork.CompanyRepository.UpdateAsync(company);
            await _unitOfWork.SaveChanges();

            return Result.Ok(company.CompanyID);
        }
    }
}