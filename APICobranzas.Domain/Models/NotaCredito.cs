using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class NotaCredito
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        [NotMapped]
        public string Nro { get; set; }
        public decimal Total { get; set; }
        public bool Anulado { get; set; }
        public List<Factura> Facturas { get; set; }
        public List<FacturaNota> FacturasNotas { get; set; }
        public string? Observaciones { get; set; }
    /*    public int PuntoVentaId { get; set; }
        public PuntoVenta PuntoVenta { get; set; }*/
    }
}
