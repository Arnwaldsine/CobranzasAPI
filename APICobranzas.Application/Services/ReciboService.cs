using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.Recibo;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;

namespace APICobranzas.Application.Services
{
    public class ReciboService: IReciboService
    {
        private readonly IReciboRepository _reciboRepository;
        private readonly IMapper _mapper;
        public ReciboService(IReciboRepository reciboRepository, IMapper mapper)
        {
            _reciboRepository = reciboRepository;
            _mapper = mapper;
        }
        public async Task<IList<ReciboDTO>> GetRecibos()
        {
            var Recibos = await _reciboRepository.GetRecibos();
            return  _mapper.Map<IList<ReciboDTO>>(Recibos);
          
        }
        public async Task<Tuple<bool,ReciboDetalleDTO>> AddRecibo(ReciboRequestModel recibo,int puntoVentaId)
        {
            var rec = _mapper.Map<Recibo>(recibo);
            rec.FacturasRecibos = _mapper.Map<List<FacturaRecibo>>(recibo.Detalles);


            var add = await _reciboRepository.AddRecibo(rec,puntoVentaId);
            if(add.Item1){
                var dto = _mapper.Map<ReciboDetalleDTO>(add.Item2);
                dto.Detalles = _mapper.Map<List<DetalleFacturasReciboDTO>>(add.Item2.FacturasRecibos);
                return new Tuple<bool, ReciboDetalleDTO>(true,dto);
            }

            return new Tuple<bool, ReciboDetalleDTO>(false, null);

        }
        public async Task<ReciboDTO> GetReciboNro(string nro)
        {
            return _mapper.Map<ReciboDTO>(await _reciboRepository.GetReciboNro(nro));

        }
        public async Task<ReciboDetalleDTO> GetRecibo(int id)
        {
            return _mapper.Map<ReciboDetalleDTO>(await _reciboRepository.GetRecibo(id));
        }
        public async Task UpdateRecibo(ReciboRequestModel recibo)

        {
            var rec = _mapper.Map<Recibo>(recibo);
            await _reciboRepository.UpdateRecibo(rec);

        }
        public async Task<bool> RemoveRecibo(int id)
        {
            var recibo = await _reciboRepository.GetRecibo(id);
            if(recibo != null)
            {
                await _reciboRepository.RemoveRecibo(recibo);
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
