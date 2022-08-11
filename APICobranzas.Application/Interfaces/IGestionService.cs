using APICobranzas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Gestion;

namespace APICobranzas.Application.Interfaces
{
    public interface IGestionService
    {
        Task<DetalleCompletoGestionDTO> GetGestiones(int obraSocialId, int puntoVentaId);
        Task<GestionDTO> GetGestion(int id);
        Task<GestionDTO> UpdateGestion(int id, GestionRequest gestion);
          Task<IList<GestionPendienteDTO>> GetGestionesPendientes();
        Task<bool> RemoveGestion(int id);
        Task<GestionDTO> AddGestion(GestionRequest gestion, int accountId);
    }
}
