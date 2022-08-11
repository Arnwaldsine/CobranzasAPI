using APICobranzas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.Interfaces
{
    public interface ITipoPrestadorService
    {

        Task<IList<TipoPrestadorDTO>> GetTiposPrestador();
        Task<TipoPrestadorDTO> GetTipoPrestador(int id);

    }
}
