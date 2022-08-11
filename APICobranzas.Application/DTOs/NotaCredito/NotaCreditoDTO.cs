using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.NotaCredito
{
  public  class NotaCreditoDTO
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        public string Nro { get; set; }
        public decimal Total { get; set; }
        public string ObraSocial { get; set; }
        public string PuntoVenta { get; set; }
       public string Observaciones { get; set; }
        public List<DetalleFacturaNotaDTO> Detalles { get; set; }
    }
}
