using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.NotaCredito;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;

namespace APICobranzas.Application.Services
{
    public  class NotaCreditoService :INotaCreditoService
    {
        private readonly INotaCreditoRepository _repo;
        private readonly IMapper _mapper;
        public NotaCreditoService(INotaCreditoRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<List<NotaCreditoItemDTO>> GetNotas()
        {
            return _mapper.Map<List<NotaCreditoItemDTO>>(await _repo.GetNotas());
        }

        public async Task<NotaCreditoDTO> GetNota(int id)
        {
            return _mapper.Map<NotaCreditoDTO>(await _repo.GetNota(id));
        }

        public async Task<bool> AnularNota(int id)
        {
            return await _repo.AnularNota(id);
        }
        public async Task<(bool, NotaCreditoDTO)> AddNota(NotaCreditoRequest req)
        {
            var nota = _mapper.Map<NotaCredito>(req);
            nota.FacturasNotas = _mapper.Map<List<FacturaNota>>(req.Detalles);

            var added = await _repo.AddNota(nota);
            if (added.Item1)
            {
                var dto = _mapper.Map<NotaCreditoDTO>(added.Item2);
                dto.Detalles = _mapper.Map<List<DetalleFacturaNotaDTO>>(added.Item2.FacturasNotas);
                return (true,dto);
            }

            return (false, null);
        }

    }
}
