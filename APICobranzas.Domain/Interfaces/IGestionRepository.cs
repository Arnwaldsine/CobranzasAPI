using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface IGestionRepository
    {
        Task RemoveGestion(int id);
        Task<(IList<Gestion>, List<Factura>)> GetGestiones(int obraSocialId, int puntoVentaId);
        Task<Gestion>GetGestion(int id);
        Task<Gestion> UpdateGestion(Gestion gestion);
        //    Task RealizarGestion(int id, DateTime nuevaFecha);
        Task<Gestion> AddGestion(Gestion gestion);
        Task<IList<Gestion>> GetGestionesPendientes();
    }
}
