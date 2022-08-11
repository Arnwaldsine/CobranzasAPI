using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Interfaces;

namespace APICobranzas.Infra.Data.Repository
{
    public class ContactoRepository:IContactoRepository
    {
        private readonly APIDbContext _context;
        public ContactoRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Contacto>> GetContactos()
        {
            return await _context.Contactos.ToListAsync();
        }
        public async Task<Contacto>GetContacto(int id)
        {
            return await _context.Contactos.FindAsync(id);
        }
    }
}
