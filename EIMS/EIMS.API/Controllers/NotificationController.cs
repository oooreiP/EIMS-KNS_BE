using EIMS.Application.Features.Notifications.Commands;
using EIMS.Application.Features.Notifications.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public NotificationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Lấy danh sách thông báo của tôi (Có phân trang)
        /// </summary>
        /// <param name="pageIndex">Trang số mấy (mặc định 1)</param>
        /// <param name="pageSize">Số dòng/trang (mặc định 10)</param>
        /// <param name="onlyUnread">True: Chỉ lấy tin chưa đọc | Null/False: Lấy tất cả</param>
        [HttpGet]
        public async Task<IActionResult> GetMyNotifications(
            [FromQuery] int pageIndex = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] bool? onlyUnread = null)
        {
            var query = new GetMyNotificationsQuery
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                OnlyUnread = onlyUnread
            };

            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }

        /// <summary>
        /// Đếm số lượng thông báo chưa đọc (Để hiện chấm đỏ trên UI)
        /// </summary>
        [HttpGet("unread-count")]
        public async Task<IActionResult> GetUnreadCount()
        {
            var query = new GetUnreadCountQuery(); // Query mới (xem code bên dưới)
            var result = await _mediator.Send(query);
            return Ok(new { count = result.Value });
        }

        /// <summary>
        /// Đánh dấu 1 thông báo là Đã đọc
        /// </summary>
        [HttpPut("{id}/read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var command = new MarkNotificationReadCommand { NotificationID = id };
            var result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(new { message = "Đã đánh dấu đã đọc." });
        }

        /// <summary>
        /// Đánh dấu TẤT CẢ thông báo là Đã đọc
        /// </summary>
        [HttpPut("read-all")]
        public async Task<IActionResult> MarkAllAsRead()
        {
            var command = new MarkAllReadCommand(); // Command mới (xem code bên dưới)
            var result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Errors);
            return Ok(new { message = "Đã đánh dấu tất cả là đã đọc." });
        }
    }
}
