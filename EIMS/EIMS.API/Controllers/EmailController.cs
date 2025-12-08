using EIMS.Application.Features.Emails.Commands;
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
    }
}
