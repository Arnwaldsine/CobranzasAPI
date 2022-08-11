using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Application.DTOs
{
   public class PuntoVentaDTO
    { 
        public int Id { get; set; }
        public int Numero { get; set; }
        [DisplayName("Punto de venta")]
        public string Punto { get; set; }
    }
}
