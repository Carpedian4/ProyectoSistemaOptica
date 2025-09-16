using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSistemaOptica.BD.Datos;
using ProyectoSistemaOptica.BD.Datos.Entity;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Components.WebAssembly.HotReload.WebAssemblyHotReload;

namespace ProyectoSistemaOptica.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly AppDbContext context;

        public VentaController(AppDbContext context)
        {
            this.context = context;
        }

        // GET: api/Venta → lista todas las ventas SIN detalles
        [HttpGet]
        public async Task<ActionResult<List<Venta>>> GetVentas()
        {
            var ventas = await context.Ventas.ToListAsync();
            if (ventas == null)
                return NotFound($"No hay ventas cargadas aun");
            return Ok(ventas);
        }

        // GET: api/Venta/5 → busca una venta por Id CON detalles
        [HttpGet("{id}")]
        public async Task<ActionResult> GetVenta(int id)
        {
            var venta = await context.Ventas
                                     .Include(v => v.Detalles)
                                     
                                     .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null)
                return NotFound($"No se encontró la venta con el Id: {id}.");

            return Ok(venta);
        }

        // POST: api/Venta → registra una venta
        [HttpPost]
        public async Task<int> PostVenta(Venta venta)
        {
            await context.Ventas.AddAsync(venta);
            await context.SaveChangesAsync();
            return venta.Id;
        }
    }
}