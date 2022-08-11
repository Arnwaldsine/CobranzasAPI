using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Recibo;

namespace APICobranzas.Application.DTOs.Factura
{
    public class DetalleRecibosFacturaDTO
    {
        public ReciboDTO Recibo { get; set; }
        public FormaPagoDTO FormaPago { get; set; }
        [Required]
        public string NroChequeTransf { get; set; }
        [Required]
        public string NroReciboTes { get; set; }
        public BancoDTO Banco { get; set; }
        public decimal SubTotal { get; set; }
    }
}
