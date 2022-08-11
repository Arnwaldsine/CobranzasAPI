using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.NotaCredito
{
    public class DetalleFacturaNotaRequest
    {
   
        public int FacturaId { get; set; }
        public decimal SubTotal { get; set; }
        public int FormaPagoId { get; set; }
    }
}
