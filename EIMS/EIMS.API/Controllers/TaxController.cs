using EIMS.Application.DTOs.TaxAPIDTO;
using EIMS.Application.Features.CQT.NotifyInvoiceError;
using EIMS.Application.Features.CQT.Queries;
using EIMS.Application.Features.CQT.SubmitInvoice.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaxController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("submit")]
        public async Task<IActionResult> Submit(int invoiceId)
        {
            var result = await _mediator.Send(new SubmitInvoiceToCQTCommand(invoiceId));

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Errors);
        }
        /// <summary>
        /// 1. TẠO TỜ KHAI 04/SS (LƯU NHÁP & UPLOAD XML)
        /// </summary>
        /// <param name="command">Danh sách hóa đơn và lý do sai sót</param>
        /// <returns>NotificationID (Để dùng cho bước Preview và Send)</returns>
        [HttpPost("Create-Form04SS-Draft")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateNotification([FromBody] CreateErrorNotificationCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value); // Trả về ID của tờ khai (NotificationID)
        }
        [HttpPost("{id}/send-form-to-CQT")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public async Task<IActionResult> SendNotification(int id)
        {
            var command = new SendErrorNotificationCommand { NotificationID = id };
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Gửi thành công", referenceId = result.Value });
        }
        // GET api/invoice-error-notifications/5/preview
        [HttpGet("{id}/preview")]
        public async Task<IActionResult> Preview(int id)
        {
            var query = new GetErrorNotificationPreviewQuery { NotificationId = id };
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            // Trả về HTML với Content-Type chuẩn để Browser render được ngay
            return Content(result.Value, "text/html");
        }
        /// <summary>
        /// 4. TẢI FILE PDF (DOWNLOAD)
        /// </summary>
        [HttpGet("{id}/pdf")]
        [ProducesResponseType(typeof(FileContentResult), StatusCodes.Status200OK)]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            var query = new GetErrorNotificationPdfQuery { NotificationId = id };
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            // Trả về file PDF để trình duyệt tải xuống
            string fileName = $"TB04SS_{id}_{DateTime.Now:yyyyMMdd}.pdf";
            return File(result.Value, "application/pdf", fileName);
        }
        private string GetStatusName(int errorType)
        {
            return errorType switch
            {
                1 => "Cancelled",             // Hủy
                2 => "Adjustment_In_Progress",// Điều chỉnh
                3 => "Replacement_In_Progress",// Thay thế
                4 => "Explanation_Sent",      // Giải trình
                _ => "Unknown"
            };
        }
    }
}
