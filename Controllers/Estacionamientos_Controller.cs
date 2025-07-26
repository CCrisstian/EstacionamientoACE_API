using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstacionamientoACE_API.Models;

namespace EstacionamientoACE_API.Controllers
{
    [ApiController]
    [Route("estacionamientos")]
    public class Estacionamientos_Controller : ControllerBase
    {
        // Inyección de dependencias para el contexto de la Base de Datos
        private readonly AppDbContext _context;

        public Estacionamientos_Controller(AppDbContext context)
        {
            _context = context;
        }

        // HTTP GET: /estacionamientos
        [HttpGet] // Endpoint para obtener todos los Estacionamientos con sus coordenadas
        public async Task<IActionResult> GetEstacionamientos()
        {
            var estacionamientos = await _context.Estacionamientos
                .Where(e => e.EstLatitud != null && e.EstLongitud != null)
                .Select(e => new
                {
                    nombre = e.EstNombre,
                    latitud = e.EstLatitud,
                    longitud = e.EstLongitud
                })
                .ToListAsync();

            return Ok(estacionamientos);
        }
    }
}
