using AutoMapper;
using EIMS.Application.DTOs;
using EIMS.Application.DTOs.Invoices;
using EIMS.Application.Features.Invoices.Commands.AdjustInvoice;
using EIMS.Application.Features.Invoices.Commands.CreateInvoice;
using EIMS.Application.Features.Invoices.Commands.IssueInvoice;
using EIMS.Application.Features.Invoices.Commands.ReplaceInvoice;
using EIMS.Application.Features.Invoices.Commands.SignInvoice;
using EIMS.Application.Features.Invoices.Commands.UpdateInvoice;
using EIMS.Application.Features.Invoices.Queries;
using EIMS.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Text;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        public InvoiceController(IMediator mediator, IWebHostEnvironment env, IMapper mapper)
        {
            _mediator = mediator;
            _env = env;
            _mapper = mapper;
        }

        [HttpPost]
        [ProducesResponseType(typeof(Invoice), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateInvoice([FromBody] CreateInvoiceCommand command)
        {
            var result = await _mediator.Send(command);
            if (result.IsFailed)
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
        public async Task<IActionResult> GetAll([FromQuery] GetAllInvoicesQuery query)
        {
            var invoices = await _mediator.Send(query);
            return Ok(invoices);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var invoice = await _mediator.Send(new GetInvoiceByIdQuery(id));
            return invoice != null ? Ok(invoice) : NotFound();
        }
        [HttpGet("published")]
        public async Task<IActionResult> GetPublishedInvoices([FromQuery] GetInvoicesHodQuery query)
        {
            if (string.IsNullOrEmpty(query.SortColumn))
            {
                query.SortColumn = "date";
                query.SortDirection = "desc";
            }

            var result = await _mediator.Send(query);
            return Ok(result);
        }
        [HttpPut("draft/{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, [FromBody] UpdateInvoiceRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid request body.");
            }

            var command = _mapper.Map<UpdateInvoiceCommand>(request);
            command.InvoiceId = id;

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { InvoiceId = result.Value });
            }

            return BadRequest(result.Errors);
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
                return Ok(new
                {
                    invoiceNumber = result.Value,
                    message = $"Đã kích hoạt ký số thành công. Số hóa đơn: {result.Value}"
                });
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
        public async Task<IActionResult> IssueInvoice(int id, [FromBody] IssueInvoiceRequest requestBody)
        {
            var command = new IssueInvoiceCommand
            {
                InvoiceId = id, // Lấy từ URL
                IssuerId = requestBody.IssuerId,

                // Map các thông tin thanh toán mới
                AutoCreatePayment = requestBody.AutoCreatePayment,
                PaymentAmount = requestBody.PaymentAmount,
                PaymentMethod = requestBody.PaymentMethod,
                Note = requestBody.Note
            };
            var result = await _mediator.Send(command);
            if (result.IsSuccess)
            {
                var successMsg = result.Successes.FirstOrDefault()?.Message
                                 ?? "Phát hành hóa đơn thành công. Trạng thái: Issued.";
                return Ok(new
                {
                    message = successMsg,
                    invoiceId = id
                });
            }
            // 4. Trả về lỗi
            return BadRequest(new
            {
                message = "Không thể phát hành hóa đơn.",
                errors = result.Errors.Select(e => e.Message)
            });
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoiceStatus(int id, int statusId)
        {
            var command = new Application.Features.Invoices.Commands.ChangeInvoiceStatus.UpdateInvoiceStatusCommand { InvoiceId = id, InvoiceStatusId = statusId };
            var result = await _mediator.Send(command);
            {
                if (result.IsFailed)
                {
                    return BadRequest(new ProblemDetails
                    {
                        Status = StatusCodes.Status400BadRequest,
                        Title = "Update Invoice Status Failed",
                        Detail = result.Errors.FirstOrDefault()?.Message ?? "Invalid request."
                    });
                }
                return Ok(result.Value);
            }
        }
        /// <summary>
        /// Tạo hóa đơn điều chỉnh (Adjustment Invoice).
        /// </summary>
        /// <remarks>
        /// Lưu ý quan trọng:
        /// - Nếu điều chỉnh GIẢM: Nhập Quantity là số ÂM (ví dụ: -2).
        /// - Nếu điều chỉnh THÔNG TIN (MST/Tên): Để trống danh sách AdjustmentItems, điền NewCustomerId.
        /// - Hóa đơn gốc phải ở trạng thái Đã phát hành (Issued).
        /// </remarks>
        /// <param name="command">Thông tin điều chỉnh</param>
        /// <returns>ID của hóa đơn điều chỉnh vừa tạo</returns>
        [HttpPost("adjustment")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAdjustment([FromBody] CreateAdjustmentInvoiceCommand command)
        {
            // Gửi command sang Handler xử lý
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new
                {
                    success = true,
                    invoiceId = result.Value,
                    message = "Tạo hóa đơn điều chỉnh thành công."
                });
            }
            return BadRequest(new
            {
                success = false,
                message = result.Errors.FirstOrDefault()?.Message,
                errors = result.Errors.Select(e => e.Message)
            });
        }
        /// <summary>
        /// Tạo hóa đơn thay thế (Replacement Invoice).
        /// </summary>
        /// <remarks>
        /// **Cơ chế hoạt động:**
        /// 1. Nếu `Items` có dữ liệu: Hệ thống sẽ dùng danh sách hàng hóa mới này.
        /// 2. Nếu `Items` rỗng hoặc null: Hệ thống sẽ **tự động sao chép** toàn bộ hàng hóa từ hóa đơn gốc sang.
        /// 3. Tương tự với `CustomerId`, `Note`: Nếu gửi null sẽ giữ nguyên thông tin cũ.
        /// 
        /// **Lưu ý:** Hóa đơn gốc phải ở trạng thái Đã phát hành (Issued - 6).
        /// </remarks>
        /// <param name="command">Thông tin thay thế</param>
        /// <returns>ID hóa đơn mới vừa tạo</returns>
        [HttpPost("replacement")]
        [ProducesResponseType(typeof(int), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateReplacement([FromBody] CreateReplacementInvoiceCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new
                {
                    success = true,
                    invoiceId = result.Value,
                    message = "Tạo hóa đơn thay thế thành công. Vui lòng ký và phát hành."
                });
            }

            return BadRequest(new
            {
                success = false,
                errors = result.Errors.Select(e => e.Message)
            });
        }
        /// <summary>
        /// Cập nhật trạng thái hóa đơn thủ công (Dành cho Admin/Xử lý sự cố)
        /// </summary>
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] Application.Features.Invoices.Commands.UpdateStatus.UpdateInvoiceStatusCommand command)
        {
            if (id != command.InvoiceId)
            {
                return BadRequest("ID trên URL không khớp với ID trong body.");
            }

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { message = "Cập nhật trạng thái thành công." });
            }

            return BadRequest(new { errors = result.Errors.Select(e => e.Message) });
        }
        [HttpPost("get-hash")]
        public async Task<IActionResult> GetHashToSign([FromBody] GetHashToSignCommand command)
        {
            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(new { Error = result.Errors[0].Message });

            // Trả về Hash và ID hóa đơn
            return Ok(new
            {
                InvoiceId = command.InvoiceId,
                HashToSign = result.Value,
                Algorithm = "SHA256" // Báo cho FE biết thuật toán hash
            });
        }

        /// <summary>
        /// Bước 2: Hoàn tất ký (Client gửi Chữ ký + Cert lên)
        /// </summary>
        [HttpPost("complete_signing")]
        public async Task<IActionResult> CompleteSigning([FromBody] CompleteInvoiceSigningCommand command)
        {
            // Validate input cơ bản
            if (string.IsNullOrEmpty(command.SignatureBase64) || string.IsNullOrEmpty(command.CertificateBase64))
            {
                return BadRequest("Vui lòng cung cấp Chữ ký số và Thông tin chứng thư.");
            }

            var result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Errors);

            return Ok(new { Message = "Ký số thành công!", InvoiceId = command.InvoiceId });
        }
        [HttpGet("preview-by-invoice/{id}")]
        public async Task<IActionResult> PreviewByInvoiceId(int id)
        {
            // Truyền đường dẫn gốc vào Query
            var query = new GetInvoiceHtmlViewQuery(id, _env.ContentRootPath);
            var result = await _mediator.Send(query);

            if (result.IsFailed) return NotFound(result.Errors[0].Message);
            return Content(result.Value, "text/html", Encoding.UTF8);
        }
        /// <summary>
        /// Tải file PDF của hóa đơn (Convert từ XML)
        /// GET: api/invoices/{id}/pdf
        /// </summary>
        [HttpGet("{id}/pdf")]
        public async Task<IActionResult> DownloadPdf(int id)
        {
            string rootPath = _env.ContentRootPath;
            var query = new GetInvoicePdfQuery(id, rootPath);
            var result = await _mediator.Send(query);
            if (result.IsFailed)
            {
                return BadRequest(new { message = result.Errors[0].Message });
            }
            var pdfDto = result.Value;
            return File(
                pdfDto.FileContent,
                "application/pdf",
                pdfDto.FileName
            );
        }
        [HttpGet("user")]
        public async Task<IActionResult> GetMyInvoices([FromQuery] GetInvoicesOfUser query)
        {
            // 1. Read the claim you added
            var claim = User.FindFirst("CustomerId");
            if (claim != null && int.TryParse(claim.Value, out int cid))
            {
                query.CustomerId = cid;
            }

            // 2. Send
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}
