using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EstacionamientoACE_API.Models;

namespace EstacionamientoACE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarifasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TarifasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarifa>>> GetTarifas()
        {
            return await _context.Tarifas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarifa>> GetTarifa(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);

            if (tarifa == null)
                return NotFound();

            return tarifa;
        }

        [HttpPost]
        public async Task<ActionResult<Tarifa>> PostTarifa(Tarifa tarifa)
        {
            _context.Tarifas.Add(tarifa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTarifa), new { id = tarifa.TarifaId }, tarifa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarifa(int id, Tarifa tarifa)
        {
            if (id != tarifa.TarifaId)
                return BadRequest();

            _context.Entry(tarifa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Tarifas.Any(t => t.TarifaId == id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarifa(int id)
        {
            var tarifa = await _context.Tarifas.FindAsync(id);
            if (tarifa == null)
                return NotFound(); // ✅ retorna algo

            _context.Tarifas.Remove(tarifa);
            await _context.SaveChangesAsync();

            return NoContent(); // ✅ retorno al final
        }
    }
}
