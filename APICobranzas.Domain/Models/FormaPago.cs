using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class FormaPago
    {
        public int Id { get; set; }
        public string Forma { get; set; }
        public ICollection<FacturaRecibo> FacturasRecibos { get; set; }
        public ICollection<FacturaNota> FacturasNotas { get; set; }
    }
}
