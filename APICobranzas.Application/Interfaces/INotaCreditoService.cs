using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.NotaCredito;

namespace APICobranzas.Application.Interfaces
{
   public interface INotaCreditoService
    {
        Task<List<NotaCreditoItemDTO>> GetNotas();
        Task<NotaCreditoDTO> GetNota(int id);
        Task<(bool, NotaCreditoDTO)> AddNota(NotaCreditoRequest req);
        Task<bool> AnularNota(int id);
    }
}
