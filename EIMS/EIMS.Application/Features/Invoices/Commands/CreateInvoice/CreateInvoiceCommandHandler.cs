using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Invoices;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Result<CreateInvoiceResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<CreateInvoiceResponse>> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                return Result.Fail(new Error("Invoice must has at least one item").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            if (request.TemplateID == null || request.TemplateID == 0)
                return Result.Fail(new Error("Invoice must has a valid template id").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            await using var transaction = await _unitOfWork.BeginTransactionAsync();
            try
            {
                // 1️ Xử lý Customer
                Customer? customer = null;
                if (request.CustomerID == null || request.CustomerID == 0)
                {
                    customer = new Customer
                    {
                        CustomerName = request.CompanyName ?? request.Name ?? "Khách hàng chưa đặt tên",
                        TaxCode = request.TaxCode ?? "",
                        Address = request.Address ?? "Chưa cập nhật",
                        ContactEmail = "noemail@system.local",
                        ContactPerson = request.Name,
                        ContactPhone = ""
                    };
                    customer = await _unitOfWork.CustomerRepository.CreateCustomerAsync(customer);
                }

                var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(request.TemplateID.Value);
                if (template == null)
                    return Result.Fail(new Error($"Template {request.TemplateID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
                var serial = await _unitOfWork.SerialRepository.GetByIdAndLockAsync(template.SerialID);
                if (serial == null)
                    return Result.Fail(new Error($"Template {serial.SerialID} not found").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
                serial.CurrentInvoiceNumber += 1;
                long newInvoiceNumber = serial.CurrentInvoiceNumber;
                await _unitOfWork.SerialRepository.UpdateAsync(serial);

                decimal subtotal = request.Items.Sum(i => i.Amount);
                decimal vatAmount = request.Items.Sum(i => i.VATAmount);

                if (request.Amount <= 0) request.Amount = subtotal;
                if (request.TaxAmount <= 0) request.TaxAmount = vatAmount;
                if (request.TotalAmount <= 0) request.TotalAmount = request.Amount + request.TaxAmount;

                decimal vatRate = (request.Amount > 0 && request.TaxAmount > 0)
                    ? Math.Round((request.TaxAmount / request.Amount) * 100, 2)
                    : 0;

                var invoice = new Invoice
                {
                    InvoiceNumber = newInvoiceNumber,
                    TemplateID = request.TemplateID ?? 1,
                    CustomerID = customer?.CustomerID ?? request.CustomerID!.Value,
                    CreatedAt = DateTime.UtcNow,
                    SubtotalAmount = request.Amount,
                    VATAmount = request.TaxAmount,
                    VATRate = vatRate,
                    TotalAmount = request.TotalAmount,
                    TotalAmountInWords = $"{request.TotalAmount:n0} đồng",
                    SignDate = DateTime.UtcNow,
                    InvoiceStatusID = 1,
                    PaymentStatusID = 1,
                    IssuerID = request.SignedBy ?? 1,
                    InvoiceItems = request.Items.Select(i => new InvoiceItem
                    {
                        ProductID = i.ProductId,
                        Quantity = i.Quantity,
                        Amount = i.Amount,
                        VATAmount = i.VATAmount
                    }).ToList()
                };

                await _unitOfWork.InvoicesRepository.CreateInvoiceAsync(invoice);
                await _unitOfWork.CommitAsync();
                var response = new CreateInvoiceResponse
                {
                    InvoiceID = invoice.InvoiceID,
                    InvoiceNumber = invoice.InvoiceNumber,
                    CustomerID = invoice.CustomerID,
                    TotalAmount = invoice.TotalAmount,
                    TotalAmountInWords = invoice.TotalAmountInWords,
                    Status = "Draft"
                };
                return Result.Ok(response);
            }
            catch (Exception ex)
            {
                // 10. If anything fails, roll back everything.
                await _unitOfWork.RollbackAsync();

                // Log the error (implement logging)
                // _logger.LogError(ex, "Failed to create invoice.");

                // Re-throw the exception
                return Result.Fail(new Error("Failed to create invoice").WithMetadata("ErrorCode", "Invoice.Create.Failed"));
            }
        }
    }
}