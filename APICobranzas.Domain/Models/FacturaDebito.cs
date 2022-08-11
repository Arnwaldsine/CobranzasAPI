using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class FacturaDebito
    {
        public Factura Factura { get; set; }
        public int FacturaId { get; set; }
        public NotaDebito NotaDebito { get; set; }
        public int NotaDebitoId { get; set; }
        public decimal Subtotal { get; set; }
    }
}
