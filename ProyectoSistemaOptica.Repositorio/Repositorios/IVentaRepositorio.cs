using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.DTOs;
using ProyectoSistemaOptica.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSistemaOptica.Repositorio.Repositorios
{
    public interface IVentaRepositorio
    {       
        Task<Venta> RegistrarVentaAsync(RegistrarVentaDTO ventaDto);
        Task<List<VentaListadoDTO>> GetVentasListaAsync();
        Task<List<Producto>> GetProductosAsync();
        Task<Venta?> GetVentaPorIdAsync(int id);
    }
}