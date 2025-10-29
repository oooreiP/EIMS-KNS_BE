using System.Security.Authentication;
using EIMS.Application.Commons.Interfaces;
using EIMS.Application.DTOs;
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
        private readonly ISender _sender;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IApplicationDBContext _context;

        public AuthController(ISender sender, IJwtTokenGenerator jwtTokenGenerator, IApplicationDBContext context)
        {
            _sender = sender;
            _jwtTokenGenerator = jwtTokenGenerator;
            _context = context;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var authResponse = await _sender.Send(new LoginCommand
                {
                    Email = request.Email,
                    Password = request.Password
                });

                // Generate a refresh token
                var refreshToken = _jwtTokenGenerator.GenerateRefreshToken(authResponse.UserID);

                // Save refresh token to DB
                _context.RefreshTokens.Add(refreshToken);
                await _context.SaveChangesAsync(default);

                // Set refresh token in HttpOnly, Secure cookie
                SetRefreshTokenCookie(refreshToken.Token, refreshToken.Expires);

                return Ok(authResponse);
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshToken()
        {
            // 1. Get refresh token from cookie
            var refreshToken = Request.Cookies["refreshToken"];
            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new { message = "Invalid refresh token." });
            }

            try
            {
                var response = await _sender.Send(new RefreshTokenCommand
                {
                    RefreshToken = refreshToken
                });

                // Set the NEW refresh token in the cookie using the new response fields
                SetRefreshTokenCookie(response.NewRefreshToken, response.NewRefreshTokenExpiry);

                // Create the simpler AuthResponse to return to the client
                var authResponseToReturn = new AuthResponse
                {
                    UserID = response.UserID, // Corrected casing
                    FullName = response.FullName,
                    Email = response.Email,
                    Role = response.Role,
                    AccessToken = response.AccessToken
                };
                // --- END: Modify these lines ---

                return Ok(authResponseToReturn);
            }
            catch (AuthenticationException ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var command = new RegisterCommand
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    Password = request.Password,
                    PhoneNumber = request.PhoneNumber
                };

                var userId = await _sender.Send(command);

                return StatusCode(StatusCodes.Status201Created, new { UserId = userId });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during registration: {ex}");
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "An error occurred during registration." });
            }
        }

        private void SetRefreshTokenCookie(string token, DateTime expires)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true, // Prevents client-side script access (XSS)
                Secure = true, // Ensures cookie is sent over HTTPS
                SameSite = SameSiteMode.Strict, // Prevents CSRF
                Expires = expires
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}