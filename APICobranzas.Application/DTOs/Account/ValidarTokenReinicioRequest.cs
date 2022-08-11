using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.DTOs.Account
{
    
    public class ValidarTokenReinicioRequest
    {
        [Required]
        public string Token { get; set; }
    }
}
