using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APICobranzas.Api.Controllers
{
    [ApiController]
    [Route("api/bancos")]
    public class BancosController:BaseController
    {
        private readonly IBancoService _service;

        public BancosController(IBancoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Devuelve la lista de bancos registrados
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<BancoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<BancoDTO>>> Get()
        {
            try
            {
                var lista = await _service.GetBancos();
                if (!lista.Any()) return NotFound("No se han encontrado bancos registrados");
                return Ok(lista);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
