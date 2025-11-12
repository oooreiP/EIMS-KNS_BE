using EIMS.Application.DTOs.Admin;
using EIMS.Application.Features.Admin.Commands;
using EIMS.Application.Features.User.Commands;
using EIMS.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        public UserController(ISender sender)
        {
            _sender = sender;
        }
        /// <summary>
        /// Gets all users with the 'HoD' role.
        /// </summary>
        [HttpGet("hod/all")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllHodUsers()
        {
            var result = await _sender.Send(new GetHodUsersQuery());
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Hod Users Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Gets only active users with the 'HoD' role.
        /// </summary>
        [HttpGet("hod/active")]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetActiveHodUsers()
        {
            var query = new GetHodUsersQuery { IsActive = true };
            var result = await _sender.Send(query);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Hod Users Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Gets only inactive users with the 'HoD' role.
        /// </summary>
        [HttpGet("hod/inactive")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetInactiveHodUsers()
        {
            var query = new GetHodUsersQuery { IsActive = false };
            var result = await _sender.Send(query);
            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Hod Users Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a specific 'HoD' user by their ID.
        /// </summary>
        [HttpGet("hod/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetHodUserById(int id)
        {
            var query = new GetHodUserByIdQuery(id);
            var result = await _sender.Send(query);

            if (result.IsFailed)
            {
                var firstError = result.Errors.FirstOrDefault();
                // Check if the error is the specific "Not Found" error
                if (firstError != null && firstError.Metadata.ContainsValue("User.Hod.NotFound"))
                {
                    return NotFound(new ProblemDetails
                    {
                        Status = StatusCodes.Status404NotFound,
                        Title = "Not Found",
                        Detail = firstError.Message
                    });
                }
                
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Hod User Failed",
                    Detail = firstError?.Message ?? "Invalid request."
                });
            }

            return Ok(result.Value);
        }
        /// <summary>
        /// Admin registers a new HoD account. The account is created as inactive, pending evidence upload.
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPost("admin/register-hod")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterHod([FromBody] RegisterHodCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? CreatedAtAction(nameof(GetHodUserById), new { id = result.Value.UserID }, result.Value) : BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        /// <summary>
        /// Admin approves an HoD account after reviewing evidence.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut("admin/hod/{userId}/approve")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ApproveHodAccount(int userId)
        {
            var command = new UpdateHodStatusCommand { UserId = userId, NewStatus = Domain.Enums.UserAccountStatus.Active };
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok("HOD account approved and activated.") : BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        /// <summary>
        /// Admin declines an HoD account registration.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notes">Optional notes for the decline reason.</param>
        /// <returns></returns>
        [HttpPut("admin/hod/{userId}/decline")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeclineHodAccount(int userId, [FromBody] AdminDeclineRequest request) // Create a DTO for request
        {
            var command = new UpdateHodStatusCommand { UserId = userId, NewStatus = Domain.Enums.UserAccountStatus.Declined, AdminNotes = request?.AdminNotes };
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok("HOD account declined.") : BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        /// <summary>
        /// HOD user uploads their evidence file for account activation.
        /// </summary>
        /// <param name="userId">The ID of the HOD user.</param>
        /// <param name="file">The evidence file to upload.</param>
        /// <returns></returns>
        [HttpPost("hod/upload-evidence")]
        [Authorize(Roles = "HOD")]
        public async Task<IActionResult> UploadHodEvidence([FromForm] UploadEvidenceCommand command)
        {
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok("Evidence uploaded successfully, pending admin review.") : BadRequest(result.Errors.FirstOrDefault()?.Message);
        }
    }
}