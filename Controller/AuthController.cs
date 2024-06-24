using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using apiEmpleados.Models;
using apiEmpleados.Services;

namespace apiEmpleados.Controllers
{
    [ApiController]
    [Route("api/user/")]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        private readonly IConfiguration _configuration;

        public AuthController(AuthService authService, IConfiguration configuration)
        {
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario usuario)
        {
            var result = await _authService.Register(usuario);
            if (!result)
            {
                return BadRequest(new { message = "El usuario ya existe" });
            }
            return Ok(new { message = "¡Usuario creado exitosamente!" });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] Usuario usuario)
        {
            var token = _authService.Login(usuario);
            if (token == null)
            {
                return Unauthorized(new { message = "Nombre de usuario o contraseña incorrectos" });
            }
            return Ok(new { token });
        }
    }
}
