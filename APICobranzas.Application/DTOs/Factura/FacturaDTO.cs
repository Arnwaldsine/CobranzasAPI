using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Factura;
using APICobranzas.Application.DTOs.Recibo;

namespace APICobranzas.Application.DTOs
{
    public class FacturaDTO
    {
        public int Id { get; set; }
        public string Nro { get; set; }
        public ObraSocialDTO ObraSocial { get; set; }
        public PuntoVentaDTO PuntoVenta { get; set; }
        [DisplayName("Fecha de emision")]
        public DateTime FechaEmision { get; set; }
        [DefaultValue(null)]
        [DisplayName("Ultimo pago")]
        public Nullable<DateTime> FechaUltPago { get; set; }
        [DisplayName("Acuse")]
        public DateTime? FechaAcuse { get; set; }
        [DisplayName("Dias de mora")]
        public int DiasMora { get; set; }
        [DisplayName("Debe")]
        public decimal ImporteDebe { get; set; }
        [DisplayName("Importe")]
        public decimal ImporteFactura { get; set; }
        [DisplayName("Cobrado")]
        public decimal? ImporteCobrado { get; set; }
        public EstadoDTO Estado { get; set; }
        public string Observacion { get; set; }
      //  [JsonProperty("Recibos",NullValueHandling=NullValueHandling.Ignore)]
        public IList<DetalleRecibosFacturaDTO> Recibos { get; set; }
    }
}
