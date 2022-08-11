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
    public class BancoRepository:IBancoRepository
    {
        private readonly APIDbContext _context;
        public BancoRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Banco>> GetBancos()
        {
            return await _context.Bancos.ToListAsync();
        }
    }
}
