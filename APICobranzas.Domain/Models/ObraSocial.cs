using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class ObraSocial
    {
        public int Id { get; set; }
        [Required]
        public uint Rnos { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public uint Cuit { get; set; }
        public string Tel { get; set; }
        public string? Direccion { get; set; }
        public uint CP { get; set; }
        [Url]
        public string? Pagina { get; set; }
        public string? HorarioAdmin { get; set; }
        public string? ContactoAdmin1 { get; set; }
        public string? ContactoAdmin2 { get; set; }
        public uint? TelAdmin { get; set; }
        public string? ContactoGeren1 { get; set; }
        public string? ContactoGeren2 { get; set; }
        public uint? TelGeren { get; set; }
        public string? Mailgeren { get; set; }
        public string? Observaciones { get; set; }

        public int TipoPrestadorId { get; set; }
        public TipoPrestador TipoPrestador { get; set; }

        public IList<Factura> Facturas { get; set; }

        public IList<Gestion> Gestiones { get; set; }
    }
}
