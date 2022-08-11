using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.Gestion
{
   public class GestionPendienteDTO
    {
        public int Id { get; set; }
        public string ObraSocial { get; set; }
        public DateTime UltContacto { get; set; }
        public int FacturasSinCobrar { get; set; }
        public decimal TotalDeuda { get; set; }
    }
}
