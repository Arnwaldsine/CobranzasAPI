using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.Gestion
{
    public class DetalleFacturaGestionDTO
    {
        public int Id { get; set; }
        public string? Nro { get; set; }
        public int PuntoVentaNro { get; set; }
        public string PuntoVenta { get; set; }
        public DateTime FechaEmision { get; set; }
        public int DiasMora => Convert.ToInt32((DateTime.Now - FechaEmision).TotalDays);
        public decimal ImporteDebe { get; set; }

    }
}
