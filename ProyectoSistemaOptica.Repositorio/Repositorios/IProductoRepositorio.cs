using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSistemaOptica.Repositorio.Repositorios
{
    public interface IProductoRepositorio
    {
        Task<List<ProductoDTO>> GetProductosAsync();
    }
}
