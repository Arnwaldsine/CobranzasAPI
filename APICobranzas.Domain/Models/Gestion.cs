using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class Gestion
    {
        public int Id { get; set; }
        public ObraSocial ObraSocial { get; set; }
        public int ObraSocialId { get; set; }
        public Contacto Contacto { get; set; }
        public int ContactoId { get; set; }
        public DateTime FechaContacto { get; set; }
        public Respuesta Respuesta { get; set; }
        public int RespuestaId { get; set; }
        public DateTime? FechaProxContacto { get; set; }
        public Usuario Usuario { get; set; }
        public int UsuarioId { get; set; }
        public string Observacion { get; set; }

        
    }
}
