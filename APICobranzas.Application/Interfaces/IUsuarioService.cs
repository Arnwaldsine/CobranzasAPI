using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.Account;
using APICobranzas.Domain.Models;

namespace APICobranzas.Application.Interfaces
{
   public interface IUsuarioService
    {
        Task<LoginResponse> Authenticate(LoginRequest model, string ipAddress);
        Task<LoginResponse> RefreshToken(string token, string dirIp);
        Task RevocarToken(string token, string ipAddress);
        Task Registrar(RegistroRequest model, string origin);
        Task VerificarEmail(string token);
        Task ForgotPassword(OlvidoPasswordRequest model, string origin);
        Task ValidarResetToken(ValidarTokenReinicioRequest model);
        Task ReiniciarPassword(ReinicioPasswordRequest model);
        Task<IEnumerable<UsuarioResponse>> GetUsuarios();
        Task<UsuarioResponse> GetById(int id);
        Task<UsuarioResponse> Crear(CrearRequest model);
        Task<UsuarioResponse> Modificar(int id, ModificarUsuarioRequest model);
        Task Eliminar(int id);

    }
}
