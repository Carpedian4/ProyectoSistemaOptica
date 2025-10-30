using Microsoft.EntityFrameworkCore;
using ProyectoSistemaOptica.BD.Datos;
using ProyectoSistemaOptica.BD.Datos.Entity;
using ProyectoSistemaOptica.DTOs;
using ProyectoSistemaOptica.Repositorio.Repositorios;
using ProyectoSistemaOptica.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectoSistemaOptica.Repositorio.Repositorios;
public class VentaRepositorio : IVentaRepositorio
{
    private readonly AppDbContext _context;

    public VentaRepositorio(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Venta?> RegistrarVentaAsync(RegistrarVentaDTO ventaDTO)
    {
        var nuevaVenta = new Venta
        {
            Fecha = DateTime.Now,
            Total = 0,
                       
        };

        foreach (var detalleDto in ventaDTO.Detalles)
        {
            var producto = await _context.Productos
                                         .FindAsync(detalleDto.ProductoId);

            if (producto == null || producto.Stock < detalleDto.Cantidad)
            {
                return null;
            }

            var detalle = new DetalleVenta
            {
                ProductoId = detalleDto.ProductoId,
                Cantidad = detalleDto.Cantidad,
                PrecioUnitario = producto.PrecioUnitario, 
                Subtotal = detalleDto.Cantidad * producto.PrecioUnitario
            };

            nuevaVenta.Detalles.Add(detalle);
            producto.Stock -= detalleDto.Cantidad;
        }
        nuevaVenta.Total = nuevaVenta.Detalles.Sum(d => d.Subtotal);
        _context.Ventas.Add(nuevaVenta);
        await _context.SaveChangesAsync();

        return nuevaVenta;
    }


    public async Task<List<VentaListadoDTO>> GetVentasListaAsync()
    {
        return await _context.Ventas
            .Select(v => new VentaListadoDTO
            {
                Id = v.Id,
                Fecha = v.Fecha,
                Total = v.Total
            })
            .ToListAsync();
    }

    public async Task<List<Producto>> GetProductosAsync()
    {
        return await _context.Productos.ToListAsync();
    }

    public async Task<Venta?> GetVentaPorIdAsync(int id)
    {
        return await _context.Ventas
            .Include(v => v.Detalles)
            .FirstOrDefaultAsync(v => v.Id == id);
    }
}