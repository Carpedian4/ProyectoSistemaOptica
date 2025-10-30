
using System.Collections.Generic;

namespace ProyectoSistemaOptica.DTOs
{

    public class RegistrarVentaDTO
    {
        public List<DetalleVentaDTO> Detalles { get; set; } = new List<DetalleVentaDTO>();
    }
}