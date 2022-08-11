using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class RespuestaDTO
    {
        public int Id { get; set; }
        [DisplayName("Respuesta")]
        public string Detalle { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public IList<GestionDTO> Gestiones { get; set; }
    }
}
