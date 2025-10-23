using EIMS.API.Common;
using EIMS.Application.Features.Authentication.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ApiControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterCommand command)
        {
            try
            {
                var userId = await _mediator.Send(command);
                return Ok(new { Message = "Registration successful", UserId = userId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Message = ex.Message });
            }
        }
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            try
            {
                command.IpAddress = GetIpAddress(); // get ip for auditing
                var response = await _mediator.Send(command);
                //send refresh token by HttpOnly cookie
                SetRefreshTokenCookie(response.RefreshToken);
                //send access token in response body
                return Ok(new { response.AccessToken, response.Email, response.Name, response.Role });
            }
            catch(Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            //get the refresh token from cookie
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { Message = "Refresh token not found" });
            }
            try
            {
                var command = new RefreshTokenCommand
                {
                    RefreshToken = refreshToken,
                    IpAdress = GetIpAddress()
                };
                var response = await _mediator.Send(command);
                //send new refresh token in the cookie
                SetRefreshTokenCookie(response.RefreshToken);
                return Ok(new { response.AccessToken, response.Email, response.Name, response.Role });
            }
            catch (Exception ex)
            {
                return Unauthorized(new { Message = ex.Message });
            }
        }
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _mediator.Send(new LogoutCommand { RefreshToken = refreshToken });
            }
            //clear cookie
            DeleteRefreshTokenCookie();
            return Ok(new { Message = "Logged out successfuly" });
        }
    }
}