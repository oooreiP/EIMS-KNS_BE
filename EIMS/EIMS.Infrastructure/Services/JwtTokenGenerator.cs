using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EIMS.Application.Common.Interfaces;
using EIMS.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Security.Cryptography;

namespace EIMS.Infrastructure.Services
{
    public class JwtTokenGenerator : IJwtTokenGenerator
    {
        private readonly IConfiguration _configuration;
        public JwtTokenGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string CreateAccessToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) //jwt id
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Secrect"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //set time for access token
            var expires = DateTime.UtcNow.AddMinutes(120);
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string CreateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}