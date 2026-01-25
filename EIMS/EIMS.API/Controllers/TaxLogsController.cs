using EIMS.Application.Features.TaxLogs.Commands;
using EIMS.Application.Features.TaxLogs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxLogsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        public TaxLogsController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }
        /// <summary>
        /// Lấy danh sách lịch sử truyền nhận (Không bao gồm nội dung XML chi tiết)
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetTaxApiLogListQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.IsFailed) return BadRequest(result.Errors[0].Message);

            return Ok(result.Value);
        }
        /// <summary>
        /// Lấy lịch sử giao dịch thuế của một hóa đơn
        /// </summary>
        [HttpGet("invoice/{invoiceId}")]
        public async Task<IActionResult> GetByInvoice(int invoiceId)
        {
            var result = await _mediator.Send(new GetTaxLogsByInvoiceQuery { InvoiceId = invoiceId });
            return Ok(result.Value);
        }

        /// <summary>
        /// Xem chi tiết Log (Load XML Request/Response)
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _mediator.Send(new GetTaxLogByIdQuery { TaxLogID = id });

            if (result.IsFailed)
                return NotFound(new { message = "Không tìm thấy log." });

            return Ok(result.Value);
        }

        /// <summary>
        /// Tạo log thủ công (Dành cho Dev/Tester debug)
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTaxLogCommand command)
        {
            var result = await _mediator.Send(command);
            return Ok(new { LogId = result.Value });
        }

        /// <summary>
        /// Xóa log rác
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteTaxLogCommand { Id = id });
            if (result.IsSuccess) return Ok();
            return BadRequest(result.Errors);
        }
        /// <summary>
        /// Xem chi tiết Log (Hỗ trợ view đẹp hoặc view raw)
        /// </summary>
        /// <param name="id">ID của Log</param>
        /// <param name="type">'request' hoặc 'response'</param>
        /// <param name="viewHtml">true: Xem đẹp (Mặc định), false: Xem raw xml</param>
        [HttpGet("{id}/view")]
        public async Task<IActionResult> ViewLogContent(int id, [FromQuery] string Type ="Response", [FromQuery] bool viewHtml = true)
        {
            string rootPath = _env.ContentRootPath;
            var query = new GetLogHtmlViewQuery(id, Type, viewHtml, rootPath);
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return NotFound(result.Errors[0].Message);
            }

            string content = result.Value;

            // XỬ LÝ CONTENT-TYPE TRẢ VỀ
            if (viewHtml)
            {
                // Trả về HTML để trình duyệt render giao diện
                return Content(content, "text/html", Encoding.UTF8);
            }
            else
            {
                string contentType = content.Trim().StartsWith("<") ? "text/xml" : "text/plain";
                return Content(content, contentType, Encoding.UTF8);
            }
        }
    }
}
