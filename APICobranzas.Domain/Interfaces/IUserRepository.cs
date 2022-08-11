using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Domain.Interfaces
{
   public interface IUserRepository
   {
       Task<Usuario> GetById(int id);
       Task<Usuario> GetByEmail(string email);
       Task UpdateUser(Usuario usuario);
       Task<List<Usuario>> GetAll();
       Task<bool> HayUsuarios();

       Task AddUser(Usuario usuario);
       Task<Usuario> BuscarPorToken(string token);
       Task<Usuario> ValidarResetToken(string token);
       Task DeleteUser(Usuario usuario);
       Task<Usuario> BuscarPorRefreshToken(string token);

   }
}
