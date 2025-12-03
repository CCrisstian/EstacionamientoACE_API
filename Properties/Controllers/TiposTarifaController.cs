using EstacionamientoACE_API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoACE_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposTarifaController : ControllerBase
    {
        private readonly AppDbContext _context;

        public TiposTarifaController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTiposTarifas()
        {
            var tipos = await _context.TiposTarifas.ToListAsync();
            return Ok(tipos);
        }
    }
}
