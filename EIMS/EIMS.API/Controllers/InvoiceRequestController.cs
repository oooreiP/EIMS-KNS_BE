using AutoMapper;
using EIMS.Application.Features.InvoiceRequests.Commands;
using EIMS.Application.Features.Invoices.Queries;
using EIMS.Application.Features.InvoiceRequests.Queries;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Authorization;
using System.Text;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceRequestController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        public InvoiceRequestController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }
        [HttpPost]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateRequest([FromBody] CreateInvoiceRequestCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invoice creation failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided."
                });
            }
            return Ok(result.Value);
        }
        [HttpPost("{requestId}/upload-evidence")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UploadEvidence(
        [FromRoute] int requestId, IFormFile pdfFile,
        CancellationToken cancellationToken)
        {
            if (pdfFile == null || pdfFile.Length == 0)
            {
                return BadRequest("Vui lòng đính kèm file PDF.");
            }

            var command = new UploadEvidenceCommand(requestId, pdfFile);
            var result = await _mediator.Send(command, cancellationToken);

            if (result.IsFailed)
            {
                var error = result.Errors.FirstOrDefault();
                return BadRequest(new
                {
                    Message = error?.Message,
                    Metadata = error?.Metadata
                });
            }

            return Ok(new
            {
                FileUrl = result.Value
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetAllInvoiceRequestsQuery query)
        { 
            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("{id}/prefill_invoice")]
        public async Task<IActionResult> GetRequestToFillById(int id)
        {
            var query = new GetSaleRequestByIDQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(result.Errors);
            }

            return Ok(result.Value);
        }
        [HttpPost("preview-pdf")]
        public async Task<IActionResult> PreviewInvoiceRequestPDF(int id)
        {
            var query = new PreviewInvoiceHTMLQuery
            {
                RootPath = _env.ContentRootPath,
                RequestId = id
            };
            var result = await _mediator.Send(query);

            if (result.IsFailed)
                return BadRequest(result.Errors);
            return File(result.Value, "application/pdf", $"request_preview_id_{id}.pdf"); ;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
         => Ok(await _mediator.Send(new GetInvoiceRequestByIdQuery(id)));
        [HttpPost("reject")]
        //[Authorize(Roles = "Accountant,Admin")]
        public async Task<IActionResult> Reject([FromBody] RejectInvoiceRequestCommand command)
        => Ok(await _mediator.Send(command));
        [HttpPut("{id}/cancel")]
        public async Task<IActionResult> CancelRequest(int id)
        {
            var command = new CancelInvoiceRequestCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(new { Message = "Đã hủy yêu cầu thành công." });
        }
    }

}
