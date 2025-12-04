using EIMS.Application.Features.TaxLogs.Commands;
using EIMS.Application.Features.TaxLogs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxLogsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxLogsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy lịch sử giao dịch thuế của một hóa đơn
        /// </summary>
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoice(int invoiceId)
        {
            var result = await _mediator.Send(new GetTaxLogsByInvoiceQuery { InvoiceId = invoiceId });
            return Ok(result.Value);
        }

        /// <summary>
        /// Xem chi tiết Log (Load XML Request/Response)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTaxLogByIdQuery { Id = id });

            if (result.IsFailed)
                return NotFound(new { message = "Không tìm thấy log." });

            return Ok(result.Value);
        }

        /// <summary>
        /// Tạo log thủ công (Dành cho Dev/Tester debug)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaxLogCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { LogId = result.Value });
        }

        /// <summary>
        /// Xóa log rác
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTaxLogCommand { Id = id });
            if (result.IsSuccess) return Ok();
            return BadRequest(result.Errors);
        }
    }
}
