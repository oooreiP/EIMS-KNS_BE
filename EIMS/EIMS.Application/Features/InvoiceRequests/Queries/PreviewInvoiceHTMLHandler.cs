using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons;
using EIMS.Domain.Entities;
using FluentResults;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EIMS.Application.DTOs.InvoiceItem;
using EIMS.Application.Features.InvoiceRequests.Commands;

namespace EIMS.Application.Features.InvoiceRequests.Queries
{
    public class PreviewInvoiceHTMLHandler : IRequestHandler<PreviewInvoiceHTMLQuery, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IInvoiceXMLService _invoiceXMLService;
        private readonly IPdfService _pdfService;


        public PreviewInvoiceHTMLHandler(
            IUnitOfWork unitOfWork,
            IInvoiceXMLService invoiceXMLService,
            IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
            _invoiceXMLService = invoiceXMLService;
            _pdfService = pdfService;
        }

        public async Task<Result<string>> Handle(PreviewInvoiceHTMLQuery request, CancellationToken cancellationToken)
        {
            var invoiceRequest = await _unitOfWork.InvoiceRequestRepository.GetAllQueryable()
                .AsNoTracking() 
                .Include(i => i.Customer)
                .Include(i => i.InvoiceRequestItems).ThenInclude(it => it.Product)
                .FirstOrDefaultAsync(i => i.RequestID == request.RequestId, cancellationToken);

            if (invoiceRequest == null) return Result.Fail("Invoice Request not found");
            var template = await _unitOfWork.InvoiceTemplateRepository.GetByIdAsync(-1);
            if (template == null) return Result.Fail("Template not found");

            var companyId = invoiceRequest.CompanyID ?? 1; 
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(companyId);
            var invoice = new EmptyRequest
            {
                TemplateID = -1,
                InvoiceStatusID = 1,
                CompanyID = invoiceRequest.CompanyID ?? 1,
                SalesID = invoiceRequest.SaleID,
                MinRows = invoiceRequest.MinRows,
                Notes = invoiceRequest.Notes,
                PaymentMethod = invoiceRequest.PaymentMethod,
                CustomerID = invoiceRequest.CustomerID,
                CustomerName = invoiceRequest.InvoiceCustomerName,
                TaxCode = invoiceRequest.InvoiceCustomerTaxCode ?? string.Empty,
                Address = invoiceRequest.InvoiceCustomerAddress,
                ContactEmail = invoiceRequest.Customer?.ContactEmail,
                ContactPerson = invoiceRequest.Customer?.ContactPerson,
                ContactPhone = invoiceRequest.Customer?.ContactPhone,
                Amount = invoiceRequest.SubtotalAmount,
                TaxAmount = invoiceRequest.VATAmount,
                TotalAmount = invoiceRequest.TotalAmount,
                Items = invoiceRequest.InvoiceRequestItems?.Select(item => new CreateInvoiceItemRequest
                {
                    ProductId = item.ProductID,
                    ProductName = item.Product.Name,
                    Unit = item.Product.Unit,
                    Quantity = item.Quantity,
                    Amount = item.Amount,
                    VATAmount = item.VATAmount
                }).ToList() ?? new List<CreateInvoiceItemRequest>()
            };

            try
            {
                string xmlContent = await _pdfService.PreviewInvoiceHtmlAsync(invoice, request.RootPath);

                return Result.Ok(xmlContent);
            }
            catch (Exception ex)
            {
                return Result.Fail($"Lỗi tạo PDF Preview: {ex.Message}");
            }
        }
    }
}
