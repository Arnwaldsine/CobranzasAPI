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
    public class FormaPagoService: IFormaPagoService
    {
        private readonly IMapper _mapper;
        private readonly IFormaPagoRepository _repo;
        public FormaPagoService(IMapper mapper,IFormaPagoRepository repo)
        {
            _mapper = mapper;
            _repo = repo;
        }
        public async Task<IList<FormaPagoDTO>>GetFormasPago() {
            return _mapper.Map<IList<FormaPagoDTO>>(await _repo.GetFormasPago());
        }
    }
}
