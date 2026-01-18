using EIMS.Application.Features.Emails.Commands;
using EIMS.Application.Features.Emails.Queries;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmailController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gửi email hóa đơn kèm file PDF/XML từ Cloudinary.
        /// </summary>
        [HttpPost("{invoiceId}/send-email")]
        public async Task<IActionResult> SendInvoiceEmail(int invoiceId, [FromBody] SendInvoiceEmailCommand command)
        {
            command.InvoiceId = invoiceId;
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(new { success = false, message = result.Errors[0].Message });

            return Ok(new
            {
                success = true,
                message = "Email đã được gửi thành công",
                sentTo = command.RecipientEmail ?? "customer-default@email.com",
                sentAt = DateTime.UtcNow
            });
        }
        [HttpPost("{invoiceId}/send-minutes")]
        public async Task<IActionResult> SendMinutes(int invoiceId, [FromBody] SendInvoiceMinutesCommand command)
        {
            command.InvoiceId = invoiceId;
            var result = await _mediator.Send(command);
            return result.IsSuccess ? Ok(Result.Ok("Đã gửi biên bản thành công.")) : BadRequest(result);
        }
        [HttpPost("preview-minutes")]
        public async Task<IActionResult> PreviewMinutes([FromBody] PreviewInvoiceMinutesQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return File(
                result.Value.FileContent,
                "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
                result.Value.FileName
            );
        }
    }
}
