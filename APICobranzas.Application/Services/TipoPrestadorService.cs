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
    public class TipoPrestadorService: ITipoPrestadorService
    {
        private readonly ITipoPrestadorRepository _repo;
        private readonly IMapper _mapper;
        public TipoPrestadorService(IMapper mapper, ITipoPrestadorRepository repo)
        {
            _repo = repo;
            _mapper = mapper;
        }
        public async Task<IList<TipoPrestadorDTO>> GetTiposPrestador()
        {
            return _mapper.Map<IList<TipoPrestadorDTO>>(await _repo.GetTiposPrestador());
        }
        public async Task<TipoPrestadorDTO> GetTipoPrestador(int id)
        {
            return _mapper.Map<TipoPrestadorDTO>(await _repo.GetTipoPrestador(1));
        }
    }
}
