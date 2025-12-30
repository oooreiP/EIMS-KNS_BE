using EIMS.Application.Features.AuditLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuditController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Xem lịch sử thay đổi dữ liệu (Data Logs)
        /// </summary>
        [HttpGet("data-logs")]
        public async Task<IActionResult> GetDataLogs([FromQuery] GetAuditLogsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }

        /// <summary>
        /// Xem nhật ký hoạt động hệ thống (Activity Logs)
        /// </summary>
        [HttpGet("activity-logs")]
        public async Task<IActionResult> GetActivityLogs([FromQuery] GetSystemActivityLogsQuery query)
        {
            var result = await _mediator.Send(query);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Errors);
        }
    }
}
