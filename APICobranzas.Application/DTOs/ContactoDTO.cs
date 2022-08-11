using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class ContactoDTO
    {
        public int Id { get; set; }
        [Display(Name ="Tipo")]
        public string Tipo { get; set; }
        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public IList<GestionDTO> Gestiones { get; set; }
    }
}
