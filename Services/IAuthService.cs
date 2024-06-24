using apiEmpleados.Models;

namespace apiEmpleados.Services
{
    public interface IAuthService
    {
        Task<string> Register(Usuario usuario);
        Task<string> Login(Usuario usuario);
    }
}
