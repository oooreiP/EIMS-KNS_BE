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
            var invoice = await _unitOfWork.InvoicesRepository.GetByIdAsync(request.InvoiceId);
            if (invoice == null)
                return Result.Fail(new Error("Invoice not found").WithMetadata("ErrorCode", "Invoice.NotFound"));

            var template = await _unitOfWork.InvoiceTemplateRepository.GetTemplateDetailsAsync(invoice.TemplateID);
            if (template == null)
                return Result.Fail(new Error("Template not found").WithMetadata("ErrorCode", "Template.NotFound"));


            // 2. === APPLY THE LOGIC HERE ===

            // A. Parse the JSON Config from the Template
            // We need this to know "MinRows" (e.g., 5)
            var config = JsonSerializer.Deserialize<TemplateConfig>(template.LayoutDefinition);
            int minRows = config.TableSettings.MinRows;

            // B. Map the basic data
            var viewModel = new InvoiceViewModel
            {
                InvoiceNumber = invoice.InvoiceNumber.ToString(),
                SellerName = "My Company Name", // Or from Company Table
                BuyerName = invoice.Customer.CustomerName,
                Items = _mapper.Map<List<InvoiceItemDto>>(invoice.InvoiceItems),
                LogoUrl = template.LogoUrl,
                FrameUrl = template.TemplateFrame?.ImageUrl,
                Config = config
            };

            // C. *** CALCULATE FILLER ROWS *** // This is the specific logic you asked about
            int currentItems = viewModel.Items.Count; // e.g., Customer bought 1 item
            int rowsToAdd = 0;

            if (currentItems < minRows)
            {
                rowsToAdd = minRows - currentItems; // 5 - 1 = 4 empty rows needed
            }

            // Create an empty list of integers [0, 1, 2, 3] just so the HTML {{each}} loop runs 4 times
            viewModel.FillerRows = Enumerable.Range(0, rowsToAdd).ToList();

            // 3. Generate HTML
            // Load template.html text
            string htmlTemplate = File.ReadAllText("template.html");

            // Compile with Handlebars
            var templateFunc = Handlebars.Compile(htmlTemplate);
            string finalHtml = templateFunc(viewModel); // <--- Inject ViewModel into HTML

            // 4. Convert HTML to PDF
            byte[] pdfBytes = await _pdfService.ConvertHtmlToPdfAsync(finalHtml);
            return Result.Ok(pdfBytes);
        }
    }
}