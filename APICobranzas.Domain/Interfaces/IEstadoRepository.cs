using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface IEstadoRepository
    {
        Task<IList<Estado>> GetEstados();
        Task<Estado> GetEstado(int id);
    }
}
