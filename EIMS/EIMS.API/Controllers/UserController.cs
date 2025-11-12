using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet.Actions;
using EIMS.Application.Features.User.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Admin")]
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
    }
}