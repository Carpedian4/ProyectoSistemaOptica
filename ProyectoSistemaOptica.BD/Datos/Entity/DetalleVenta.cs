using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSistemaOptica.BD.Datos.Entity
{
    public class DetalleVenta : EntityBase
    {

        // Relación con Venta
        public int VentaId { get; set; }
        public Venta Venta { get; set; } = null!;

        // Relación con Producto
        public int ProductoId { get; set; }
        public Producto Producto { get; set; } = null!;

        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; } // Cantidad * PrecioUnitario
    }
}
