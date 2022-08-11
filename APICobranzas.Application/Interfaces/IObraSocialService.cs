using APICobranzas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.ObraSocial;

namespace APICobranzas.Application.Interfaces
{
    public interface IObraSocialService
    {
        Task<IList<ObraSocialDTO>> GetObrasSociales();
        Task<ObraSocialDTO> GetObraSocial(int id);
        Task<bool> UpdateObraSocial(int id, ObraSocialRequestModel dto);
 
         Task<bool> RemoveObraSocial(int id);
        Task<ObraSocialDTO> AddObrasocial(ObraSocialRequestModel rm);
        Task<IList<ObraSocialDTO>> GetByTipo(int tipoId);
    }
}
