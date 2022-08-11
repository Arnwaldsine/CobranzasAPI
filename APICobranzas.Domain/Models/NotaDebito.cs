using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class NotaDebito
    {
        public int Id { get; set; }
        public DateTime? Fecha { get; set; }
        [NotMapped]
        public string Nro { get; set; }
        public decimal Total { get; set; }
        public bool Anulado { get; set; }
        public List<Factura> Facturas { get; set; }
        public List<FacturaDebito> FacturasDebitos { get; set; }
        public string? Observaciones { get; set; }
    }
}
