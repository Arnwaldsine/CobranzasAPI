using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APICobranzas.Api.Helpers.Query
{
    public  class FacturaQueryParameters :QueryStringParameters
    {
        public FacturaQueryParameters()
        {
            OrderBy = "FechaEmision";
        }
    }
}
