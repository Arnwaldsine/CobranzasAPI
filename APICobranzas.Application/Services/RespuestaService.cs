using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using AutoMapper;

namespace APICobranzas.Application.Services
{
    public class RespuestaService:IRespuestaService
    {
        private readonly IRespuestaRepository _respuestaRepository;
        private readonly IMapper _mapper;
        public RespuestaService(IMapper mapper, IRespuestaRepository respuestaRepository)
        {
            _mapper = mapper;
            _respuestaRepository = respuestaRepository;
        }
        public async Task<IList<RespuestaDTO>> GetRespuestas()
        {
            return _mapper.Map<IList<RespuestaDTO>>(await _respuestaRepository.GetRespuestas());
        }
        public async Task<RespuestaDTO> GetRespuesta(int id)
        {
            return _mapper.Map<RespuestaDTO>(await _respuestaRepository.GetRespuesta(id));
        }
    }

}
