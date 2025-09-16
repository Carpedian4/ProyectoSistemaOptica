using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoSistemaOptica.BD.Datos.Entity
{
      public class Venta : EntityBase
    {
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }

        // Relación con DetalleVenta
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}
