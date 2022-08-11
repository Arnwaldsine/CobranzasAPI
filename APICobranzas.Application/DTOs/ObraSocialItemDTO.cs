using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
   public class ObraSocialItemDTO
    {
        public int Id { get; set; }
        [Display(Name = "RNOS")]
        public uint Rnos { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "CUIT")]
        public uint Cuit { get; set; }
        public TipoPrestadorDTO TipoPrestador { get; set; }

    }
}
