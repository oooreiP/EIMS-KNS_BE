using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;

namespace EIMS.Application.Features.Company.Commands
{
    public class UploadCompanyLogoCommandHandler
    {
        private readonly IUnitOfWork _uow;
        private readonly IFileStorageService _fileStorageService;

        public UploadCompanyLogoCommandHandler(IUnitOfWork uow, IFileStorageService fileStorageService)
        {
            _uow = uow;
            _fileStorageService = fileStorageService;
        }

        public async Task<Result<string>> Handle(UploadCompanyLogoCommand request, CancellationToken cancellationToken)
        {
            if (request.File == null || request.File.Length == 0)
            {
                return Result.Fail("Logo file is empty.");
            }

            var company = await _uow.CompanyRepository.GetByIdAsync(request.CompanyId);
            if (company == null)
            {
                return Result.Fail("Company not found.");
            }

            await using var stream = request.File.OpenReadStream();
            var uploadResult = await _fileStorageService.UploadFileAsync(stream, request.File.FileName, "company-logos");

            if (uploadResult.IsFailed)
            {
                return Result.Fail(uploadResult.Errors);
            }

            company.LogoUrl = uploadResult.Value.Url;
            await _uow.CompanyRepository.UpdateAsync(company);
            await _uow.SaveChanges();

            return Result.Ok(company.LogoUrl);
        }
    }
}