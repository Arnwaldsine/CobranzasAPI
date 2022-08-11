using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Context;
using APICobranzas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICobranzas.Infra.Data.Repository
{
    public class NotaCreditoRepository : INotaCreditoRepository
    {
        private readonly APIDbContext _c;

        public NotaCreditoRepository(APIDbContext context)
        {
            _c = context;
        }

        public async Task<List<NotaCredito>> GetNotas()
        {
            return await _c.NotasCredito.Include(z=>z.Facturas).ThenInclude(z=>z.ObraSocial).ToListAsync();
        }

        public async Task<List<NotaCredito>> GetAnuladas()
        {
            return await _c.NotasCredito.Where(x => x.Anulado == true).ToListAsync();
        }
        public async Task<NotaCredito> GetNota(int id)
        {                                                                                                                                                                                                       
            return await _c.NotasCredito.Include(x=>x.FacturasNotas)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }

        public async Task<bool> AnularNota(int id)
        {
            var nota = await _c.NotasCredito.FindAsync(id);
            if (nota.Anulado == true)
            {
                return false;
            }

            nota.Anulado = true;
            foreach (var v in nota.FacturasNotas)
            {
                var factura = await _c.Facturas.FindAsync(v.FacturaId);
                factura.ImporteFactura += v.Subtotal;
            }

            await _c.SaveChangesAsync();
            return true;
        }
        public async Task<Tuple<bool,NotaCredito>> AddNota(NotaCredito nota)
        {
            if (!await MontosValidos(nota.FacturasNotas)) return new Tuple<bool, NotaCredito>(false, null);
            nota.Total = nota.FacturasNotas.Sum(x => x.Subtotal);
            
            var creado = await _c.NotasCredito.AddAsync(nota);
          /*  foreach (var det in crea.FacturasNotas)
            {
                det.NotaCreditoId = nota.Id;
            }*/
            await setEstados(nota);

            await _c.SaveChangesAsync();
            var n = await NotaCompleta(creado.Entity.Id);
            return new Tuple<bool, NotaCredito>(true, n);

        }

        public async Task setEstados(NotaCredito nota)
        {
            foreach (var det in nota.FacturasNotas)
            {
                det.FacturaId = nota.Id;
                var factura = await _c.Facturas.FindAsync(det.FacturaId);

                if (det.Subtotal == factura.ImporteFactura)
                {
                    factura.EstadoId = 4;
                }
                else
                {
                    factura.EstadoId = 9;
                }

            }
        }
        public async Task<bool> MontosValidos(IList<FacturaNota> fn)
        {
            foreach (var fac in fn)
            {
                if (fac.Subtotal > await _c.Facturas.Where(x => x.Id == fac.FacturaId).Select(x => x.ImporteFactura)
                    .FirstOrDefaultAsync()) return false;

            }

            return true;
        }

        public async Task<NotaCredito> NotaCompleta(int id)
        {
            return await _c.NotasCredito.AsNoTracking()
                .Include(x => x.FacturasNotas)
                .ThenInclude(x => x.Factura)
                .Include(x => x.FacturasNotas)
                .ThenInclude(x => x.FormaPago)
                .Include(x => x.FacturasNotas)
                .ThenInclude(x => x.Factura)
                .ThenInclude(x => x.ObraSocial)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
