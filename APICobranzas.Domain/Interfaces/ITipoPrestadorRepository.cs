using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
   public interface ITipoPrestadorRepository
    {

        Task<ICollection<TipoPrestador>> GetTiposPrestador();
        Task<TipoPrestador> GetTipoPrestador(int id);

    }
}
