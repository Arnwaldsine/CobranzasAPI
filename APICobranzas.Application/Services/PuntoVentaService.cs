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
    public class PuntoVentaService: IPuntoVentaService
    {
        private readonly IPuntoVentaRepository _repo;
        private readonly IMapper _mapper;
        public PuntoVentaService(IMapper mapper, IPuntoVentaRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<IList<PuntoVentaDTO>> GetPuntosVenta()
        {
            return _mapper.Map<IList<PuntoVentaDTO>>(await _repo.GetPuntosVenta());

        }
        public async Task<PuntoVentaDTO> GetPuntoVenta(int id)
        {
            return _mapper.Map<PuntoVentaDTO>(await _repo.GetPuntoVenta(id));


        }

    }
}
