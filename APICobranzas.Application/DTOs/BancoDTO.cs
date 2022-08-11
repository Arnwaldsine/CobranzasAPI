using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Recibo;

namespace APICobranzas.Application.DTOs
{
    public class BancoDTO
    {
        public int Id { get; set; }
        [Display(Name ="Banco")]
        public string Nombre { get; set; }
    }
}
