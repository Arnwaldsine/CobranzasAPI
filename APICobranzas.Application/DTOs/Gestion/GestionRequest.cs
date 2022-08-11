using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.Gestion
{
   public class GestionRequest
    {
        public int ObraSocialId { get; set; }
        public int ContactoId { get; set; }
        public int RespuestaId { get; set; }
        public DateTime? FechaProxContacto { get; set; }
        public string Observacion { get; set; }
    }
}
