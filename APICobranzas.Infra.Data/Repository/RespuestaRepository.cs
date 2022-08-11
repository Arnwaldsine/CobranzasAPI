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
    public class RespuestaRepository : IRespuestaRepository
    {
        private readonly APIDbContext _context;
        public RespuestaRepository(APIDbContext context)
        {
            _context = context;
        }
        public async Task<IList<Respuesta>> GetRespuestas()
        {
            return await _context.Respuestas.ToListAsync();
        }
        public async Task<Respuesta>GetRespuesta(int id)
        {
            return await _context.Respuestas
                .Include(x => x.Gestiones)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
