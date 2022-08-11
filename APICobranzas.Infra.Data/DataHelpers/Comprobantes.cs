using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Infra.Data.DataHelpers
{
    public static class Comprobantes
    {
        public static string NroComprobante(int puntoVenta, int Id)
        {
           return puntoVenta.ToString().PadLeft(4, '0') + "-" + Id.ToString().PadLeft(8, '0');
        }
    }
}
