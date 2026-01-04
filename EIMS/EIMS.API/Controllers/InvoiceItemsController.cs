using EIMS.Application.Features.InvoiceItems.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceItemsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceItemsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. GET: api/invoice-items/invoice/123
        // Lấy chi tiết items của một hóa đơn cụ thể
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoiceId(int invoiceId)
        {
            var query = new GetInvoiceItemsByInvoiceIdQuery { InvoiceId = invoiceId };
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }

        // 2. GET: api/invoice-items/customer/456
        // Lấy toàn bộ lịch sử items đã mua của một khách hàng
        [HttpGet("customer/{customerId}")]
        public async Task<IActionResult> GetByCustomerId(int customerId)
        {
            var query = new GetInvoiceItemsByCustomerIdQuery { CustomerId = customerId };
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }
    }
}
