using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.Account;
using APICobranzas.Application.Helpers;
using APICobranzas.Domain.Interfaces;
using Microsoft.Extensions.Options;
using BC = BCrypt.Net.BCrypt;
using IEmailService = APICobranzas.Application.Interfaces.IEmailService;

namespace APICobranzas.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        private readonly IEmailService _emailService;

        public UsuarioService(
            IUserRepository repo,
            IMapper mapper,
            IOptions<AppSettings> appSettings,
            IEmailService emailService)
        {
            _repo = repo;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _emailService = emailService;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest model, string ipAddress)
        {
            var account =  await _repo.GetByEmail(model.Email);

            if (account == null ||  !account.EstaVerificado || !BC.Verify(model.Password, account.PasswordHash))
                throw new AppException("El Email o la contraseña son incorrectas");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = GenerarJwtToken(account);
            var refreshToken = GenerarRefreshToken(ipAddress);
            account.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from account
            eliminarTokenViejo(account);

            // save changes to db
           await  _repo.UpdateUser(account);

            var response = _mapper.Map<LoginResponse>(account);
            response.JwtToken = jwtToken;
            response.RefreshToken = refreshToken.Token;
            return response;
        }

        public async Task<LoginResponse> RefreshToken(string token, string dirIp)
        {
            var (refreshToken, cuenta) = await ObtenerRefreshToken(token);

           
            var newRefreshToken = GenerarRefreshToken(dirIp);
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = dirIp;
            refreshToken.ReplacedByToken = newRefreshToken.Token;
            cuenta.RefreshTokens.Add(newRefreshToken);

            eliminarTokenViejo(cuenta);

           await _repo.UpdateUser(cuenta);

       
            var jwtToken =  GenerarJwtToken(cuenta);

            var respuesta = _mapper.Map<LoginResponse>(cuenta);
            respuesta.JwtToken = jwtToken;
            respuesta.RefreshToken = newRefreshToken.Token;
            return respuesta;
        }

        public async Task RevocarToken(string token, string ipAddress)
        {
            var (refreshToken, account) =await  ObtenerRefreshToken(token);

            // revoke token and save
            refreshToken.Revoked = DateTime.UtcNow;
            refreshToken.RevokedByIp = ipAddress;
            await _repo.UpdateUser(account);
        }

        public async Task Registrar(RegistroRequest model, string origin)
        {
            model.Email = model.Email.ToLower();
           
            if (await _repo.GetByEmail(model.Email) is not null)
            {
                
                enviarEmailYaExistente(model.Email, origin);
                return;
            }

           
            var account = _mapper.Map<Usuario>(model);

           
            var isFirstAccount = await _repo.HayUsuarios();
            account.Rol= isFirstAccount ? Rol.Administrador : Rol.Usuario;
            account.Creado = DateTime.UtcNow;
            account.TokenDeVerificacion = TokenAleatorio();

           
            account.PasswordHash = BC.HashPassword(model.Password);

           
            await _repo.AddUser(account);
            
            enviarMailVerificacion(account, origin);
        }

        public async Task VerificarEmail(string token)
        {
            var account = await _repo.BuscarPorToken(token);

            if (account == null) throw new AppException("La verificacion por mail ha fallado.");

            account.FechaVerificion= DateTime.UtcNow;
            account.TokenDeVerificacion = null;

            await _repo.UpdateUser(account);
        }

        public async Task ForgotPassword(OlvidoPasswordRequest model, string origin)
        {
            var account = await _repo.GetByEmail(model.Email);

           
            if (account == null) return;

     
            account.TokenReinicio = TokenAleatorio();
            account.FechaExpiracionTokenReseteo = DateTime.UtcNow.AddDays(1);
           await  _repo.UpdateUser(account);

            // send email
            enviarMaiReinicioPassword(account, origin);
        }

        public async Task ValidarResetToken(ValidarTokenReinicioRequest model)
        {
            var cuenta =await  _repo.ValidarResetToken(model.Token);

            if (cuenta == null)
                throw new AppException("El Token es invalido");
        }

        public async Task ReiniciarPassword(ReinicioPasswordRequest model)
        {
            var c = await _repo.ValidarResetToken(model.Token);

            if (c == null)
                throw new AppException("El Token es invalido");

            // update password and remove reset token
            c.PasswordHash = BC.HashPassword(model.Password);
            c.FechaReseteoPassword = DateTime.UtcNow;
            c.TokenReinicio= null;
            c.FechaExpiracionTokenReseteo= null;

            await _repo.UpdateUser(c);
        }

        public async Task<IEnumerable<UsuarioResponse>> GetUsuarios()
        {
            var cuentas = _repo.GetAll();
            return _mapper.Map<IList<UsuarioResponse>>(cuentas);
        }

        public async Task<UsuarioResponse> GetById(int id)
        {
            var account = await ObtenerUsuario(id);
            return _mapper.Map<UsuarioResponse>(account);
        }

        public async Task<UsuarioResponse> Crear(CrearRequest model)
        {
            model.Email = model.Email.ToLower();
            // validate
            if (await _repo.GetByEmail(model.Email) is not null)
                throw new AppException($"Email '{model.Email}' is already registered");

            // map model to new account object
            var account = _mapper.Map<Usuario>(model);
            account.Creado = DateTime.UtcNow;
            account.FechaVerificion = DateTime.UtcNow;

            // hash password
            account.PasswordHash = BC.HashPassword(model.Password);

            // save account
        await   _repo.AddUser(account);

            return _mapper.Map<UsuarioResponse>(account);
        }

        public async Task<UsuarioResponse> Modificar(int id, ModificarUsuarioRequest model)
        {
            var cuenta =  await ObtenerUsuario(id);

            // validate
            if (cuenta.Email != model.Email && await _repo.GetByEmail(model.Email) is not null)
                throw new AppException($"El Email '{model.Email}' ya esta en uso.");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                cuenta.PasswordHash = BC.HashPassword(model.Password);

            // copy model to account and save
            _mapper.Map(model, cuenta);
            cuenta.Actualizado = DateTime.UtcNow;
            await _repo.UpdateUser(cuenta);
            return _mapper.Map<UsuarioResponse>(cuenta);
        }

        public async Task Eliminar(int id)
        {
            var cuenta = await ObtenerUsuario(id);
           await _repo.DeleteUser(cuenta);
        }

        // helper methods

        private async Task<Usuario> ObtenerUsuario(int id)
        {
            var cuenta = await _repo.GetById(id);
            if (cuenta == null) throw new KeyNotFoundException("No se ha encontrado la cuenta");
            return cuenta;
        }

        private async Task<(RefreshToken, Usuario)> ObtenerRefreshToken(string token)
        {
            var cuenta =await _repo.BuscarPorRefreshToken(token); 
            if (cuenta == null) throw new AppException("Invalid token");
            var refreshToken = cuenta.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive) throw new AppException("Invalid token");
            return (refreshToken, cuenta);
        }

        private string GenerarJwtToken(Usuario account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", account.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        private RefreshToken GenerarRefreshToken(string dirIP)
        {
            return new RefreshToken
            {
                Token = TokenAleatorio(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = dirIP
            };
        }

        private void eliminarTokenViejo(Usuario account)
        {
            account.RefreshTokens.RemoveAll(x =>
                !x.IsActive &&
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private string TokenAleatorio()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }

        private void enviarMailVerificacion(Usuario account, string origin)
        {
            string message;
            if (!string.IsNullOrEmpty(origin))
            {
                var verifyUrl = $"{origin}/account/verify-email?token={account.TokenDeVerificacion}";
                message = $@"<p>Por favor, haga click en el siguiente link para verificar su cuenta:</p>
                             <p><a href=""{verifyUrl}"">{verifyUrl}</a></p>
                              <p>Muchas gracias - Franco Arnaudin</p>";
            }
            else
            {
                message = $@"<p>Por favor, use el token siguiente para verificar su cuenta con la siguiente ruta <code>/accounts/verify-email</code></p>
                             <p><code>{account.TokenDeVerificacion}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Validacion de cuenta - API de Franco Arnaudin",
                html: $@"<h4>Verifique su email</h4>
                         <p>Gracias por registrarse!!</p>
                         {message}"
            );
        }

        private void enviarEmailYaExistente(string email, string origin)
        {
            string mensaje;
            if (!string.IsNullOrEmpty(origin))
                mensaje = $@"<p>Si no recuerda su password, visite la seccion <a href=""{origin}/account/forgot-password"">olvide mi password</a></p>";
            else
                mensaje = "<p>Si no sabe su password, puede reiniciarla mediante la siguiente direccion <code>/accounts/forgot-password</code> api route.</p>";

            _emailService.Send(
                to: email,
                subject: "Validacion de cuenta - API de Franco Arnaudin",
                html: $@"<h4>Email en uso</h4>
                         <p>su direccion de email <strong>{email}</strong> ya se encuentra en uso.</p>
                         {mensaje}"
            );
        }

        private void enviarMaiReinicioPassword(Usuario account, string origin)
        {
            string mensaje;
            if (!string.IsNullOrEmpty(origin))
            {
                var urlReinicio = $"{origin}/account/reset-password?token={account.TokenReinicio}";
                mensaje = $@"<p>Por favor, haga click en el siguiente link para reiniciar su password. El link es valido por un dia:</p>
                             <p><a href=""{urlReinicio}"">{urlReinicio}</a></p>";
            }
            else
            {
                mensaje = $@"<p>Por favor, utilice el siguiente token para reiniciar su password <code>/accounts/reset-password</code> api route:</p>
                             <p><code>{account.TokenReinicio}</code></p>";
            }

            _emailService.Send(
                to: account.Email,
                subject: "Validacion de cuenta - API de Franco Arnaudin",
                html: $@"<h4>Email de reinicio de Password</h4>
                         {mensaje}"
            );
        }
    }
}
