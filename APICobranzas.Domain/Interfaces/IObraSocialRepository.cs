using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface IObraSocialRepository
    {
        Task<IList<ObraSocial>> GetObrasSociales();
        Task<ObraSocial> GetObraSocial(int id);
        Task<bool> RemoveObraSocial(int id);
        Task<bool> UpdateObraSocial(ObraSocial obraSocial);
        Task<ObraSocial> AddObraSocial(ObraSocial obraSocial);
        Task<IList<ObraSocial>> GetByTipo(int tipoId);
    }
}