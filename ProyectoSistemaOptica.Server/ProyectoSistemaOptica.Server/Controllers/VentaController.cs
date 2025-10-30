using Microsoft.AspNetCore.Mvc;
using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.DTOs;
using ProyectoSistemaOptica.Repositorio.Repositorios;
using ProyectoSistemaOptica.Shared.DTO;

namespace ProyectoSistemaOptica.Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VentaController : ControllerBase
    {
        private readonly IVentaRepositorio _ventaRepositorio;

        public VentaController(IVentaRepositorio ventaRepositorio)
        {
            _ventaRepositorio = ventaRepositorio;
        }

        // RUTA: POST /api/Venta
        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody] RegistrarVentaDTO ventaDto)
        {
            try
            {
                var nuevaVenta = await _ventaRepositorio.RegistrarVentaAsync(ventaDto);           
                return Created($"/api/Venta/{nuevaVenta.Id}", new { Mensaje = $"Venta registrada con éxito. Nro: {nuevaVenta.Id}" });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(ex.Message); 
            }
            catch (Exception)
            {
                return StatusCode(500, "Error interno del sistema al registrar la venta. Intente de nuevo.");
            }
        }

        // RUTA: GET /api/Venta
        [HttpGet]
        public async Task<ActionResult<List<VentaListadoDTO>>> GetVentas()
        {
            var ventasDTO = await _ventaRepositorio.GetVentasListaAsync();

            if (ventasDTO == null || !ventasDTO.Any())
                return NotFound("No hay ventas cargadas aún.");
            return Ok(ventasDTO);
        }

        // RUTA: GET /api/Venta/{id}
  
        [HttpGet("{id}")]
        public async Task<ActionResult<Venta>> GetVenta(int id)
        {          
            var venta = await _ventaRepositorio.GetVentaPorIdAsync(id);
            if (venta == null)
                return NotFound($"No se encontró la venta con el Id: {id}.");
            return Ok(venta);
        }
    }
}