using Ecommerce.Api.Data;
using Ecommerce.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;
using System.Threading.Tasks;

namespace Riwi.Api.Services
{
    public class AuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _config;

        public AuthService(ApplicationDbContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        public async Task<LoginResponse> Login(LoginRequest request)
        {
            var user = await _context.Access
                .FirstOrDefaultAsync(x => x.username == request.Username && x.is_active);

            if (user == null)
                return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.password))
                return null;

            var token = GenerateJwtToken(user);

            return new LoginResponse
            {
                Token = token,
                Username = user.username,
                RoleId = user.role_id
            };
        }

        private string GenerateJwtToken(Ecommerce.Api.Models.Access user)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", user.id_access.ToString()),
                new Claim("role", user.role_id.ToString()),
                new Claim(ClaimTypes.Name, user.username)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpireHours"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
