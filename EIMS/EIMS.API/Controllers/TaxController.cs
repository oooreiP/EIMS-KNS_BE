using EIMS.Application.DTOs.TaxAPIDTO;
using EIMS.Application.Features.CQT.NotifyInvoiceError;
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
        /// Gửi thông báo sai sót (Mẫu 04/SS-HĐĐT) lên Cơ quan Thuế
        /// </summary>
        /// <param name="invoiceId">ID hóa đơn bị sai</param>
        /// <param name="request">Thông tin sai sót</param>
        [HttpPost("{invoiceId}/notify-error")]
        public async Task<IActionResult> NotifyInvoiceError(int invoiceId, [FromBody] NotifyErrorRequest request)
        {
            var command = new NotifyInvoiceErrorCommand
            {
                InvoiceId = invoiceId,
                ErrorType = request.ErrorType,
                Reason = request.Reason,
            };

            // 2. Gửi Command qua Mediator
            var result = await _mediator.Send(command);

            // 3. Xử lý kết quả
            if (result.IsSuccess)
            {
                return Ok(new
                {
                    message = "Gửi thông báo sai sót thành công. CQT đã tiếp nhận.",
                    status = GetStatusName(request.ErrorType)
                });
            }

            // Trả về lỗi nếu thất bại (CQT từ chối, chưa cấp mã, v.v.)
            return BadRequest(new
            {
                message = "Gửi thông báo thất bại.",
                errors = result.Errors.Select(e => e.Message)
            });
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
