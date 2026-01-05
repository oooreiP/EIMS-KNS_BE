using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Features.Dashboard.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DashboardController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("customer")]
        public async Task<IActionResult> GetCustomerStats()
        {
            var query = new GetCustomerDashboardQuery();
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
        [HttpGet("admin")]
        public async Task<IActionResult> GetAdminStats()
        {
            var query = new GetAdminDashboardQuery();
            var result = await _mediator.Send(query);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
        [HttpGet("sales")]
        public async Task<IActionResult> GetSalesDashboard()
        {
            var query = new GetSalesDashboardQuery();
            var result = await _mediator.Send(query);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
    }
}