using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
   public class Banco
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public ICollection<FacturaRecibo> FacturasRecibos { get; set; }

    }
}
