using System.Security.Claims;
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
        /// Gets all users with the 'HoD' role (Paginated).
        /// </summary>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllHodUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetHodUsersQuery 
            { 
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };
            
            var result = await _sender.Send(query);
            
            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Hod Users Failed",
                    Detail = result.Errors.FirstOrDefault()?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
       /// <summary>
        /// Gets only active users with the 'HoD' role (Paginated).
        /// </summary>
        [HttpGet("active")]
        public async Task<IActionResult> GetActiveHodUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetHodUsersQuery 
            { 
                IsActive = true, 
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };
            
            var result = await _sender.Send(query);
            
            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Hod Users Failed",
                    Detail = result.Errors.FirstOrDefault()?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }
        /// <summary>
        /// Gets only inactive users with the 'HoD' role (Paginated).
        /// </summary>
        [HttpGet("inactive")]
        public async Task<IActionResult> GetInactiveHodUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetHodUsersQuery 
            { 
                IsActive = false, 
                PageNumber = pageNumber, 
                PageSize = pageSize 
            };
            
            var result = await _sender.Send(query);
            
            if (result.IsFailed)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Get Users Failed",
                    Detail = result.Errors.FirstOrDefault()?.Message ?? "Invalid request."
                });
            }
            return Ok(result.Value);
        }

        /// <summary>
        /// Gets a specific 'HoD' user by their ID.
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);
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
                    Title = "Get Users Failed",
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
        // [HttpPost("admin/register-hod")]
        // [Authorize(Roles = "Admin")]
        // public async Task<IActionResult> RegisterHod([FromBody] RegisterHodCommand command)
        // {
        //     var result = await _sender.Send(command);
        //     return result.IsSuccess ? CreatedAtAction(nameof(GetHodUserById), new { id = result.Value.UserID }, result.Value) : BadRequest(result.Errors.FirstOrDefault()?.Message);
        // }

        /// <summary>
        /// Admin approves an HoD account after reviewing evidence.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpPut("admin/{userId}/active")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ActiveAccount(int userId)
        {
            var command = new UpdateUserStatusCommand { UserId = userId, NewStatus = true };
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok("Account approved and activated.") : BadRequest(result.Errors.FirstOrDefault()?.Message);
        }

        /// <summary>
        /// Admin declines an HoD account registration.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="notes">Optional notes for the decline reason.</param>
        /// <returns></returns>
        [HttpPut("admin/{userId}/inactive")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> IncticveAccount(int userId, [FromBody] AdminDeclineRequest request) // Create a DTO for request
        {
            var command = new UpdateUserStatusCommand { UserId = userId, NewStatus = false, AdminNotes = request?.AdminNotes };
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok("Account declined.") : BadRequest(result.Errors.FirstOrDefault()?.Message);
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
        /// <summary>
        /// Gets a paginated list of all users with optional search and filtering.
        /// </summary>
        [HttpGet("users")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetUsersQuery query)
        {
            var result = await _sender.Send(query);
            
            if (result.IsFailed)
            {
                return BadRequest(result.Errors.FirstOrDefault()?.Message);
            }
            return Ok(result.Value);
        }
        // GET: api/users/profile
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile([FromQuery] GetMyProfileQuery request)
        {
            // var result = await _sender.Send(request);
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier) // Standard .NET
                   ?? User.FindFirst("sub")                     // Standard JWT
                   ?? User.FindFirst("UserID")                  // Custom
                   ?? User.FindFirst("id");
             if (userIdClaim != null && int.TryParse(userIdClaim.Value, out int userId))
            {
                request.AuthenticatedUserId = userId;
            }
            else
            {
                return Unauthorized(new { title = "Authentication Failed", detail = "User ID not found in token." });
            }
            var result = await _sender.Send(request);

            if (result.IsSuccess)
            {
                return Ok(new { InvoiceId = result.Value });
            }

            return BadRequest(result.Errors);
            // if (result.IsSuccess) return Ok(result.Value);
            // return NotFound(result.Errors);
        }

        // PUT: api/users/profile
        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileCommand command)
        {
            var result = await _sender.Send(command);

            if (result.IsSuccess) return NoContent();
            return BadRequest(result.Errors);
        }
    }
}