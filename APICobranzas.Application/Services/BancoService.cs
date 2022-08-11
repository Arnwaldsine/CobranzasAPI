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
    public class BancoService : IBancoService
    {
        private readonly IBancoRepository _bancoRepository;
        private readonly IMapper _mapper;
        public BancoService(IBancoRepository bancoRepository,IMapper mapper)
        {
            _bancoRepository = bancoRepository;
            _mapper = mapper;
        }
        public async Task<IList<BancoDTO>> GetBancos()
        {
            return _mapper.Map<IList<BancoDTO>>( await _bancoRepository.GetBancos());
        }
    }
}
