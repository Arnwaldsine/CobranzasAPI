using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.NotaCredito
{
   public class DetalleFacturaNotaDTO
    {
        public int Id { get; set; }
        public string Nro { get; set; }
        public decimal Subtotal { get; set; }
        public string FormaPago { get; set; }
    }
}
