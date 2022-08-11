using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface IReciboRepository
    {
        Task<Recibo> GetReciboNro(string nro);
        Task<bool> RemoveRecibo(Recibo recibo);
        Task<IList<Recibo>> GetRecibos();
        Task<Recibo> GetRecibo(int id);
        Task<Recibo> UpdateRecibo(Recibo recibo);
        Task<Tuple<bool, Recibo>> AddRecibo(Recibo recibo, int puntoVentaId);
    }
}
