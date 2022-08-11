using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APICobranzas.Application.Services
{
   public class EstadoService: IEstadoService
    {
        private readonly IEstadoRepository _estadoRepository;
        private readonly IMapper _mapper;
        public EstadoService(IEstadoRepository estadoRepository, IMapper mapper)
        {
            _estadoRepository = estadoRepository;
            _mapper = mapper;
        }
        public async Task<IList<EstadoDTO>> GetEstados()
        {
          return  _mapper.Map<IList<EstadoDTO>>(await _estadoRepository.GetEstados());
        }
        public async Task<EstadoDTO> GetEstado(int id)
        {
            return _mapper.Map<EstadoDTO>(await _estadoRepository.GetEstado(id));
        }
    }
}
