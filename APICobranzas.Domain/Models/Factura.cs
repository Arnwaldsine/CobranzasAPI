using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace APICobranzas.Domain.Models
{
    public class Factura
    {
        public int Id { get; set; }

      [NotMapped]
        public string Nro { get; set; }

        public ObraSocial ObraSocial { get; set; }
        public int ObraSocialId { get; set; }
        public PuntoVenta PuntoVenta { get; set; }
        public int PuntoventaId { get; set; }
        public DateTime FechaEmision { get; set; }
        [DefaultValue(null)]
        public DateTime? FechaUltPago { get; set; }
        public DateTime? FechaAcuse { get; set; }
        [NotMapped]
        public int DiasMora => Convert.ToInt32((DateTime.Now - FechaEmision).TotalDays);
     
        public decimal ImporteDebe { get; set; }
        public decimal ImporteFactura { get; set; }
        public decimal ImporteCobrado { get; set; }
        public Estado Estado { get; set; }
        [DefaultValue(1)]
        
        public int? EstadoId { get; set; }
        #nullable enable
        public string? Observacion { get; set; }
        #nullable disable
      
        public IList<Recibo> Recibos{ get; set; }
        public IList<NotaCredito>NotasCredito { get; set; }
        public IList<NotaDebito> Notasdebito { get; set; }
        public List<FacturaNota> FacturasNotas { get; set; }
        public List<FacturaRecibo> FacturasRecibos { get; set; }

        public List<FacturaDebito> FacturasDebitos { get; set; }
    }
}
