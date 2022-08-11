using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
    public interface INotaCreditoRepository
    {
        Task<List<NotaCredito>> GetNotas();
        Task<List<NotaCredito>> GetAnuladas();
        Task<NotaCredito> GetNota(int id);
        Task<bool> AnularNota(int id);
        Task<Tuple<bool, NotaCredito>> AddNota(NotaCredito nota);
        Task setEstados(NotaCredito nota);
        Task<bool> MontosValidos(IList<FacturaNota> fn);
        Task<NotaCredito> NotaCompleta(int id);

    }
}
