using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using apiEmpleados.Data;
using apiEmpleados.Models;

namespace apiEmpleados.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(AppDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<bool> Register(Usuario usuario)
        {
            if (_context.Usuarios.Any(u => u.usuario == usuario.usuario))
            {
                return false;
            }
            usuario.contraseña = BCrypt.Net.BCrypt.HashPassword(usuario.contraseña);
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return true;
        }

        public string Login(Usuario usuario)
        {
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.usuario == usuario.usuario);
            if (existingUser == null || !BCrypt.Net.BCrypt.Verify(usuario.contraseña, existingUser.contraseña))
            {
                return null;
            }
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, existingUser.id_usuario.ToString()),
                    new Claim(ClaimTypes.Name, existingUser.usuario)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
