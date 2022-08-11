using System.ComponentModel.DataAnnotations;

namespace APICobranzas.Application.DTOs.Factura
{
   public class FacturaRequestModel
    {
        [Required(ErrorMessage = "Obra social requerida")]
        public int ObraSocialId { get; set; }
        [Required(ErrorMessage = "Ingrese un importe")]
        public decimal ImporteFactura { get; set; }
        public string Observacion { get; set; }
    }
}
