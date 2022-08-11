using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace APICobranzas.Domain.Models
{
    public class PuntoVenta
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Punto { get; set; }
        public ICollection<Usuario> Usuarios { get; set; }
        public ICollection<Factura> Facturas { get; set; }
        public IList<Recibo> Recibos { get; set; }
        public IList<NotaCredito> NotasCredito { get; set; }
    }
}
