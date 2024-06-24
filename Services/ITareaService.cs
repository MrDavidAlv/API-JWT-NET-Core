using apiEmpleados.Models;

namespace apiEmpleados.Services
{
    public interface ITareaService
    {
        Task<string> CrearTarea(Tarea tarea);
        Task<List<Tarea>> ObtenerTareas(int userId);
    }
}
