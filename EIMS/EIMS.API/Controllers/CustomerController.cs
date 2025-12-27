using EIMS.Application.Features.Customers.Commands;
using EIMS.Application.Features.Customers.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CustomerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchByTaxCode([FromQuery] string taxCode)
        {
            var result = await _mediator.Send(new SearchCustomerByTaxCodeQuery(taxCode));

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        [HttpGet("find")]
        public async Task<IActionResult> FindCustomers([FromQuery] string q)
        {
            if (string.IsNullOrWhiteSpace(q))
                return BadRequest("Search term is required.");

            var result = await _mediator.Send(new SearchCustomerQuery(q));

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        [HttpPost]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                // Return a clear error message
                return BadRequest(new
                {
                    message = "Failed to create customer",
                    errors = result.Errors.Select(e => e.Message)
                });
            }

            return Ok(new { CustomerID = result.Value });
        }
        /// <summary>
        /// Gets a paginated list of customers with optional search.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers([FromQuery] GetCustomersQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(new
                {
                    message = "Failed to retrieve customers",
                    errors = result.Errors.Select(e => e.Message)
                });
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Activates a customer account.
        /// </summary>
        [HttpPut("{id}/active")]
        public async Task<IActionResult> ActivateCustomer(int id)
        {
            var command = new UpdateCustomerStatusCommand
            {
                CustomerId = id,
                IsActive = true
            };

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsKey("ErrorCode") && (string)e.Metadata["ErrorCode"] == "Customer.Status.NotFound"))
                    return NotFound(new { message = result.Errors.First().Message });

                return BadRequest(new { message = result.Errors.First().Message });
            }

            return Ok(new { message = "Customer activated successfully." });
        }

        /// <summary>
        /// Deactivates a customer account.
        /// </summary>
        [HttpPut("{id}/inactive")]
        public async Task<IActionResult> DeactivateCustomer(int id)
        {
            var command = new UpdateCustomerStatusCommand
            {
                CustomerId = id,
                IsActive = false
            };

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsKey("ErrorCode") && (string)e.Metadata["ErrorCode"] == "Customer.Status.NotFound"))
                    return NotFound(new { message = result.Errors.First().Message });

                return BadRequest(new { message = result.Errors.First().Message });
            }

            return Ok(new { message = "Customer deactivated successfully." });
        }
        [HttpGet("debt-summary")]
        public async Task<IActionResult> GetCustomerDebtSummary(
                                            [FromQuery] int pageNumber = 1,
                                            [FromQuery] int pageSize = 10,
                                            [FromQuery] string? search = null,
                                            [FromQuery] string? sortBy = null,
                                            [FromQuery] string sortOrder = "desc",
                                            [FromQuery] bool hasOverdue = false)
        {
            var query = new GetCustomerDebtSummaryQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                SearchTerm = search,
                SortBy = sortBy,
                SortOrder = sortOrder,
                HasOverdue = hasOverdue
            };

            var result = await _mediator.Send(query);

            if (result.IsFailed) return BadRequest(result.Errors);

            return Ok(result.Value);
        }
        [HttpGet("{id}/debt-detail")]
        public async Task<IActionResult> GetCustomerDebtDetail(
    int id,
    [FromQuery] int invoicePageIndex = 1,
    [FromQuery] int invoicePageSize = 10,
    [FromQuery] int paymentPageIndex = 1,
    [FromQuery] int paymentPageSize = 10)
        {
            var query = new GetCustomerDebtDetailQuery
            {
                CustomerId = id,
                InvoicePageIndex = invoicePageIndex,
                InvoicePageSize = invoicePageSize,
                PaymentPageIndex = paymentPageIndex,
                PaymentPageSize = paymentPageSize
            };

            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(new { message = result.Errors.First().Message });
            }

            return Ok(result.Value);
        }
    }
}
