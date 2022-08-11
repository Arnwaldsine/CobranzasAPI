using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
   public class Recibo
    {
        public int Id { get; set; }
        [NotMapped]
        public string Nro { get; set; }
        [Required]
        public decimal Total { get; set; }
        
        public DateTime Fecha { get; set; }
        public List<Factura> Facturas { get; set; }
        // public ICollection<Factura> Facturas { get; set; }
        public bool Anulado { get; set; }
        
        public string Observaciones { get; set; }
        public List<FacturaRecibo> FacturasRecibos { get; set; }

    }
}
