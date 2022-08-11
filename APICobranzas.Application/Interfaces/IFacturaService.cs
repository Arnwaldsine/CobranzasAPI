using APICobranzas.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Factura;
using APICobranzas.Domain.Models;


namespace APICobranzas.Application.Interfaces
{
    public interface IFacturaService
    {
        Task<List<FacturaItemDTO>> GetFacturas();
        Task<FacturaDTO> GetFactura(int id);
        Task<bool> AnularFactura(int id);
        Task<FacturaDTO> AddFactura(FacturaRequestModel factura, int puntoVentaId);
        Task<bool> RemoveFactura(int id);
        Task<bool> UpdateFactura(FacturaRequestModel factura);
        Task<List<FacturaItemDTO>> GetFacturasOS(int id);
    }
}
