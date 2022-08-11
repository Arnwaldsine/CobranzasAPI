using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace APICobranzas.Api.Controllers
{
    [Route("api/tiposPrestador")]
    [ApiController]
    public class TiposPrestadorController : BaseController
    {
        private readonly ITipoPrestadorService _service;
        
        public TiposPrestadorController(ITipoPrestadorService service)
        {
            _service = service;
        }
        /// <summary>
        /// Muestrra la lista de tipos de prestador disponible
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TipoPrestadorDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IList<TipoPrestadorDTO>>> Get()
        {
            try
            {
                var list = await _service.GetTiposPrestador();
                if (!list.Any()) return NotFound("No hay tipos registrados.");
                return Ok(list);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Devuelve un tipo de prestador indicando su clave primaria, junto a los prestadores de ese tipo en particular
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPrestadorDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoPrestadorDTO>> GetById(int id)
        {
            try
            {
                var tp = await _service.GetTipoPrestador(id);
                if (tp is null) return NotFound("No se puedo encontrar el tipo solicitado");
                return Ok(tp);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
