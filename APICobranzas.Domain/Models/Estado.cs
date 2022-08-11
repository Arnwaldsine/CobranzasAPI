using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace APICobranzas.Domain.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Detalle { get; set; }
        
        public ICollection<Factura> Facturas { get; set; }
    }
}
