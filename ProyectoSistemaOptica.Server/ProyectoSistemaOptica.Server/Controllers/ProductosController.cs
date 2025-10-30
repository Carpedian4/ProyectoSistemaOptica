using Microsoft.AspNetCore.Mvc;
using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.Repositorio.Repositorios;
using ProyectoSistemaOptica.Shared.DTO;

namespace ProyectoSistemaOptica.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {

        private readonly IProductoRepositorio _productoRepositorio;

        public ProductosController(IProductoRepositorio productoRepositorio)
        {
            _productoRepositorio = productoRepositorio;
        }

        [HttpGet]
        // RUTA: GET /api/Productos
        public async Task<ActionResult<List<ProductoDTO>>> GetProductos()
        {
            //  método específico del repositorio
            var productos = await _productoRepositorio.GetProductosAsync(); 

            if (productos == null || productos.Count == 0)
                return NotFound("No hay productos cargados.");

            return Ok(productos);
        }
    }
}