using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;
using Microsoft.AspNetCore.Identity;

namespace APICobranzas.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        public int PuntoVentaId { get; set; }
        public PuntoVenta PuntoVenta { get; set; }

        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool AceptaTerminos { get; set; }
        public Rol Rol { get; set; }
        public string TokenDeVerificacion { get; set; }
        public DateTime? FechaVerificion { get; set; }
        public bool  EstaVerificado => FechaVerificion.HasValue || FechaReseteoPassword.HasValue;
        public string TokenReinicio { get; set; }
        public DateTime? FechaExpiracionTokenReseteo { get; set; }
        public DateTime? FechaReseteoPassword { get; set; }
        public DateTime Creado { get; set; }
        public DateTime? Actualizado{ get; set; }
        public List<RefreshToken> RefreshTokens { get; set; }


        public ICollection<Gestion> Gestiones { get; set; }
        public bool TieneToken(string token)
        {
            return this.RefreshTokens?.Find(x => x.Token == token) != null;
        }
    }

}
