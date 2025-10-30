using System.Security.Authentication;
using AutoMapper;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs.Authentication;
using EIMS.Application.Features.Authentication.Commands;
using EIMS.Application.Features.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EIMS.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ISender _sender;
        private readonly IAuthCookieService _authCookieService;

        public AuthController(ISender sender, IAuthCookieService authCookieService, IMapper mapper)
        {
            _sender = sender;
            _authCookieService = authCookieService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var command = _mapper.Map<LoginCommand>(request);
            var loginResult = await _sender.Send(command);
            if (loginResult.IsFailed)
            {
                var firstError = loginResult.Errors.FirstOrDefault();
                // Return error response
                return Unauthorized(new ProblemDetails // Use ProblemDetails for standard error responses
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Authentication Failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided." // Use the message from the Result
                });
            }
            var loginResponse = loginResult.Value;
            _authCookieService.SetRefreshTokenCookie(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiry);
            _authCookieService.SetRefreshTokenCookie(loginResponse.RefreshToken, loginResponse.RefreshTokenExpiry);
            // var authResponse = new AuthResponse
            // {
            //     UserID = loginResponse.UserID,
            //     FullName = loginResponse.FullName,
            //     Email = loginResponse.Email,
            //     Role = loginResponse.Role,
            //     AccessToken = loginResponse.AccessToken
            // };
            var authResponse = _mapper.Map<AuthResponse>(loginResponse);
            return Ok(authResponse);
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            // 1. Get refresh token from cookie
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new ProblemDetails 
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Authentication Failed",
                    Detail = "Invalid credentials provided." 
                });
            }
            var refreshTokenResult = await _sender.Send(new RefreshTokenCommand
            {
                RefreshToken = refreshToken
            });
            var response = refreshTokenResult.Value;
            // Set the new refresh token in the cookie using the new response fields
            _authCookieService.SetRefreshTokenCookie(response.NewRefreshToken, response.NewRefreshTokenExpiry);

            // var authResponseToReturn = new AuthResponse
            // {
            //     UserID = response.UserID,
            //     FullName = response.FullName,
            //     Email = response.Email,
            //     Role = response.Role,
            //     AccessToken = response.AccessToken
            // };
            var authResponseToReturn = _mapper.Map<AuthResponse>(response);

            return Ok(authResponseToReturn);
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            var command = _mapper.Map<RegisterCommand>(request);
            var registerResult = await _sender.Send(command);
            if (registerResult.IsFailed)
            {
                var firstError = registerResult.Errors.FirstOrDefault();
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status401Unauthorized,
                    Title = "Register Failed",
                    Detail = firstError?.Message ?? "Invalid credentials provided." // Use the message from the Result
                });
            }
            return StatusCode(StatusCodes.Status201Created, "User creation successfully");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // 1. Get the refresh token from the cookie
            var refreshToken = Request.Cookies["refreshToken"];

            // 2. Invalidate the token in the database (if found)
            if (!string.IsNullOrEmpty(refreshToken))
            {
                await _sender.Send(new LogoutCommand { RefreshToken = refreshToken });
            }
            // 3. Clear the refresh token cookie
            _authCookieService.ClearRefreshTokenCookie();
            return Ok(new { message = "Logout successful" });
        }
    }
}