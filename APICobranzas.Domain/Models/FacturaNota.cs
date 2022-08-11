using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class FacturaNota
    {
    
        public Factura Factura { get; set; }
        public int FacturaId { get; set; }
        public  NotaCredito NotaCredito {get;set;}
        public int NotaCreditoId { get; set; }
        public FormaPago FormaPago { get; set; }
        public int FormaPagoId { get; set; }
        public decimal Subtotal { get; set; }
    }
}
