using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;


namespace APICobranzas.Domain.Interfaces
{
   public interface IFacturaRepository
    {
        Task AnularFactura(int id);
        Task<List<Factura>> GetFacturas();
        Task<Factura> GetFactura(int id);
        Task<Factura> AddFactura(Factura factura);
        Task UpdateFactura(Factura factura);
        Task RemoveFactura(Factura factura);
        Task<List<Factura>> GetFacturasOS(int id);

    }
}
