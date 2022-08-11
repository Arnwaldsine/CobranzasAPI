using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Context;
using APICobranzas.Infra.Data.DataHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Interfaces;


namespace APICobranzas.Infra.Data.Repository
{
   public class FacturaRepository :IFacturaRepository
    {
        private readonly APIDbContext _context;
        public FacturaRepository(APIDbContext context)
        {
            _context = context;
        }

        public async Task<List<Factura>> GetFacturas()
        {

            var list = await _context.Facturas.OrderByDescending(x=>x.FechaEmision).ToListAsync();
            return list;
        }

        public async Task<List<Factura>> GetFacturasOS(int id)
        {
            return await _context.Facturas.OrderByDescending(z => z.FechaEmision).Where(x => x.ObraSocialId == id)
                .ToListAsync();
        }
        public async Task<Factura> GetFactura(int id)
        {
            var fac = await _context.Facturas
                .Include(a => a.Recibos)
                .Include(a => a.ObraSocial)
                .Include(a => a.PuntoVenta)
                .Include(a => a.Estado)
                .FirstOrDefaultAsync(a => a.Id == id);
       //     fac.Nro = $"{fac.PuntoVenta.Numero.ToString().PadLeft(4, '0')}-{fac.Id.ToString().PadLeft(8, '0')}";
            return fac;
         }
        public async Task<Factura> AddFactura(Factura factura)
        {
            factura.FechaAcuse = DateTime.Now;
            factura.FechaEmision = DateTime.Now;
            factura.ImporteCobrado = 0;
            
            var entity=  await  _context.Facturas.AddAsync(factura);


              await _context.SaveChangesAsync();
              var comp = await GetFactura(entity.Entity.Id);

             var a = comp;

         comp.Nro = comp.PuntoVenta.Numero.ToString().PadLeft(4, '0')+"-"+comp.Id.ToString().PadLeft(8, '0');
         await UpdateFactura(comp);

          return entity.Entity;
        }
        public async Task AnularFactura(int id)
        {
            var factura = await _context.Facturas.FindAsync(id);
            factura.EstadoId = 4;
            await _context.SaveChangesAsync();
        }
        public async Task UpdateFactura(Factura factura)
        {
            if(factura.ImporteCobrado == factura.ImporteFactura)
            {
                factura.EstadoId = 2;
                _context.Facturas.Update(factura);
                await _context.SaveChangesAsync();
            }
            else
            if(factura.ImporteCobrado<factura.ImporteFactura)
            {
                factura.EstadoId = 5;
                _context.Facturas.Update(factura);
                await _context.SaveChangesAsync();
            }

        }
        public async Task RemoveFactura(Factura factura)
        {
            _context.Facturas.Remove(factura);
            await _context.SaveChangesAsync();

        }

    /*    private void Ordenar(ref IQueryable<Factura> facturas, string orderByQueryString)
        {
            if (!facturas.Any())
                return;
            if(string.IsNullOrWhiteSpace(orderByQueryString))
            {
                facturas = facturas.OrderByDescending(x => x.FechaEmision);
                return;
            }

            var parametrosOrden = orderByQueryString.Trim().Split(',');
            var propiedades = typeof(Factura).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var ordenQueryBuilder = new StringBuilder();
            foreach (var p in parametrosOrden)
            {
                if (string.IsNullOrWhiteSpace(p))
                    continue;
                var propiedadQuery = p.Split("")[0];
                var propiedadObjeto = propiedades.FirstOrDefault(p =>
                    p.Name.Equals(propiedadQuery, StringComparison.InvariantCultureIgnoreCase));
                if (propiedadObjeto is null)
                    continue;
                var direccionOrden = p.EndsWith(" desc")? "descending":"ascending";
                ordenQueryBuilder.Append($"{propiedadObjeto.Name.ToString()} {direccionOrden}, ");

            }

            var QueryOrden = ordenQueryBuilder.ToString().TrimEnd(',', ' ');
            if (string.IsNullOrWhiteSpace(QueryOrden))
            {
                facturas = facturas.OrderBy(x => x.FechaEmision);
                return;
                
            }

            facturas = facturas.OrderBy(QueryOrden);
        }*/

    }
}
