using APICobranzas.Application.DTOs.Recibo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;

namespace APICobranzas.Application.Interfaces
{
    public interface IReciboService
    {
        Task<ReciboDTO> GetReciboNro(string nro);
        Task<IList<ReciboDTO>> GetRecibos();
        Task<Tuple<bool, ReciboDetalleDTO>> AddRecibo(ReciboRequestModel recibo, int puntoVentaId);
        Task<ReciboDetalleDTO> GetRecibo(int id);
        Task UpdateRecibo(ReciboRequestModel recibo);
        Task<bool> RemoveRecibo(int id);

    }
}
