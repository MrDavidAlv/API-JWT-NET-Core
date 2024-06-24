using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using apiEmpleados.Models;
using apiEmpleados.Services;

namespace apiEmpleados.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/tasks/")]
    public class TareasController : ControllerBase
    {
        private readonly TareaService _tareaService;

        public TareasController(TareaService tareaService)
        {
            _tareaService = tareaService;
        }

        [HttpPost("tasks")]
        public async Task<IActionResult> CreateTask([FromBody] Tarea tarea)
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var result = await _tareaService.CreateTask(tarea, userId);
            if (!result)
            {
                return BadRequest(new { message = "No se pudo crear la tarea" });
            }
            return Ok(new { message = "¡Tarea creada exitosamente!" });
        }

      
        [HttpGet("tasks")]
        public async Task<IActionResult> GetTasks()
        {
            var userId = int.Parse(User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value);
            var tasks = await _tareaService.GetTasks(userId);
            return Ok(tasks);
        }
    }
}
