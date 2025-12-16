using Riwi.Api.Data;
using Riwi.Api.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
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

        /// <summary>
        /// Simple email-based login for testing purposes
        /// In production, this should validate a password
        /// </summary>
        public async Task<LoginResponse?> Login(LoginRequest request)
        {
            var person = await _context.Persons
                .FirstOrDefaultAsync(x => x.Email == request.Email);

            if (person == null || !BCrypt.Net.BCrypt.Verify(request.Password, person.Password))
                return null;

            var token = GenerateJwtToken(person);

            return new LoginResponse
            {
                Token = token,
                Email = person.Email,
                PersonId = person.PersonId,
                FullName = person.FullName,
                Role = person.Role.ToString()
            };
        }

        private string GenerateJwtToken(Models.Person person)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim("id", person.PersonId.ToString()),
                new Claim("role", person.Role.ToString()),
                new Claim(ClaimTypes.Name, person.FullName),
                new Claim(ClaimTypes.Email, person.Email)
            };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(double.Parse(_config["Jwt:ExpireHours"] ?? "24")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
