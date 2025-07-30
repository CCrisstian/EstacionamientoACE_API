using EstacionamientoACE_API.Models;
using EstacionamientoACE_API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
                .Include(e => e.Plazas) // Incluir plazas para poder contarlas
                .Select(e => new Estacionamiento_DTO
                {
                    EstNombre = e.EstNombre ?? "Sin nombre",
                    Est_direccion = e.EstDireccion ?? "Sin dirección",
                    Est_Dias_Atencion = e.EstDiasAtencion ?? "No definido",
                    Est_Hra_Atencion = e.EstHraAtencion ?? "No definido",
                    EstLatitud = e.EstLatitud,
                    EstLongitud = e.EstLongitud,
                    EstDiasFeriadoAtencion = e.EstDiasFeriadoAtencion,
                    EstFinDeSemanaAtencion = e.EstFinDeSemanaAtencion,
                    EstHoraFinDeSemana = e.EstHoraFinDeSemana,
                    EstDisponibilidad = e.EstDisponibilidad,

                    // Calcular plazas disponibles (Plaza.Disponible debe existir)
                    CantidadPlazasDisponibles = e.Plazas.Count(p => p.PlazaDisponibilidad)
                })
                .ToListAsync();

            return Ok(estacionamientos);
        }
    }
}
