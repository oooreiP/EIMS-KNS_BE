using EIMS.Application.Features.InvoiceHistories.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceHistoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceHistoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy lịch sử của một hóa đơn cụ thể
        /// </summary>
        [HttpGet("by-invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoice(int invoiceId)
        {
            var result = await _mediator.Send(new GetHistoryByInvoiceIdQuery(invoiceId));
            return Ok(result.Value);
        }

        /// <summary>
        /// Lấy lịch sử thao tác của một nhân viên
        /// </summary>
        [HttpGet("by-user/{userId}")]
        public async Task<IActionResult> GetByUser(int userId)
        {
            var result = await _mediator.Send(new GetHistoryByUserIdQuery(userId));
            return Ok(result.Value);
        }

        /// <summary>
        /// Tìm kiếm lịch sử (Filter)
        /// </summary>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] DateTime? from, [FromQuery] DateTime? to, [FromQuery] string? action)
        {
            var query = new GetHistoryFilterQuery
            {
                FromDate = from,
                ToDate = to,
                ActionType = action
            };
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }
    }
}
