using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class EstadoDTO
    {
        public int Id { get; set; }
        [Display(Name ="Estado")]
        public string Detalle { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IList<FacturaDTO> Facturas { get; set; }
    }
}
