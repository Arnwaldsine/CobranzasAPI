using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.Factura
{
  public class FacturaItemDTO
    {
        public int Id { get; set; }
        public string Nro { get; set; }
        public string ObraSocial { get; set; }
        public string PuntoVenta { get; set; }
        public DateTime FechaEmision { get; set; }
        public DateTime? FechaUltPago { get; set; }
        public DateTime? FechaAcuse { get; set; }
        public int DiasMora => Convert.ToInt32((DateTime.Now - FechaEmision).TotalDays);
        public decimal ImporteDebe { get; set; }
        public decimal ImporteFactura { get; set; }
        public decimal ImporteCobrado { get; set; }
        public string Estado { get; set; }
        public string? Observacion { get; set; }

    }
}
