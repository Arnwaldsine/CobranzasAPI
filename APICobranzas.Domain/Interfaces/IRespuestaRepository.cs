using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface IRespuestaRepository
    {
        Task<IList<Respuesta>> GetRespuestas();
        Task<Respuesta> GetRespuesta(int id);
    }
}
