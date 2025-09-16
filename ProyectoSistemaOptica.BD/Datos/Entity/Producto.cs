using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProyectoSistemaOptica.Shared.Enum;

namespace ProyectoSistemaOptica.BD.Datos.Entity
{
    public class Producto : EntityBase
    {
        
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; } // opcional
        public string Tipo { get; set; } = null!; // "Lentes de ver", "Lentes de sol", "Accesorios"
        public int Stock { get; set; }
        public decimal PrecioUnitario { get; set; }

        public EstadoProducto Estado { get; set; } // Enum (Disponible, EnFalta, Discontinuado)

    }

}
