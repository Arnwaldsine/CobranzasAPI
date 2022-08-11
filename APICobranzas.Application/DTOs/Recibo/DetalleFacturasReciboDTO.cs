using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Factura;

namespace APICobranzas.Application.DTOs
{
    public class DetalleFacturasReciboDTO
    {
        public FacturaItemDTO Factura { get; set; }
        public FormaPagoDTO FormaPago { get; set; }
        [Required]
        public string NroChequeTransf { get; set; }
        [Required]
        public string NroReciboTes { get; set; }
        public BancoDTO Banco { get; set; }
        public decimal SubTotal { get; set; }
    }
}
