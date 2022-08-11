using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.Account
{
    public class LoginResponse
    {
        public int Id { get; set; }
        public PuntoVentaDTO PuntoVenta { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string Rol { get; set; }
        public DateTime Creado { get; set; }
        public DateTime? Actualizado { get; set; }
        public bool EstaVerificado { get; set; }
        public string JwtToken { get; set; }

        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }
    }
}
