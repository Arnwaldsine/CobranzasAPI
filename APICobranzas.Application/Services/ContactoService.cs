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
    public class ContactoService : IContactoService
    {
        private readonly IContactoRepository _contactoRepository;
        private readonly IMapper _mapper;
        public ContactoService(IContactoRepository contactoRepository, IMapper mapper)
        {
            _contactoRepository = contactoRepository;
            _mapper = mapper;
        }
        public async Task<IList<ContactoDTO>> GetContactos() {
            return _mapper.Map<IList<ContactoDTO>>(await _contactoRepository.GetContactos());
        }
    }
}
