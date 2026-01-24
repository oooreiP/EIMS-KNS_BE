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
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Memory;
using EIMS.API.Extensions;
using EIMS.Application.Features.Invoices.Commands.ViewInvoices;
using FluentResults;
using EIMS.Application.Commons.Models;
using EIMS.Application.Features.Invoices.Commands.DeleteInvoice;
namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : BaseApiController
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _env;
        private readonly IMemoryCache _memoryCache;
        public InvoiceController(IMediator mediator, IWebHostEnvironment env, IMapper mapper, IMemoryCache memoryCache)
        {
            _mediator = mediator;
            _env = env;
            _mapper = mapper;
            _memoryCache = memoryCache;
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
        [HttpPost("preview")]
        public async Task<IActionResult> PreviewInvoice([FromBody] PreviewInvoiceCommand command)
        {
            command.RootPath = _env.ContentRootPath;
            var result = await _mediator.Send(command);
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }

            return Content(result.Value, "text/html", Encoding.UTF8);
        }
        // DELETE api/invoices/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(int id)
        {
            var command = new DeleteInvoiceCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                // Trả về 204 No Content (chuẩn cho Delete thành công) hoặc 200 OK kèm message
                return Ok(new { message = "Đã xóa hóa đơn nháp thành công." });
            }

            // Trả về 400 Bad Request kèm lý do (VD: Hóa đơn đã ký không được xóa)
            return BadRequest(result);
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
            var result = await _mediator.Send(new GetInvoiceByIdQuery(id));
            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
        [HttpGet("{id}/original")]
        public async Task<IActionResult> GetOriginalInvoice(int id)
        {
            var query = new GetOriginalInvoiceQuery { ChildInvoiceId = id };
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        [HttpGet("hodInvoices")]
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
        /// <summary>
        /// Lấy danh sách hóa đơn của Sale (có thể lọc theo SaleId cụ thể hoặc lấy tất cả)
        /// GET: api/invoices/sale-assigned?pageIndex=1&pageSize=10&searchTerm=ABC&specificSaleId=5
        /// </summary>
        [HttpGet("sale-assigned")]
        [ProducesResponseType(typeof(PaginatedList<InvoiceDTO>), 200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> GetSaleInvoices([FromQuery] GetSaleInvoicesQuery query)
        {
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) 
                   ?? User.FindFirst("sub")                     
                   ?? User.FindFirst("UserID")                  
                   ?? User.FindFirst("id");

            if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                command.AuthenticatedUserId = userId;
            }
            else
            {
                return Unauthorized(new { title = "Authentication Failed", detail = "User ID not found in token." });
            }
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
                InvoiceId = invoiceId,
                RootPath = _env.ContentRootPath
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
                InvoiceId = id, 
                IssuerId = requestBody.IssuerId,
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
                return Ok($"Cập nhật trạng thái của hoá đơn số {result.Value} thành công");
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
            command.RootPath = _env.ContentRootPath;
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
            command.RootPath = _env.ContentRootPath;
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
            return Ok(new
            {
                InvoiceId = command.InvoiceId,
                DataToSign = result.Value,
                Algorithm = "RSA-SHA256 (SignedInfo C14N)"
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

            command.RootPath = _env.ContentRootPath;

            var result = await _mediator.Send(command);

            if (result.IsFailed) return BadRequest(result.Errors);

            return Ok(new { Message = "Ký số thành công!", InvoiceId = command.InvoiceId });
        }
        [HttpGet("preview-by-invoice/{id}")]
        public async Task<IActionResult> PreviewByInvoiceId(int id)
        {
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
        // [HttpGet("public/lookup/{lookupCode}")]
        // [AllowAnonymous]
        // public async Task<IActionResult> LookupInvoice(string lookupCode)
        // {
        //     string ipAddress = GetClientIpAddress();

        //     var query = new GetInvoiceByLookupCodeQuery
        //     {
        //         LookupCode = lookupCode,
        //         IPAddress = ipAddress,
        //         UserAgent = Request.Headers["User-Agent"].ToString()
        //     };

        //     var result = await _mediator.Send(query);

        //     if (result.IsSuccess)
        //         return Ok(new { success = true, data = result.Value });

        //     return BadRequest(new { success = false, message = result.Errors[0].Message});
        // }
        [HttpGet("lookup/{lookupCode}")]
        public async Task<IActionResult> LookupInvoice(
    string lookupCode,
    [FromHeader(Name = "X-Captcha-ID")] string captchaId,
    [FromHeader(Name = "X-Captcha-Input")] string captchaInput)
        {
            // 1. Kiểm tra ID có trong Cache không
            if (!_memoryCache.TryGetValue(captchaId, out string correctCode))
            {
                return BadRequest("Captcha đã hết hạn hoặc không tồn tại. Vui lòng tải lại.");
            }

            // 2. So sánh mã nhập (Không phân biệt hoa thường)
            if (!string.Equals(correctCode, captchaInput, StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest("Mã Captcha không đúng.");
            }

            // 3. Xóa Captcha sau khi dùng xong (để không dùng lại được)
            _memoryCache.Remove(captchaId);

            // 4. Logic tra cứu cũ...
            var query = new GetInvoiceByLookupCodeQuery { LookupCode = lookupCode };
            var result = await _mediator.Send(query);
            return result.ToActionResult();
        }
    }
}
