using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.NotaCredito
{
    public class NotaCreditoRequest
    {
        public string? Observaciones { get; set; }
        public List<DetalleFacturaNotaRequest> Detalles { get; set; }
    }
}
