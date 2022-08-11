using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class Contacto
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public ICollection<Gestion> Gestiones { get; set; }

    }
}
