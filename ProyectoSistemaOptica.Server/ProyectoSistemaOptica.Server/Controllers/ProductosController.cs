using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoSistemaOptica.BD.Datos;
using ProyectoSistemaOptica.BD.Datos.Entity;

namespace ProyectoSistemaOptica.Server.Controllers
{
[ApiController]
    [Route("api/[controller]")]
    public class ProductoController : ControllerBase
    {
        private readonly AppDbContext context;

        public ProductoController(AppDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Producto>>> GetProductos()
        {
            var productos = await context.Productos.ToListAsync();
            if (productos == null || productos.Count == 0)
                return NotFound("No hay productos cargados");

            return Ok(productos);
        }
    }
}
