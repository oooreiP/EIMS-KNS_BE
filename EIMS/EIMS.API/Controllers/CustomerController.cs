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
    }
}
