using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class TipoPrestadorDTO
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Tipo de prestador")]
        public string Tipo { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<ObraSocialDTO> ObrasSociales { get; set; }
    }
}
