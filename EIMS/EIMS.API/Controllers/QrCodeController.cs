using EIMS.Application.Features.QRCodes.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrCodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public QrCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateQr([FromBody] GenerateQrCodeCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
    }
}
