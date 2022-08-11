using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using APICobranzas.Application.DTOs.Factura;

namespace APICobranzas.Application.DTOs.Recibo
{
    public class ReciboRequestModel
    {
       public string? Observaciones { get; set; }
        [Required(ErrorMessage = "Metodo de pago requerido")]
        public IList<FacturaReciboRequestModel> Detalles {get;set;}
    }
}
