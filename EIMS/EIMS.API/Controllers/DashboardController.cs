using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.API.Extensions;
using EIMS.Application.Features.Dashboard.Queries;
using EIMS.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> GetAdminStats([FromQuery] GetAdminDashboardQuery query)
        {
            // var query = new GetAdminDashboardQuery();
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
        [HttpGet("hod")]
        public async Task<IActionResult> GetHodStats()
        {
            var query = new GetHodDashboardQuery();
            var result = await _mediator.Send(query);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
        [HttpGet("accountant")]
        public async Task<IActionResult> GetAccountantStats()
        {
            var query = new GetAccountantDashboardQuery();
            var result = await _mediator.Send(query);
            if (result.IsFailed)
                return BadRequest(result.Errors);
            return Ok(result.Value);
        }
    }
}