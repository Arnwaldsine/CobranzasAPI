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
    public class TipoPrestadorRepository: ITipoPrestadorRepository
    {
        private readonly APIDbContext _context;
        public TipoPrestadorRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<TipoPrestador>> GetTiposPrestador()
        {
            return await _context.TiposPrestador.ToListAsync();

        }
        public async Task<TipoPrestador> GetTipoPrestador(int id)
        {
            return await _context.TiposPrestador
                .Where(t => t.Id == id)
                .Include(t => t.ObrasSociales)
                .FirstOrDefaultAsync();
        }
    }
}
