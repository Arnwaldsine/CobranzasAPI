using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.Gestion;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;

namespace APICobranzas.Application.Services
{
    public class GestionService: IGestionService
    {
        private readonly IGestionRepository _gestionRepository;
        private readonly IMapper _mapper;
        public GestionService(IMapper mapper,IGestionRepository gestionRepository)
        {
            _gestionRepository = gestionRepository;
            _mapper = mapper;
        }
        public async Task<DetalleCompletoGestionDTO> GetGestiones(int obraSocialId, int puntoVentaId)
        {
            var tup = await _gestionRepository.GetGestiones(obraSocialId, puntoVentaId);
            DetalleCompletoGestionDTO detalle = new DetalleCompletoGestionDTO
            {
                Gestiones = _mapper.Map<IList<GestionDTO>>(tup.Item1),
                Facturas = _mapper.Map<List<DetalleFacturaGestionDTO>>(tup.Item2)
            };
            detalle.TotalDeuda = tup.Item2.Sum(z => z.ImporteDebe);
            detalle.FacturasSinCobrar = tup.Item2.Count;
            return (detalle);
        }
        public async Task<GestionDTO> GetGestion(int id)
        {
            return _mapper.Map<GestionDTO>(await _gestionRepository.GetGestion(id));
        }
        public async Task<GestionDTO> UpdateGestion(int id, GestionRequest gestion)
        {
            var model = await _gestionRepository.UpdateGestion(_mapper.Map<Gestion>(gestion));
            model.Id = id;
            return _mapper.Map<GestionDTO>(await _gestionRepository.GetGestion(id));

        }
          
        public async Task<GestionDTO> AddGestion(GestionRequest gestion,int accountId)
        {
            var creado = _mapper.Map<Gestion>(gestion);
            creado.UsuarioId = accountId;
            return _mapper.Map<GestionDTO>(await _gestionRepository.AddGestion(creado));
        }
        public async Task<IList<GestionPendienteDTO>> GetGestionesPendientes()
        {
           return  _mapper.Map<IList<GestionPendienteDTO>>(await _gestionRepository.GetGestionesPendientes());
        }
        public async Task<bool> RemoveGestion(int id)
        {
            var gestion = await _gestionRepository.GetGestion(id);
            if(gestion != null)
            {
                await _gestionRepository.RemoveGestion(id);
                return true;
            }
            else
            {
                return false;
            }
        }
        
    }
}
