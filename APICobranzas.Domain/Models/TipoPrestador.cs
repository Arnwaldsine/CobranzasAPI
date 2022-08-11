using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class TipoPrestador
    {
        public int Id { get; set; }
        [Required]
        public string Tipo { get; set; }
        public ICollection<ObraSocial> ObrasSociales { get; set; }
    }
}
