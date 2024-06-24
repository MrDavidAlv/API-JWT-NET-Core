using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using apiEmpleados.Data;
using apiEmpleados.Models;

namespace apiEmpleados.Services
{
    public class TareaService
    {
        private readonly AppDbContext _context;

        public TareaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTask(Tarea tarea, int userId)
        {
            tarea.id_usuario = userId;
            _context.Tareas.Add(tarea);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Tarea>> GetTasks(int userId)
        {
            return await _context.Tareas.Where(t => t.id_usuario == userId).ToListAsync();
        }
    }
}
