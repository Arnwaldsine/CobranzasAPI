using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
   public interface IContactoRepository
    {
        Task<ICollection<Contacto>> GetContactos();
        Task<Contacto> GetContacto(int id);
    }
}
