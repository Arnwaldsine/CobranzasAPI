using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Domain.Models
{
    public class FacturaRecibo
    {
        public int FacturaId { get; set; }
        public Factura Factura { get; set; }
        public int ReciboId { get; set; }
        public Recibo Recibo { get; set; }
        [Required]
        public int FormaPagoId { get; set; }
        public FormaPago FormaPago { get; set; }
        [Required]
        public string NroChequeTransf { get; set; }
        [Required]
        public string NroReciboTes { get; set; }
        public Banco Banco { get; set; }
        [Required]
        public int BancoId { get; set; }
        public decimal SubTotal { get; set; }
    }
}
