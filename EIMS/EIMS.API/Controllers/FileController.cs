using EIMS.Application.Features.Files.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] UploadFileCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }
        [HttpPost("uploadXML")]
        public async Task<IActionResult> UploadXML([FromForm] UploadXMLFileCommand command)
        {
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }
        [HttpPost("convert-pdf-xml")]
        public async Task<IActionResult> ConvertPdfToXml([FromForm] ConvertPdfToXmlCommand pdfFile)
        {
            var result = await _mediator.Send(new ConvertPdfToXmlCommand(pdfFile.PdfFile, pdfFile.Folder ?? "ParsedDocuments"));
            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        [HttpPost("generate-xml/{invoiceId:int}")]
        public async Task<IActionResult> GenerateInvoiceXml(int invoiceId)
        {
            var result = await _mediator.Send(new GenerateInvoiceXmlCommand(invoiceId));
            if (result.IsFailed)
                return BadRequest(result.Reasons);

            return Ok(new
            {
                Message = "XML file generated successfully",
                FilePath = result.Value
            });
        }
        [HttpPost("upload-template-image")]
        public async Task<IActionResult> UploadTemplateImage(IFormFile file, [FromQuery] string type = "logo")
        {
            var command = new UploadTemplateImageCommand(file, type);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(new { Error = result.Errors.FirstOrDefault()?.Message });

            // Return the URL to the frontend
            return Ok(new { Url = result.Value });
        }
        /// <summary>
        /// Generates and downloads the PDF version of the invoice.
        /// </summary>
        /// <param name="id">The Invoice ID</param>
        /// <returns>A PDF file download</returns>
        [HttpGet("{id}/pdf")]
        [ProducesResponseType(typeof(FileResult), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ExportPdf(int id)
        {
            var command = new GenerateInvoicePdfCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                // Check if it was a "Not Found" error
                if (result.Errors.Any(e => e.Metadata.ContainsKey("ErrorCode") && (string)e.Metadata["ErrorCode"] == "Invoice.NotFound"))
                {
                    return NotFound(new { message = result.Errors.First().Message });
                }

                return BadRequest(new { message = "Failed to generate PDF", errors = result.Errors.Select(e => e.Message) });
            }

            // Return the PDF file
            var pdfBytes = result.Value;
            var fileName = $"Invoice_{id}.pdf";
            return File(pdfBytes, "application/pdf", fileName);
        }
    }
}
