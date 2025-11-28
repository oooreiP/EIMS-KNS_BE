using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using EIMS.Application.Features.Invoices.Commands.IssueInvoice;
using EIMS.Application.Features.Invoices.Commands.SignInvoice;
using EIMS.Application.Features.Invoices.Queries;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        public InvoiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            var result = await _mediator.Send(command);
            if(result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Invoice creation failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided."
                });
            }
            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _mediator.Send(new GetAllInvoicesQuery());
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery(id));
            return invoice != null ? Ok(invoice) : NotFound();
        }
        /// <summary>
        /// Kích hoạt quy trình ký số hóa đơn điện tử
        /// </summary>
        /// <param name="invoiceId">ID hóa đơn cần ký</param>
        /// <returns>HTTP 200 OK nếu ký thành công, hoặc HTTP 400/500 nếu thất bại</returns>
        [HttpPost("{invoiceId}/sign")] // Endpoint: POST api/invoices/{invoiceId}/sign
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> SignInvoice(int invoiceId)
        {
            // 1. Tạo Command
            var command = new SignInvoiceCommand
            {
                InvoiceId = invoiceId
                // Lưu ý: Nếu cần, có thể thêm CertificateSerial hoặc SecretPin vào đây
            };

            // 2. Gửi Command qua Mediator
            var result = await _mediator.Send(command);

            // 3. Xử lý kết quả trả về
            if (result.IsSuccess)
            {
                return Ok(new { message = $"Đã kích hoạt ký số thành công cho hóa đơn ID: {invoiceId}. Trạng thái đang được cập nhật." });
            }
            else
            {
                // Trả về HTTP 400 Bad Request kèm thông báo lỗi cụ thể
                // Ví dụ: Không tìm thấy file XML, lỗi kết nối Cert, hoặc Cert hết hạn
                return BadRequest(new
                {
                    message = "Ký số thất bại.",
                    errors = result.Errors.Select(e => e.Message)
                });
            }
        }
        [HttpPost("{id}/issue")]
        public async Task<IActionResult> IssueInvoice(int id)
        {
            var command = new IssueInvoiceCommand { InvoiceId = id };
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { message = "Phát hành hóa đơn thành công. Trạng thái: Issued." });
            }

            return BadRequest(new
            {
                message = "Không thể phát hành hóa đơn.",
                errors = result.Errors.Select(e => e.Message)
            });
        }
    }
}
