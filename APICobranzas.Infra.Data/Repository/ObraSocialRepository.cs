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
   public class ObraSocialRepository:IObraSocialRepository
    {
        private readonly APIDbContext _context;
        public ObraSocialRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<IList<ObraSocial>> GetObrasSociales()
        {
            return await _context.ObrasSociales
                .Include(o => o.TipoPrestador)
                .OrderBy(x=>x.Nombre)
                .ToListAsync();
        }
        public async Task<IList<ObraSocial>> GetByTipo(int tipoId)
        {
            return await _context.ObrasSociales
                .Include(o => o.TipoPrestador)
                .Where(x=>x.TipoPrestadorId == tipoId)
                .OrderBy(z=>z.Nombre)
                .ToListAsync();
        }
        public async Task<ObraSocial> AddObraSocial(ObraSocial obraSocial)
        {
          var model =  await _context.ObrasSociales.AddAsync(obraSocial);
            await  _context.SaveChangesAsync();
            return model.Entity;

        }
        public async Task<ObraSocial> GetObraSocial(int id)
        {
            return await _context.ObrasSociales
                .Where(o => o.Id == id)
                .Include(o => o.Facturas)
                .ThenInclude(o => o.Estado)
                .Include(o=>o.TipoPrestador)
                .FirstOrDefaultAsync();
                
        }
        public async Task<bool> RemoveObraSocial(int id)
        {
            _context.Remove(id);
         var removed = await _context.SaveChangesAsync();
            return removed > 0;
        }
        public async Task<bool> UpdateObraSocial(ObraSocial obraSocial) {
            _context.ObrasSociales.Update(obraSocial);
            var updated = await _context.SaveChangesAsync();
            return updated > 0;
        }
    }
}
