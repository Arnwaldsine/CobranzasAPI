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
    public class PuntoVentaRepository : IPuntoVentaRepository
    {
        private readonly APIDbContext _context;
        public PuntoVentaRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<PuntoVenta>> GetPuntosVenta()
        {
            return await _context.PuntosVenta.ToListAsync();
        }
        public async Task<PuntoVenta> GetPuntoVenta(int id)
        {
            return await _context.PuntosVenta
                .Where(p => p.Id == id)
                .Include(p=>p.Facturas)
                    .ThenInclude(p=>p.ObraSocial)
                    .ThenInclude(p=>p.Facturas)
                        .ThenInclude(f=>f.Estado)
                .FirstOrDefaultAsync();
        }
    }
}
