using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class ObraSocialDTO
    {
        public int Id { get; set; }
        [Display(Name ="RNOS")]
        public uint Rnos { get; set; }
        public string Nombre { get; set; }
        [Display(Name = "CUIT")]
        public uint Cuit { get; set; }
        [Display(Name = "Teléfono")]
        public string Tel { get; set; }
        [Display(Name = "Dirección")]
        public string? Direccion { get; set; }
        [Display(Name = "C.P.")]
        public uint CP { get; set; }
        public string? Pagina { get; set; }
        [Display(Name = "Horario administración")]
        public string? HorarioAdmin { get; set; }
        [Display(Name = "Contacto Admin. 1")]
        public string? ContactoAdmin1 { get; set; }
        [Display(Name = "Contacto Admin. 2")]
        public string? ContactoAdmin2 { get; set; }
        [Display(Name = "Telefono Administracion")]
        public uint? TelAdmin { get; set; }
        [Display(Name = "Contacto Gerencia 1")]
        public string? ContactoGeren1 { get; set; }
        [Display(Name = "Contacto Gerencia 2")]
        public string? ContactoGeren2 { get; set; }
        [Display(Name = "Teléfono Gerencia ")]
        public uint? TelGeren { get; set; }
        [Display(Name = "Mail Gerencia")]
        public string? Mailgeren { get; set; }
        public string? Observaciones { get; set; }

        public TipoPrestadorDTO TipoPrestador { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public IList<FacturaDTO> Facturas { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]

        public IList<GestionDTO> Gestiones { get; set; }
    }
}
