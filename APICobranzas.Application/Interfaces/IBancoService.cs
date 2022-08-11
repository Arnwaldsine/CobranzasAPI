using APICobranzas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.Interfaces
{
    public interface IBancoService
    {
        Task<IList<BancoDTO>> GetBancos();
    }
}
