using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
   public class Respuesta
    {
        public int Id { get; set; }

        public string Detalle { get; set; }
        public ICollection<Gestion> Gestiones { get; set; }

    }
}
