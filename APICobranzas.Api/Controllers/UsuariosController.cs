using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Account;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Models;
using AutoMapper;

namespace APICobranzas.Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : BaseController
    {
        private readonly IUsuarioService  _usuarioService;
        private readonly IMapper _mapper;

        public UsuariosController(
            IUsuarioService accountService,
            IMapper mapper)
        {
              _usuarioService = accountService;
            _mapper = mapper;
        }
        /// <summary>
        /// Login de usuario, una vez ya registrado y verificado.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest model)
        {
            var response = await _usuarioService.Authenticate(model, dirIP());
            AgregarTokenYPuntoVentaACookies(response.RefreshToken,response.PuntoVenta.Id);
            return Ok(response);
        }
        /// <summary>
        /// Renueva el token de acceso de un usuario ya autenticado
        /// </summary>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<ActionResult<LoginResponse>> RefrescarToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            var response = await _usuarioService.RefreshToken(refreshToken, dirIP());
            AgregarTokenYPuntoVentaACookies(response.RefreshToken,response.PuntoVenta.Id);
            return Ok(response);
        }
        /// <summary>
        /// Revoca o invalida el token actual del usuario autenticado
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost("revoke-token")]
        public async  Task<IActionResult> RevocarToken(RevocarTokenRequest model)
        {
            // accept token from request body or cookie
            var token = model.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest(new { message = "Token requerido" });

            // users can revoke their own tokens and admins can revoke any tokens
            if (!Usuario.TieneToken(token) && Usuario.Rol != Rol.Administrador)
                return Unauthorized(new { message = "Sin autorizacion" });

             await _usuarioService.RevocarToken(token, dirIP());
            return Ok(new { message = "Token revocado" });
        }
        /// <summary>
        /// Registro de usuario
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Registrar(RegistroRequest model)
        {
            await _usuarioService.Registrar(model, Request.Headers["origin"]);
            return Ok(new { message = "Registro exitoso. Revise su casilla de mail y siga las instrucciones"});
        }
        /// <summary>
        /// Verificacion del email pasado en el metodo "Registrar"
        /// </summary>
        /// <param name="model">Token recibido como parametro de url en el mail</param>
        /// <returns></returns>
        [HttpPost("verify-email")]
        public async Task<IActionResult> VerificarEmail(VerificarEmailRequest model)
        {
            await _usuarioService.VerificarEmail(model.Token);
            return Ok(new { message = "Cuenta verificada! Ya puede logearse" });
        }
        /// <summary>
        /// Olvido de Password, envio de mail con instrucciones
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("forgot-password")]
        public async Task<IActionResult> OlvidoPassword(OlvidoPasswordRequest model)
        {
            await _usuarioService.ForgotPassword(model, Request.Headers["origin"]);
            return Ok(new { message = "Revise su casilla de mail y siga las instrucciones para reiniciar su password" });
        }
        /// <summary>
        /// Valida el Token de reinicio de contraseña que llega al correo
        /// </summary>
        /// <param name="model">Token enviado por correo electronico al usuario</param>
        /// <returns></returns>
        [HttpPost("validate-reset-token")]
        public async Task<IActionResult> ValidarTokenReinicio(ValidarTokenReinicioRequest model)
        {
            await _usuarioService.ValidarResetToken(model);
            return Ok(new { message = "Token is valid" });
        }
        /// <summary>
        /// Reinicio de contraseña
        /// </summary>
        /// <param name="model">Password nueva</param>
        /// <returns></returns>
        [HttpPost("reset-password")]
        public async Task<IActionResult> ReiniciarPassword(ReinicioPasswordRequest model)
        {
            await _usuarioService.ReiniciarPassword(model);
            return Ok(new { message = "Reinicio de password exitoso!" });
        }
        /// <summary>
        /// Lista completa de usuarios. Solo el adminsitrador puede hacerlo.
        /// </summary>
        /// <returns></returns>
        [Authorize(Rol.Administrador)]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetUsuarios()
        {
            var accounts = await _usuarioService.GetUsuarios();
            return Ok(accounts);
        }
        /// <summary>
        /// Devuelve un usuario indicando su clave primaria. Solo se puede ver el usuario propio, excepto el administrador 
        /// </summary>
        /// <param name="id">Id de usuario</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UsuarioResponse>> GetById(int id)
        {
     
            if (id != Usuario.Id && Usuario.Rol != Rol.Administrador)
                return Unauthorized(new { message = "Sin autorizacion para realizar la accion solicitada" });

            var account = await _usuarioService.GetById(id);
            return Ok(account);
        }
        /// <summary>
        /// Crea un usuario en el sistema, solo para administrador.
        /// </summary>
        /// <param name="model">Modelo de usuario a crear</param>
        /// <returns></returns>
        [Authorize(Rol.Administrador)]
        [HttpPost]
        public async Task<ActionResult<UsuarioResponse>> CrearUsuario(CrearRequest model)
        {
            var account = await _usuarioService.Crear(model);
            return Ok(account);
        }
        /// <summary>
        /// Modifica el usuario propio,excepto el administrador que puede cambiar cualquier usuario
        /// </summary>
        /// <param name="id">Clave primaria del usuario</param>
        /// <param name="model">Modelo con datos modificados del usuario</param>
        /// <returns></returns>
        [Authorize]
        [HttpPut("{id:int}")]
        public async Task<ActionResult<UsuarioResponse>> Modificar(int id, ModificarUsuarioRequest model)
        {

            if (id != Usuario.Id && Usuario.Rol != Rol.Administrador)
                return Unauthorized(new { message = "Sin autorizacion para realizar la accion solicitada" });

            // only admins can update role
            if (Usuario.Rol != Rol.Administrador)
                model.Rol = null;

            var account = await _usuarioService.Modificar(id, model);
            return Ok(account);
        }
        /// <summary>
        /// Elimina un usuario, solamente administrador.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Eliminar(int id)
        {
           
            if (id != Usuario.Id && Usuario.Rol != Rol.Administrador)
                return Unauthorized(new { message = "Sin autorizacion para realizar la accion solicitada" });

            await _usuarioService.Eliminar(id);
            return Ok(new { message = "Cuenta eliminada exitosamente." });
        }

        // Agrega los datos del punto de venta y del Token a las cookies del usuario
        private void AgregarTokenYPuntoVentaACookies(string token,int puntoVentaId)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(1)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
            Response.Cookies.Append("puntoVentaId",puntoVentaId.ToString(),cookieOptions);
        }
     

        private string dirIP()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
                return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
