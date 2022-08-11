using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Factura;
using APICobranzas.Application.Mapper;
namespace APICobranzas.Application.Services
{
    public class FacturaService : IFacturaService
    {
        private readonly IFacturaRepository _facturaRepository;
        private readonly IMapper _mapper;
        public FacturaService(IFacturaRepository facturaRepository, IMapper mapper)
        {
            _facturaRepository = facturaRepository;
            _mapper = mapper;
        }
        public async Task<List<FacturaItemDTO>> GetFacturas()
        {
            var list =  await _facturaRepository.GetFacturas();
            var conv = _mapper.Map<List<FacturaItemDTO>>(list);
            return conv;
        }
        public async Task<FacturaDTO> GetFactura(int id)
        {

            return _mapper.Map<FacturaDTO>(await _facturaRepository.GetFactura(id));
        }
        public async Task<FacturaDTO> AddFactura(FacturaRequestModel factura,int puntoVentaId)
        {
            var model = _mapper.Map<Factura>(factura);
            model.PuntoventaId = puntoVentaId;

              return  _mapper.Map<FacturaDTO>(await _facturaRepository.AddFactura(model));

        }

        public async Task<List<FacturaItemDTO>> GetFacturasOS(int id)
        {
            return _mapper.Map<List<FacturaItemDTO>>(await _facturaRepository.GetFacturasOS(id));
        }
        public async Task<bool> RemoveFactura(int id)
        {
            var fac = await  _facturaRepository.GetFactura(id);
            if(fac != null)
            {
               
                    return true;
               

            }
            else
            {
                return false;
            }
        }
        public async Task<bool> UpdateFactura(FacturaRequestModel factura)
        {
            if (factura != null)
            {
                await _facturaRepository.UpdateFactura(_mapper.Map<Factura>(factura));
   
                return true;
            }
            else
            {
                return false;
            }
        }
        public async Task<bool> AnularFactura(int id)
        {
            var fac = await _facturaRepository.GetFactura(id);
            if (fac != null)
            {
                await _facturaRepository.AnularFactura(id);
                return true;
            }
            else
                return false;
        }
    }
}
