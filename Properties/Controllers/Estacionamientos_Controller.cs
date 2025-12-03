using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstacionamientoACE_API.Models;
using EstacionamientoACE_API.Models.DTO;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;

namespace EstacionamientoACE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstacionamientosController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<EstacionamientosController> _logger;

        public EstacionamientosController(AppDbContext context, ILogger<EstacionamientosController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // ------------ DTO PARA RECIBIR PUNTAJE ------------
        public class PuntajeRequest
        {
            public int Puntaje { get; set; }
        }

        // ------------ GET ALL ------------
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estacionamiento_DTO>>> GetEstacionamientos()
        {
            try
            {
                var estacionamientos = await _context.Estacionamientos
                    .Include(e => e.Plazas).ThenInclude(p => p.CategoriaVehiculo)
                    .Include(e => e.Tarifas).ThenInclude(t => t.TipoTarifa)
                    .Include(e => e.Tarifas).ThenInclude(t => t.CategoriaVehiculo)
                    .Include(e => e.AceptaMetodosDePagos).ThenInclude(amp => amp.MetodoDePago)
                    .AsSplitQuery()
                    .ToListAsync();

                var dtos = estacionamientos.Select(e => new Estacionamiento_DTO
                {
                    EstId = e.EstId,
                    DuenoLegajo = e.DuenoLegajo,
                    EstNombre = e.EstNombre,
                    EstProvincia = e.EstProvincia,
                    EstLocalidad = e.EstLocalidad,
                    EstDireccion = e.EstDireccion,

                    EstPuntaje = e.EstPuntaje,
                    EstPuntajeAcumulado = e.EstPuntajeAcumulado,
                    EstCantidadVotos = e.EstCantidadVotos,

                    EstDiasAtencion = e.EstDiasAtencion,
                    EstHraAtencion = e.EstHraAtencion,
                    EstDiasFeriadoAtencion = e.EstDiasFeriadoAtencion,
                    EstFinDeSemanaAtencion = e.EstFinDeSemanaAtencion,
                    EstHoraFinDeSemana = e.EstHoraFinDeSemana,
                    EstDisponibilidad = e.EstDisponibilidad,
                    EstLatitud = e.EstLatitud,
                    EstLongitud = e.EstLongitud,

                    Plazas = e.Plazas?.Select(p => new Plaza_DTO
                    {
                        PlazaId = p.PlazaId,
                        PlazaNombre = p.PlazaNombre,
                        PlazaTipo = p.PlazaTipo,
                        PlazaDisponibilidad = p.PlazaDisponibilidad,
                        CategoriaDescripcion = p.CategoriaVehiculo?.CategoriaDescripcion
                    }).ToList() ?? new List<Plaza_DTO>(),

                    Tarifas = e.Tarifas?.Select(t => new Tarifa_DTO
                    {
                        TarifaId = t.TarifaId,
                        TarifaMonto = t.TarifaMonto,
                        TarifaDesde = t.TarifaDesde,
                        CategoriaId = t.CategoriaId,
                        CategoriaDescripcion = t.CategoriaVehiculo?.CategoriaDescripcion,
                        TiposTarifaId = t.TiposTarifaId,
                        TiposTarifaDescripcion = t.TipoTarifa?.TiposTarifaDescripcion
                    }).ToList() ?? new List<Tarifa_DTO>(),

                    MetodosDePagoAceptados = e.AceptaMetodosDePagos?.Select(amp => new AceptaMetodoDePagoDTO
                    {
                        EstId = amp.EstId,
                        MetodoPagoId = amp.MetodoPagoId,
                        MetodoPagoDescripcion = amp.MetodoDePago?.MetodoPagoDescripcion,
                        AMPDesde = amp.AmpDesde,
                        AMPHasta = amp.AmpHasta
                    }).ToList() ?? new List<AceptaMetodoDePagoDTO>()
                }).ToList();

                return Ok(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR en GetEstacionamientos");
                return StatusCode(500, ex.Message);
            }
        }

        // ------------ GET POR ID ------------
        [HttpGet("{id}")]
        public async Task<ActionResult<Estacionamiento_DTO>> GetEstacionamiento(int id)
        {
            try
            {
                var e = await _context.Estacionamientos
                    .Include(e => e.Plazas).ThenInclude(p => p.CategoriaVehiculo)
                    .Include(e => e.Tarifas).ThenInclude(t => t.TipoTarifa)
                    .Include(e => e.Tarifas).ThenInclude(t => t.CategoriaVehiculo)
                    .Include(e => e.AceptaMetodosDePagos).ThenInclude(amp => amp.MetodoDePago)
                    .AsSplitQuery()
                    .FirstOrDefaultAsync(e => e.EstId == id);

                if (e == null)
                    return NotFound();

                var dto = new Estacionamiento_DTO
                {
                    EstId = e.EstId,
                    DuenoLegajo = e.DuenoLegajo,
                    EstNombre = e.EstNombre,
                    EstProvincia = e.EstProvincia,
                    EstLocalidad = e.EstLocalidad,
                    EstDireccion = e.EstDireccion,
                    EstPuntaje = e.EstPuntaje,
                    EstPuntajeAcumulado = e.EstPuntajeAcumulado,
                    EstCantidadVotos = e.EstCantidadVotos,
                    EstDiasAtencion = e.EstDiasAtencion,
                    EstHraAtencion = e.EstHraAtencion,
                    EstDiasFeriadoAtencion = e.EstDiasFeriadoAtencion,
                    EstFinDeSemanaAtencion = e.EstFinDeSemanaAtencion,
                    EstHoraFinDeSemana = e.EstHoraFinDeSemana,
                    EstDisponibilidad = e.EstDisponibilidad,
                    EstLatitud = e.EstLatitud,
                    EstLongitud = e.EstLongitud,

                    Plazas = e.Plazas?.Select(p => new Plaza_DTO
                    {
                        PlazaId = p.PlazaId,
                        PlazaNombre = p.PlazaNombre,
                        PlazaTipo = p.PlazaTipo,
                        PlazaDisponibilidad = p.PlazaDisponibilidad,
                        CategoriaDescripcion = p.CategoriaVehiculo?.CategoriaDescripcion
                    }).ToList() ?? new List<Plaza_DTO>(),

                    Tarifas = e.Tarifas?.Select(t => new Tarifa_DTO
                    {
                        TarifaId = t.TarifaId,
                        TarifaMonto = t.TarifaMonto,
                        TarifaDesde = t.TarifaDesde,
                        CategoriaId = t.CategoriaId,
                        CategoriaDescripcion = t.CategoriaVehiculo?.CategoriaDescripcion,
                        TiposTarifaId = t.TiposTarifaId,
                        TiposTarifaDescripcion = t.TipoTarifa?.TiposTarifaDescripcion
                    }).ToList() ?? new List<Tarifa_DTO>(),

                    MetodosDePagoAceptados = e.AceptaMetodosDePagos?.Select(amp => new AceptaMetodoDePagoDTO
                    {
                        EstId = amp.EstId,
                        MetodoPagoId = amp.MetodoPagoId,
                        MetodoPagoDescripcion = amp.MetodoDePago?.MetodoPagoDescripcion,
                        AMPDesde = amp.AmpDesde,
                        AMPHasta = amp.AmpHasta
                    }).ToList() ?? new List<AceptaMetodoDePagoDTO>()
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR en GetEstacionamiento");
                return StatusCode(500, ex.Message);
            }
        }

        // ------------ POST CREAR ------------
        [HttpPost]
        public async Task<ActionResult<Estacionamiento_DTO>> PostEstacionamiento([FromBody] Estacionamiento estacionamiento)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                _context.Estacionamientos.Add(estacionamiento);
                await _context.SaveChangesAsync();

                var dto = new Estacionamiento_DTO
                {
                    EstId = estacionamiento.EstId,
                    DuenoLegajo = estacionamiento.DuenoLegajo,
                    EstNombre = estacionamiento.EstNombre,
                    EstProvincia = estacionamiento.EstProvincia,
                    EstLocalidad = estacionamiento.EstLocalidad,
                    EstDireccion = estacionamiento.EstDireccion,
                    EstPuntaje = estacionamiento.EstPuntaje,
                    EstPuntajeAcumulado = estacionamiento.EstPuntajeAcumulado,
                    EstCantidadVotos = estacionamiento.EstCantidadVotos,
                    EstDiasAtencion = estacionamiento.EstDiasAtencion,
                    EstHraAtencion = estacionamiento.EstHraAtencion,
                    EstDiasFeriadoAtencion = estacionamiento.EstDiasFeriadoAtencion,
                    EstFinDeSemanaAtencion = estacionamiento.EstFinDeSemanaAtencion,
                    EstHoraFinDeSemana = estacionamiento.EstHoraFinDeSemana,
                    EstDisponibilidad = estacionamiento.EstDisponibilidad,
                    EstLatitud = estacionamiento.EstLatitud,
                    EstLongitud = estacionamiento.EstLongitud
                };

                return CreatedAtAction(nameof(GetEstacionamiento), new { id = dto.EstId }, dto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR en POST");
                return StatusCode(500, ex.Message);
            }
        }

        // ------------ POST PUNTUAR ------------
        [HttpPost("{id}/puntuar")]
        public async Task<IActionResult> Puntuar(int id, [FromBody] PuntajeRequest request)
        {
            try
            {
                var estacionamiento = await _context.Estacionamientos.FindAsync(id);

                if (estacionamiento == null)
                    return NotFound($"No existe estacionamiento con ID {id}");

                // SUMAR PUNTAJE
                estacionamiento.EstPuntajeAcumulado += request.Puntaje;
                estacionamiento.EstCantidadVotos += 1;

                // CALCULAR PROMEDIO
                estacionamiento.EstPuntaje =
                    estacionamiento.EstCantidadVotos == 0
                        ? 0
                        : estacionamiento.EstPuntajeAcumulado / estacionamiento.EstCantidadVotos;

                await _context.SaveChangesAsync();

                return Ok(new
                {
                    nuevoPromedio = estacionamiento.EstPuntaje,
                    cantidadVotos = estacionamiento.EstCantidadVotos
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR PUNTUAR: " + ex.Message);
                Console.WriteLine("DETALLE: " + ex.InnerException?.Message);

                return StatusCode(500, new
                {
                    error = ex.Message,
                    detalle = ex.InnerException?.Message
                });
            }

        }


        // ------------ DELETE ------------
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstacionamiento(int id)
        {
            try
            {
                var estacionamiento = await _context.Estacionamientos.FindAsync(id);
                if (estacionamiento == null)
                    return NotFound();

                _context.Estacionamientos.Remove(estacionamiento);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR en DELETE");
                return StatusCode(500, ex.Message);
            }
        }
    }
}
