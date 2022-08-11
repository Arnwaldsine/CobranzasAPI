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
    public class GestionRepository:IGestionRepository
    {
        private readonly APIDbContext _context;
        public GestionRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task RemoveGestion(int id)
        {
            _context.Remove(id);
            await _context.SaveChangesAsync();
        }
        public async Task<(IList<Gestion>,List<Factura>)> GetGestiones(int obraSocialId,int puntoVentaId)
        {
            if (await _context.ObrasSociales.FindAsync(obraSocialId) is null) return (null, null);
            return (await _context.Gestiones
                .Include(g => g.Usuario)
                .ThenInclude(a => a.PuntoVenta)
                .Include(g => g.Contacto)
                .Include(g => g.Respuesta)              
                .Where(x=>x.ObraSocialId == obraSocialId)
                .OrderByDescending(z=>z.FechaContacto)
                .ToListAsync(), await _context.Facturas.Where(x=>x.ImporteDebe>0 ).OrderBy(z=>z.FechaEmision).ToListAsync());
        }
        public async Task<Gestion> GetGestion(int id)
        {
            return await _context.Gestiones.AsNoTracking()
                .Include(g => g.Usuario)
                .ThenInclude(a => a.PuntoVenta)
                .Include(g => g.Contacto)
                .Include(g => g.ObraSocial)
                .Include(g => g.Respuesta)
                .FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<Gestion> UpdateGestion(Gestion gestion)
        {
            _context.Gestiones.Update(gestion);
            await _context.SaveChangesAsync();
            return await GetGestion(gestion.Id);

        }
        
        public async Task<IList<Gestion>> GetGestionesPendientes() {
            return await _context.Gestiones.AsNoTracking().Include(z=>z.ObraSocial)
                .Include(x=>x.ObraSocial.Facturas)
                .Where(a => a.FechaProxContacto == DateTime.Now  )
                .ToListAsync();
        }

        public async Task<Gestion> AddGestion(Gestion gestion)
        {
            gestion.FechaContacto = DateTime.Now;
            var g = await _context.Gestiones.AddAsync(gestion);
            await _context.SaveChangesAsync();
            return await GetGestion(g.Entity.Id);
        }
    }
}
