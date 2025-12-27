using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceTemplate;
using FluentResults;
using HandlebarsDotNet;
using MediatR;

namespace EIMS.Application.Features.Files.Commands
{
    public class GenerateInvoicePdfCommandHandler : IRequestHandler<GenerateInvoicePdfCommand, Result<byte[]>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPdfService _pdfService;
        public GenerateInvoicePdfCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPdfService pdfService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _pdfService = pdfService;
        }

        public async Task<Result<byte[]>> Handle(GenerateInvoicePdfCommand request, CancellationToken cancellationToken)
        {
            // 1. Get Data from Database
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId, includeProperties: "InvoiceItems.Product,Customer");
            if (invoice == null)
                return Result.Fail(new Error("Invoice not found").WithMetadata("ErrorCode", "Invoice.NotFound"));
            var company = await _unitOfWork.CompanyRepository.GetByIdAsync(1);
            var template = await _unitOfWork.InvoiceTemplateRepository.GetTemplateDetailsAsync(invoice.TemplateID);
            if (template == null)
                return Result.Fail(new Error("Template not found").WithMetadata("ErrorCode", "Template.NotFound"));
            TemplateConfig? config = null;
            if (!string.IsNullOrEmpty(template.LayoutDefinition))
            {
                config = JsonSerializer.Deserialize<TemplateConfig>(template.LayoutDefinition);
            }

            // B. Map the basic data
            var viewModel = new InvoiceViewModel
            {
                InvoiceNumber = invoice.InvoiceNumber.ToString(),
                SellerName = company.CompanyName,
                SellerTaxCode = company.TaxCode,
                SellerAddress = company.Address,
                SellerPhone = company.ContactPhone,
                SellerBankAccount = company.AccountNumber, // Or from Company Table
                BuyerName = invoice.Customer.CustomerName,
                BuyerAddress = invoice.Customer?.Address ?? "",
                BuyerTaxCode = invoice.Customer?.TaxCode ?? "",
                Items = _mapper.Map<List<InvoiceItemDto>>(invoice.InvoiceItems),
                LogoUrl = template.LogoUrl,
                FrameUrl = template.TemplateFrame?.ImageUrl,
                Config = config,
                GrandTotal = invoice.TotalAmount,
                AmountInWords = invoice.TotalAmountInWords,
                SignDate = invoice.SignDate.ToString(),
            };

            // C. *** CALCULATE FILLER ROWS *** // This is the specific logic you asked about
            int targetMinRows = invoice.MinRows;
            int currentItems = viewModel.Items.Count;
            int rowsToAdd = 0;

            if (currentItems < targetMinRows)
            {
                rowsToAdd = targetMinRows - currentItems;
            }

            // Create an empty list of integers [0, 1, 2, 3] just so the HTML {{each}} loop runs 4 times
            viewModel.FillerRows = Enumerable.Range(0, rowsToAdd).ToList();

            // 3. Generate HTML
            // Load template.html text
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "template.html");

            if (!File.Exists(templatePath))
                return Result.Fail("Template HTML file is missing on server.");

            string htmlTemplate = await File.ReadAllTextAsync(templatePath, cancellationToken);
            Handlebars.RegisterHelper("increment", (writer, context, parameters) =>
                        {
                            // @index is an int (0, 1, 2...), so we just add 1 to it
                            if (parameters.Length > 0 && parameters[0] is int index)
                            {
                                writer.WriteSafeString(index + 1);
                            }
                        });

            Handlebars.RegisterHelper("formatNumber", (writer, context, parameters) =>
            {
                if (parameters.Length > 0 && decimal.TryParse(parameters[0].ToString(), out decimal value))
                {
                    // Formats as 1,000,000 (standard format)
                    writer.WriteSafeString(value.ToString("N0"));
                }
            });
            // Compile with Handlebars
            var templateFunc = Handlebars.Compile(htmlTemplate);
            string finalHtml = templateFunc(viewModel); // <--- Inject ViewModel into HTML

            // 4. Convert HTML to PDF
            byte[] pdfBytes = await _pdfService.ConvertHtmlToPdfAsync(finalHtml);
            return Result.Ok(pdfBytes);
        }
    }
}