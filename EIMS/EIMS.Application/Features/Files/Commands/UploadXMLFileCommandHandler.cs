using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Files.Commands
{
    internal class UploadXMLFileCommandHandler : IRequestHandler<UploadXMLFileCommand, Result<FileUploadResultDto>>
    {
        private readonly IFileStorageService _cloud;
        private readonly IUnitOfWork _unitOfWork;

        public UploadXMLFileCommandHandler(IFileStorageService cloud, IUnitOfWork unitOfWork)
        {
            _cloud = cloud;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<FileUploadResultDto>> Handle(UploadXMLFileCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.invoiceId);

            var result = await _cloud.UploadFileAsync(request.File);
            invoice.XMLPath = result.Value.Url;
            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();
            return result;
        }
    }
}
