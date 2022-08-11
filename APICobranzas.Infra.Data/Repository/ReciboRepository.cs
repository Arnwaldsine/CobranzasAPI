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
   
   
    public class ReciboRepository : IReciboRepository
    {
        private readonly APIDbContext _c;
        public ReciboRepository(APIDbContext c)
        {
            _c = c;
        }
        public async Task<IList<Recibo>> GetRecibos()
        {
            var recibos = await _c.Recibos
                .Include(a=>a.FacturasRecibos)
                    .ThenInclude(a=>a.Banco)
                .Include(a=>a.FacturasRecibos)
                    .ThenInclude(a=>a.FormaPago)
                .ToListAsync();
            foreach(var recibo in recibos)
            {
                recibo.Nro = $"{recibo.Facturas.First().PuntoVenta.Numero.ToString().PadLeft(4, '0')}-{recibo.Id.ToString().PadLeft(4, '0')}";
            }
            return recibos;
        }
        public async Task<Recibo> GetReciboNro(string nro)
        {
            var recibo =  await _c.Recibos.FirstOrDefaultAsync(x => x.Nro == nro);
            recibo.Nro = $"{recibo.Facturas.First().PuntoVenta.Numero.ToString().PadLeft(4, '0')}-{recibo.Id.ToString().PadLeft(8,'0')}";
            return recibo;
        }
        public async Task<Tuple<bool,Recibo>> AddRecibo(Recibo recibo,int puntoVentaId)
        {
            if (await MontosValidos(recibo.FacturasRecibos))
            {
                recibo.Total = recibo.FacturasRecibos.Sum(x => x.SubTotal);
                recibo.Fecha = DateTime.Now;
                var entity = await _c.Recibos.AddAsync(recibo);
                //aca va cobrofacturas
                await CobroFacturas(recibo);
                await _c.SaveChangesAsync();
                var completo = await GetRecibo(entity.Entity.Id);
                completo.Nro=  $"{puntoVentaId.ToString()?.PadLeft(4, '0')}-{entity.Entity.Id.ToString().PadLeft(8, '0')}";
            await _c.SaveChangesAsync();
             
                return new Tuple<bool, Recibo>(true,completo);
            }

            return new Tuple<bool, Recibo>(false, null);
            

        }
        public async Task<Recibo> GetRecibo(int id)
        {
            var recibo= await _c.Recibos
                .AsNoTracking() 
                .Include(r => r.FacturasRecibos)
                .ThenInclude(x=>x.Banco)
                .Include(r => r.FacturasRecibos)
                .ThenInclude(x=>x.FormaPago)
                .Include(r => r.FacturasRecibos)
                .ThenInclude(x=>x.Factura)
                .FirstOrDefaultAsync(x=>x.Id==id);
                
            return recibo;
        }

        public async Task<Recibo> UpdateRecibo(Recibo recibo) {
            var r =  _c.Recibos.Update(recibo);
            return r.Entity;
        }
        public async Task<bool> RemoveRecibo(Recibo recibo)
        {
            _c.Recibos.Remove(recibo);
            _c.FacturaRecibo.RemoveRange(_c.FacturaRecibo.Where(x => x.ReciboId == recibo.Id));
            var removed = await _c.SaveChangesAsync();
            return removed > 0;
        }

        public async Task<bool> MontosValidos(List<FacturaRecibo> lista)
        {
            foreach (var fac in lista)
            {
                if (fac.SubTotal > await _c.Facturas.Where(x => x.Id == fac.FacturaId).Select(s => s.ImporteDebe).FirstOrDefaultAsync())
                {
                    return false;
                }

            }
            return true;
        }

        public async Task CobroFacturas(Recibo recibo)
        {
            foreach (var det in recibo.FacturasRecibos)
            {
                det.ReciboId = recibo.Id;
                var factura = await _c.Facturas.FindAsync(det.FacturaId);
                factura.ImporteCobrado += det.SubTotal;
                factura.EstadoId = factura.ImporteCobrado == factura.ImporteFactura ? 2 : 5;
                factura.FechaUltPago = recibo.Fecha;
            }
        }

        public async Task<Recibo> ReciboCompleto(int id)
        {
            return await _c.Recibos.AsNoTracking()
                .Include(x => x.FacturasRecibos).ThenInclude(f => f.Banco)
                .Include(x => x.FacturasRecibos).ThenInclude(f => f.FormaPago)
                .Include(x => x.FacturasRecibos).ThenInclude(f => f.Factura)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AsignarNro(Recibo recibo)
        {

        }
    }
}

/*

 public async Task<bool> AddRecibo(Recibo recibo)
        {

             await _c.Recibos.AddAsync(recibo);
            
            await _c.FacturaRecibo.AddRangeAsync(recibo.FacturasRecibos);

           foreach(var detalle in recibo.FacturasRecibos)
            {
                try
                {
                    var factura = await _c.Facturas.FindAsync(detalle.FacturaId);

                    factura.ImporteCobrado = detalle.SubTotal;
                }
                catch(Exception ex)
                {
                    throw new Exception("Ha ocurrido un error: " + ex.Message);
                }
              
            }
            return await _c.SaveChangesAsync() > 0;
        }


            var ins = await _c.Recibos.AddAsync(new Recibo
            {
                 BancoId = recibo.BancoId,
                 Nro = recibo.Nro,
                 NroChequeTransf = recibo.NroChequeTransf,
                 NroReciboTes = recibo.NroReciboTes,
                 Total= recibo.Total,
                 FormaPagoId = recibo.FormaPagoId,
                 Fecha = recibo.Fecha
            });
            await _c.SaveChangesAsync();

            var last = facturas.Last();
            foreach (int id in facturas)
            {
                var montoRestanteRecibo = recibo.Total;
                var obj = new FacturaRecibo
                {
                    FacturaId = id,
                    ReciboId = ins.Entity.Id
                };
                _c.FacturaRecibo.Add(obj);
                var fAModificar = _c.Facturas.Find(id);
                if(montoRestanteRecibo >= fAModificar.ImporteDebe)
                {
                    fAModificar.ImporteCobrado = fAModificar.ImporteFactura;
                    montoRestanteRecibo = (decimal)(recibo.Total - fAModificar.ImporteCobrado);                    
                }
                else
                {
                    fAModificar.ImporteCobrado = fAModificar.ImporteCobrado+montoRestanteRecibo;
                }
                if (id.Equals(last))
                {
                    if(montoRestanteRecibo>fAModificar.ImporteFactura || montoRestanteRecibo > fAModificar.ImporteCobrado)
                    {
                        _c.Recibos.Remove(ins.Entity);
                        return false;
                    }                   
                }
               
            }
            var created = await _c.SaveChangesAsync();
            return created > 0;
            */