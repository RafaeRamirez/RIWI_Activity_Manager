using Microsoft.AspNetCore.Mvc;
using Riwi.Api.Dtos;
using Riwi.Api.Services;
using System.Threading.Tasks;

namespace Riwi.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _auth;

        public AuthController(AuthService auth)
        {
            _auth = auth;
        }

        /*
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var result = await _auth.Login(request);

            if (result == null)
                return Unauthorized(new { message = "Credenciales incorrectas" });

            return Ok(result);
        }
        */
    }
}