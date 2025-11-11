using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EIMS.Application.Features.Invoices.Commands.CreateInvoice
{
    public class CreateInvoiceCommandHandler : IRequestHandler<CreateInvoiceCommand, Invoice>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateInvoiceCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Invoice> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("Invoice must contain at least one item.");

            // 1️⃣ Xử lý Customer
            Domain.Entities.Customer? customer = null;
            if (request.CustomerID == null || request.CustomerID == 0)
            {
                customer = new Domain.Entities.Customer
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

            var nextInvoiceNumber = await _unitOfWork.InvoicesRepository
                        .GetNextInvoiceNumberAsync(request.TemplateID ?? 1);
            decimal subtotal = request.Items.Sum(i => i.Amount);
            decimal vatAmount = request.Items.Sum(i => i.VATAmount);

            if (request.Amount <= 0) request.Amount = subtotal;
            if (request.TaxAmount <= 0) request.TaxAmount = vatAmount;
            if (request.TotalAmount <= 0) request.TotalAmount = request.Amount + request.TaxAmount;

            decimal vatRate = (request.Amount > 0 && request.TaxAmount > 0)
                ? Math.Round((request.TaxAmount / request.Amount) * 100, 2)
                : 0;

            var invoice = new Domain.Entities.Invoice
            {
                InvoiceNumber = long.Parse(nextInvoiceNumber),
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
            await _unitOfWork.SaveChanges();

            return invoice;
        }
    }
}