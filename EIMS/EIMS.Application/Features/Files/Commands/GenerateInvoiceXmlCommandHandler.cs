using EIMS.Application.Commons.Interfaces;
using EIMS.Application.Commons.Mapping;
using EIMS.Application.DTOs.XMLModels;
using FluentResults;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace EIMS.Application.Features.Files.Commands
{
    public class GenerateInvoiceXmlCommandHandler : IRequestHandler<GenerateInvoiceXmlCommand, Result<string>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<GenerateInvoiceXmlCommandHandler> _logger;

        public GenerateInvoiceXmlCommandHandler(IUnitOfWork unitOfWork, ILogger<GenerateInvoiceXmlCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result<string>> Handle(GenerateInvoiceXmlCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var invoice = await _unitOfWork.InvoicesRepository
                    .GetByIdAsync(request.InvoiceId, includeProperties: "Customer,InvoiceItems.Product,Company");

                if (invoice == null)
                    return Result.Fail(new Error("Invoice not found").WithMetadata("ErrorCode", "Invoice.NotFound"));

                var xmlModel = InvoiceXmlMapper.MapInvoiceToXmlModel(invoice);
                var serializer = new XmlSerializer(typeof(HDon));
                var fileName = $"Invoice_{invoice.InvoiceNumber.ToString() ?? invoice.InvoiceID.ToString()}.xml";
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedXml");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, fileName);

                using (var writer = new StreamWriter(filePath))
                {
                    serializer.Serialize(writer, xmlModel);
                }

                _logger.LogInformation("Invoice XML generated successfully: {FilePath}", filePath);
                return Result.Ok(filePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to generate invoice XML for ID {InvoiceId}", request.InvoiceId);
                return Result.Fail(new Error("Failed to generate invoice XML").CausedBy(ex));
            }
        }
    }
}
