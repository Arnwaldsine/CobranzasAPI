using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.ObraSocial;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;

namespace APICobranzas.Application.Services
{
    public class ObraSocialService : IObraSocialService
    {
        private readonly IObraSocialRepository _obraSocialRepository;
        private readonly IMapper _m;
        public ObraSocialService(IMapper m,IObraSocialRepository obraSocialRepository)
        {
            _m = m;
            _obraSocialRepository = obraSocialRepository;
        }
        public async Task<IList<ObraSocialDTO>> GetObrasSociales()
        {
            return _m.Map<IList<ObraSocialDTO>>(await _obraSocialRepository.GetObrasSociales());
        }
        public async Task<IList<ObraSocialDTO>> GetByTipo(int tipoId)
        {
            return _m.Map<IList<ObraSocialDTO>>(await _obraSocialRepository.GetByTipo(tipoId));
        }
        public async Task<ObraSocialDTO> GetObraSocial(int id)
        {
            return _m.Map<ObraSocialDTO>(await _obraSocialRepository.GetObraSocial(id));
        }
        public async Task<bool> UpdateObraSocial(int id, ObraSocialRequestModel dto)
        {
            var model = _m.Map<ObraSocial>(dto);
            model.Id = id;
              return await  _obraSocialRepository.UpdateObraSocial(model);
        }
        public async Task<bool> RemoveObraSocial(int id)
        {
             return   await _obraSocialRepository.RemoveObraSocial(id);
               
        }

        public async Task<ObraSocialDTO> AddObrasocial(ObraSocialRequestModel rm)
        {
            var creada = await _obraSocialRepository.AddObraSocial(_m.Map<ObraSocial>(rm));
            return _m.Map<ObraSocialDTO>(creada);
        }
    }
}
