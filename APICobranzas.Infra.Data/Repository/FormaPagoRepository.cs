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
    public class FormaPagoRepository : IFormaPagoRepository
    {
        private readonly APIDbContext _context;
        public FormaPagoRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<FormaPago>> GetFormasPago()
        {
            return await _context.FormasPago.ToListAsync();
        }
    }
}
