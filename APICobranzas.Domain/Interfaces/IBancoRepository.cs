using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
   public interface IBancoRepository
    {
        Task<ICollection<Banco>> GetBancos();
     
    }
}
