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
        [HttpPost("send-invoice")]
        [ProducesResponseType(typeof(Result), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Result), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> SendInvoiceEmail([FromBody] SendInvoiceEmailCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(result);

            return Ok(Result.Ok().WithSuccess("Email gửi thành công."));
        }
    }
}
