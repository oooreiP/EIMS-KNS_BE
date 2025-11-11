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
        public async Task<IActionResult> Upload([FromForm] UploadFileCommand command )
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
    }
}
