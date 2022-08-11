using System.ComponentModel.DataAnnotations;

namespace APICobranzas.Application.DTOs.Factura
{
   public class FacturaReciboRequestModel
    {
       public int FacturaId { get; set; }
       public int FormaPagoId { get; set; }
       [Required]
       public string NroChequeTransf { get; set; }
       [Required]
       public string NroReciboTes { get; set; }
        [Required]
        public int BancoId { get; set; }
        public decimal SubTotal { get; set; }
    }
}
