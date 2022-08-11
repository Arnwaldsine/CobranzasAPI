using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Domain.Interfaces;
using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Context;
using APICobranzas.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace APICobranzas.Infra.Data.Repository
{
    // MANEJA EL CRUD DE LA TABLA DE USUARIOS
    public class UsuarioRepository : IUserRepository
    {
        private readonly APIDbContext _c;

        public UsuarioRepository(APIDbContext c)
        {
            _c = c;
        }

        public async Task<Usuario> GetById(int id)
        {
            return await _c.Usuarios.AsNoTracking().Include(x => x.PuntoVenta).SingleOrDefaultAsync(z=>z.Id ==id);
        }

        public  async Task<Usuario> GetByEmail(string email)
        {
            return await _c.Usuarios.AsNoTracking().Include(x => x.PuntoVenta).SingleOrDefaultAsync(x => x.Email == email);
        }

        public async Task UpdateUser(Usuario usuario)
        {
            usuario.Email = usuario.Email.ToLower();
            _c.Usuarios.Update(usuario);
            await _c.SaveChangesAsync();
        }

        public async Task<List<Usuario>> GetAll()
        {
            return await _c.Usuarios.AsNoTracking().Include(x=>x.PuntoVenta).ToListAsync();
        }

        public async Task<bool> HayUsuarios()
        {
            return await _c.Usuarios.AnyAsync();
        }

        public async Task AddUser(Usuario usuario)
        {
           var model =  await _c.Usuarios.AddAsync(usuario);
          await  _c.SaveChangesAsync();

        }

        public async Task<Usuario> BuscarPorToken(string token)
        {
            return await _c.Usuarios.SingleOrDefaultAsync(x => x.TokenDeVerificacion == token);
        }

        public async Task<Usuario> ValidarResetToken(string token)
        {
            return await _c.Usuarios.SingleOrDefaultAsync(x =>
                x.TokenReinicio == token &&
                x.FechaExpiracionTokenReseteo > DateTime.UtcNow);
        }

        public async Task DeleteUser(Usuario usuario)
        {
            _c.Usuarios.Remove(usuario);
           await _c.SaveChangesAsync();
        }

        public async Task <Usuario> BuscarPorRefreshToken(string token)
        {
            return await _c.Usuarios.SingleOrDefaultAsync(u => u.RefreshTokens.Any(t => t.Token == token));
        }
        
    }
}
