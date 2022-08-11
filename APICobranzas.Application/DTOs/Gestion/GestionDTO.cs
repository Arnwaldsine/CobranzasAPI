using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class GestionDTO
    {
        public int Id { get; set; }
        [DisplayName("Ultimo Contacto")]
        public DateTime FechaContacto { get; set; }
        public string Contacto { get; set; }
        public string Respuesta { get; set; }
        [DisplayName("Proximo Contacto")]
        public DateTime? FechaProxContacto { get; set; }
        public string Usuario { get; set; }
        public string Observacion { get; set; }
    }
}
