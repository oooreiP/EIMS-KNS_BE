using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.InvoiceTemplate.Commands
{
    public class UpdateInvoiceTemplateStatusHandler : IRequestHandler<UpdateInvoiceTemplateStatusCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInvoiceTemplateStatusHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<int>> Handle(UpdateInvoiceTemplateStatusCommand request, CancellationToken cancellationToken)
        {
            var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(request.TemplateID);

            if (template == null)
            {
                return Result.Fail<int>($"Không tìm thấy Mẫu hóa đơn với ID: {request.TemplateID}");
            }
            if (template.IsActive == request.IsActive)
            {
                return Result.Ok(template.TemplateID);
            }

            template.IsActive = request.IsActive;
            _unitOfWork.InvoiceTemplateRepository.UpdateAsync(template);
            await _unitOfWork.SaveChanges();

            return Result.Ok(template.TemplateID);
        }
    }
}
