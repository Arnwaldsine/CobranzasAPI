using System.ComponentModel.DataAnnotations;

namespace APICobranzas.Application.DTOs.ObraSocial
{
    public class ObraSocialRequestModel
    {

        [Required(ErrorMessage = "Campo requerido")]
        [Range(0, uint.MaxValue, ErrorMessage = "inserte solo valores numericos")]
        public uint Rnos { get; set; }
        [Required(ErrorMessage = "Campo requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage ="Campo requerido")]
        [Range(0, uint.MaxValue, ErrorMessage = "inserte solo valores numericos")]

        public uint Cuit { get; set; }
        public string Tel { get; set; }
        public string Direccion { get; set; }
        public uint CP { get; set; }
        [Url(ErrorMessage ="Inserte una direccion de sitio web valida")]
        public string Pagina { get; set; }
        public string HorarioAdmin { get; set; }
        [StringLength(150)]
        public string ContactoAdmin1 { get; set; }
        [StringLength(150)]
        public string ContactoAdmin2 { get; set; }
        public uint TelAdmin { get; set; }
        [StringLength(150)]
        public string ContactoGeren1 { get; set; }
        [StringLength(150)]
        public string ContactoGeren2 { get; set; }
        public uint TelGeren { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Mailgeren { get; set; }
        public string Observaciones { get; set; }
        [Required(ErrorMessage ="Debe especificar el tipo de prestador")]
        public int TipoPrestadorId { get; set; }
        
    }
}
