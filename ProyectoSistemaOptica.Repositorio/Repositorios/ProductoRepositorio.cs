using ProyectoSistemaOptica.BD.Datos;
using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.Shared.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSistemaOptica.Repositorio.Repositorios
{
    public class ProductoRepositorio : IProductoRepositorio
    {
        private readonly AppDbContext _context;

        public ProductoRepositorio(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductoDTO>> GetProductosAsync()
        {
            return await _context.Productos
                // **Mapeo de Entidad a DTO usando Select**
                .Select(p => new ProductoDTO
                {
                    Id = p.Id,
                    Nombre = p.Nombre,
                    Descripcion = p.Descripcion,
                    PrecioUnitario = p.PrecioUnitario,
                    Stock = p.Stock
                })
                .ToListAsync();
        }
    }
}
