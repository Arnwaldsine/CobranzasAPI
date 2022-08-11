using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface IPuntoVentaRepository
    {

        Task<ICollection<PuntoVenta>> GetPuntosVenta();
        Task<PuntoVenta> GetPuntoVenta(int id);

    }
}
