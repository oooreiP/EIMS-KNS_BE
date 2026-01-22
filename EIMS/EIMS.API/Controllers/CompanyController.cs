using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EIMS.Application.Features.Company.Commands;
using EIMS.Application.Features.Company.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCompanyById(int id)
        {
            var query = new GetCompanyByIdQuery(id);
            var result = await _mediator.Send(query);

            if (result.IsFailed)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsKey("ErrorCode") && (string)e.Metadata["ErrorCode"] == "Company.Get.NotFound"))
                    return NotFound(result.Errors);

                return BadRequest(result.Errors);
            }

            return Ok(result.Value);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCompany(int id, [FromBody] UpdateCompanyCommand command)
        {
            if (id != command.CompanyID)
            {
                return BadRequest("ID mismatch");
            }

            var result = await _mediator.Send(command);

            if (result.IsFailed)
            {
                if (result.Errors.Any(e => e.Metadata.ContainsKey("ErrorCode") && (string)e.Metadata["ErrorCode"] == "Company.Update.NotFound"))
                    return NotFound(result.Errors);

                return BadRequest(result.Errors);
            }
            return Ok(result.Value);
        }
        [HttpPost("upload-signature")]
        public async Task<IActionResult> UploadSignature([FromForm] UploadSignatureCommand command)
        {
            command.CompanyId = 1;

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }
        [HttpPost("{id}/logo")]
        public async Task<IActionResult> UploadCompanyLogo(int id, [FromForm] UploadCompanyLogoCommand command)
        {
            command.CompanyId = id;

            var result = await _mediator.Send(command);

            if (result.IsSuccess)
            {
                return Ok(new { Url = result.Value });
            }

            return BadRequest(result.Errors);
        }
    }
}