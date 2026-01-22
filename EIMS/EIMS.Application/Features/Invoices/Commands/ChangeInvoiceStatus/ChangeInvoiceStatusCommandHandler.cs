using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using FluentResults;
using MediatR;

namespace EIMS.Application.Features.Invoices.Commands.ChangeInvoiceStatus
{
    public class UpdateInvoiceStatusCommandHandler : IRequestHandler<UpdateInvoiceStatusCommand, Result<int>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceRealtimeService _invoiceRealtimeService;
        private readonly IDashboardRealtimeService _dashboardRealtimeService;

        public UpdateInvoiceStatusCommandHandler(IUnitOfWork unitOfWork, IInvoiceRealtimeService invoiceRealtimeService, IDashboardRealtimeService dashboardRealtimeService)
        {
            _unitOfWork = unitOfWork;
            _invoiceRealtimeService = invoiceRealtimeService;
            _dashboardRealtimeService = dashboardRealtimeService;
        }
        public async Task<Result<int>> Handle(UpdateInvoiceStatusCommand request, CancellationToken cancellationToken)
        {
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if(invoice == null)
                return Result.Fail(new Error($"Invoice with ID {request.InvoiceId} not found")
                    .WithMetadata("ErrorCode", "Invoice.Status.NotFound"));
            invoice.InvoiceStatusID = request.InvoiceStatusId;
            await _unitOfWork.InvoicesRepository.UpdateAsync(invoice);
            await _unitOfWork.SaveChanges();
            await _invoiceRealtimeService.NotifyInvoiceChangedAsync(new EIMS.Application.Commons.Models.InvoiceRealtimeEvent
            {
                InvoiceId = invoice.InvoiceID,
                ChangeType = "StatusChanged",
                CompanyId = invoice.CompanyId,
                CustomerId = invoice.CustomerID,
                StatusId = invoice.InvoiceStatusID,
                Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
            }, cancellationToken);
            await _dashboardRealtimeService.NotifyDashboardChangedAsync(new EIMS.Application.Commons.Models.DashboardRealtimeEvent
            {
                Scope = "Invoices",
                ChangeType = "StatusChanged",
                EntityId = invoice.InvoiceID,
                Roles = new[] { "Admin", "Accountant", "Sale", "HOD" }
            }, cancellationToken);
            return Result.Ok(invoice.InvoiceID);
        }
    }
}
