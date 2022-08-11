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
   public class EstadoRepository : IEstadoRepository
    {
        private readonly APIDbContext _context;
        public EstadoRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<IList<Estado>> GetEstados()
        {
            return await _context.Estados.ToListAsync();
        }
        public async Task<Estado>GetEstado(int id)
        {
            return await _context.Estados
                .AsNoTracking()
                .Include(a => a.Facturas)
                    .ThenInclude(a => a.PuntoVenta)
                .Include(a=>a.Facturas)
                    .ThenInclude(a=>a.ObraSocial)
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
