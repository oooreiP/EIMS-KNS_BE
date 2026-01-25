using EIMS.Application.DTOs.Mails;
using EIMS.Application.DTOs.Minutes;
using EIMS.Application.Features.InvoiceStatements.Queries;
using EIMS.Application.Features.Minutes.Commands;
using EIMS.Application.Features.Minutes.Queries;
using FluentResults;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MinuteController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _env;
        public MinuteController(IMediator mediator, IWebHostEnvironment env)
        {
            _mediator = mediator;
            _env = env;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateMinuteWithFileDto dto)
        {
            var command = new CreateMinuteInvoiceCommand(dto);
            var resultId = await _mediator.Send(command);
            return Ok(new { Id = resultId, Message = "Đã upload biên bản lên Cloud và gửi kế toán thành công." });
        }
        // GET: api/minutes/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var query = new GetMinuteInvoiceByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Errors);
        }

        // GET: api/minutes?pageIndex=1&pageSize=10&searchTerm=abc&status=1
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetMinuteInvoicesQuery query)
        {
            var result = await _mediator.Send(query);

            // Vì GetAll hiếm khi lỗi logic, thường chỉ trả về list rỗng
            return Ok(result.Value);
        }

        // GET: api/minute/by-sale/{saleId}?pageIndex=1&pageSize=10&searchTerm=abc&status=1
        [HttpGet("by-sale/{saleId}")]
        public async Task<IActionResult> GetBySaleId(int saleId, [FromQuery] GetMinuteInvoicesQuery query)
        {
            query.SaleId = saleId;
            var result = await _mediator.Send(query);
            return Ok(result.Value);
        }
        [HttpPost("sign-seller/{id}")]
        public async Task<IActionResult> SignBySeller(int id)
        {
            var command = new SignMinuteInvoiceCommand(id, "ĐẠI DIỆN BÊN B", _env.ContentRootPath);

            var result = await _mediator.Send(command);

            if (result.IsFailed)
                return BadRequest(result.Errors);

            return Ok(new { Message = "Đã ký số thành công", NewUrl = result.Value });
        }
        [HttpPut("{id}/file")]
        public async Task<IActionResult> UpdateMinuteFile(int id, IFormFile file)
        {
            var command = new UpdateMinuteFileCommand(id, file);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { FilePath = result.Value });
        }
        [HttpPut("{id}/complete")]
        public async Task<IActionResult> CompleteMinute(int id)
        {
            var command = new CompleteMinuteInvoiceCommand(id);
            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { Message = "Đã cập nhật trạng thái biên bản thành công." });
        }
        [HttpGet("{id}/export-pdf")]
        public async Task<IActionResult> ExportPdf(int id)
        {
            string rootPath = _env.ContentRootPath;
            var query = new GetMinutePdfQuery(id, rootPath);

            Result<FileAttachment> result = await _mediator.Send(query);
            if (result.IsFailed)
            {
                return BadRequest(new { Error = result.Errors.FirstOrDefault()?.Message });
            }

            var fileData = result.Value;
            return File(
                fileData.FileContent,
                "application/pdf",
                fileData.FileName
            );
        }
    }
}
