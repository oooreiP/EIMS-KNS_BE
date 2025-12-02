using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.InvoiceTemplate;
using FluentResults;
using HandlebarsDotNet;
using MediatR;

namespace EIMS.Application.Features.InvoiceTemplate.Queries
{
    public class ViewTemplateQueryHandler : IRequestHandler<ViewTemplateQuery, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ViewTemplateQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<string>> Handle(ViewTemplateQuery request, CancellationToken cancellationToken)
        {
            // 1. Fetch Template from Database (Include Frame!)
            // Ensure GetTemplateDetailsAsync includes .Include(t => t.TemplateFrame)
            var template = await _unitOfWork.InvoiceTemplateRepository.GetTemplateDetailsAsync(request.TemplateId);

            if (template == null)
                return Result.Fail(new Error("Template not found"));

            // 2. Parse the Saved Configuration
            TemplateConfig config;
            try
            {
                config = JsonSerializer.Deserialize<TemplateConfig>(template.LayoutDefinition) ?? new TemplateConfig();
            }
            catch
            {
                config = new TemplateConfig(); // Fallback if JSON is invalid/empty
            }

            // 3. Create SAMPLE DATA (Just for visualization)
            // You can copy/paste the same sample data from your Preview handler
            var viewModel = new InvoiceViewModel
            {
                // Header
                Serial = $"{template.Serial?.Prefix?.PrefixID}C25T",
                InvoiceNumber = "0000000",
                Day = DateTime.Now.Day,
                Month = DateTime.Now.Month,
                Year = DateTime.Now.Year,
                SignDate = DateTime.Now.ToString("dd/MM/yyyy"),

                // Visuals (From Database)
                LogoUrl = template.LogoUrl,
                FrameUrl = template.TemplateFrame?.ImageUrl, // Get from linked table
                Config = config,

                // Sample Seller/Buyer Content
                SellerName = "CÔNG TY CỔ PHẦN E-INVOICE (DEMO)",
                SellerTaxCode = "0101234567",
                SellerAddress = "Tầng 1, Tòa nhà ABC, Phố Demo, Hà Nội",

                BuyerName = "Nguyễn Văn Khách Hàng",
                BuyerCompany = "Công ty TNHH Mua Hàng Mẫu",

                // Sample Items
                Items = new List<InvoiceItemDto>
                {
                    new InvoiceItemDto { ProductId = 1, Quantity = 5, VATAmount = 10, Amount = 500000 },
                    new InvoiceItemDto { ProductId = 2, Quantity = 1, VATAmount = 10, Amount = 2000000 }
                },
                GrandTotal = 2750000,
                AmountInWords = "Hai triệu bảy trăm năm mươi nghìn đồng"
            };

            // 4. Calculate Filler Rows (Visual Logic)
            int minRows = config.TableSettings.MinRows;
            int currentItems = viewModel.Items.Count;
            int rowsToAdd = (currentItems < minRows) ? minRows - currentItems : 0;
            viewModel.FillerRows = Enumerable.Range(0, rowsToAdd).ToList();

            // 5. Render HTML
            string templatePath = Path.Combine(Directory.GetCurrentDirectory(), "template.html");
            if (!File.Exists(templatePath)) return Result.Fail("Template HTML file missing on server");

            string htmlTemplate = await File.ReadAllTextAsync(templatePath, cancellationToken);
            var templateFunc = Handlebars.Compile(htmlTemplate);
            string finalHtml = templateFunc(viewModel);

            return Result.Ok(finalHtml);
        }
    }
}