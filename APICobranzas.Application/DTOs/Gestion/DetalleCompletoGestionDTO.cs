using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Factura;

namespace APICobranzas.Application.DTOs.Gestion
{
    public class DetalleCompletoGestionDTO
    {
        public int FacturasSinCobrar { get; set; }
        public decimal TotalDeuda { get; set; }
        public IList<GestionDTO> Gestiones { get; set; }
        public List<DetalleFacturaGestionDTO> Facturas { get; set; }
    }
}
