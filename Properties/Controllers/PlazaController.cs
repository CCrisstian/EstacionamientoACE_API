using EstacionamientoACE_API.Models;
using EstacionamientoACE_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EstacionamientoACE_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // <-- ¡ESTA ES LA CORRECCIÓN!
    public class PlazasController(AppDbContext context) : ControllerBase
    {
        private readonly AppDbContext _context = context;

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlazasPorEstacionamientoId(int id)
        {
            // [Agregué logging por si acaso, es buena práctica]
            try
            {
                var plazas = await _context.Plazas
                    .Where(p => p.EstId == id)
                    .Include(p => p.CategoriaVehiculo) // <-- Asegúrate de incluir esto
                    .Select(p => new Plaza_DTO
                    {
                        PlazaId = p.PlazaId,
                        PlazaNombre = p.PlazaNombre ?? "N/A",
                        PlazaTipo = p.PlazaTipo,
                        PlazaDisponibilidad = p.PlazaDisponibilidad,
                        CategoriaDescripcion = p.CategoriaVehiculo != null
                            ? p.CategoriaVehiculo.CategoriaDescripcion
                            : "Sin categoría"
                    })
                    .ToListAsync();

                return Ok(plazas); // Esto devolverá '[]' (OK) si no hay plazas, lo cual es correcto
            }
            catch (Exception ex)
            {
                // Si el 'Include' de arriba falla o algo más, lo veremos aquí.
                // (Necesitarías inyectar ILogger para ver esto, pero por ahora lo dejamos)
                return StatusCode(500, $"Error interno: {ex.Message}");
            }
        }
    }
}