using EIMS.Application.Features.ErrorNotifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceErrorNotificationsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public InvoiceErrorNotificationsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy danh sách Thông báo sai sót (Có phân trang, lọc theo trạng thái, ngày tháng)
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /api/invoice-error-notifications?pageNumber=1&pageSize=10&status=1&keyword=TB001
        /// </remarks>
        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetErrorNotificationListQuery query)
        {
            // [FromQuery] giúp tự động map các tham số trên URL (?page=1&status=1) vào object Query
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value); // Trả về PagedResult
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        /// <summary>
        /// Lấy chi tiết một Thông báo sai sót theo ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetErrorNotificationByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
            {
                return Ok(result.Value); // Trả về DTO chi tiết
            }

            // Nếu lỗi là do không tìm thấy
            if (result.Errors.Any(e => e.Message.Contains("không tìm thấy")))
            {
                return NotFound(result.Errors.FirstOrDefault()?.Message);
            }

            return BadRequest(result.Errors.FirstOrDefault()?.Message);
        }
    }
}
