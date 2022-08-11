using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Application.DTOs.Account
{
    public class ModificarUsuarioRequest
    {
        private string _password;
        private string _confirmarPassword;
        private string _rol;
        private string _email;

        public int PuntoVentaId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }

        [EnumDataType(typeof(Rol))]
        public string Rol
        {
            get => _rol;
            set => _rol = VacioANull(value);
        }

        [EmailAddress]
        public string Email
        {
            get => _email;
            set => _email = VacioANull(value);
        }

        [MinLength(6)]
        public string Password
        {
            get => _password;
            set => _password = VacioANull(value);
        }

        [Compare("Password")]
        public string ConfirmarPassword
        {
            get => _confirmarPassword;
            set => _confirmarPassword = VacioANull(value);
        }

        // helpers

        private string VacioANull(string value)
        {
            // replace empty string with null to make field optional
            return string.IsNullOrEmpty(value) ? null : value;
        }
    }
}
