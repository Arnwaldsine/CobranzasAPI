using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs
{
    public class FormaPagoDTO
    {
        public int Id { get; set; }
        [DisplayName("Metodo de Pago")]
        public string Forma { get; set; }
        //[JsonIgnore]
    }
}
