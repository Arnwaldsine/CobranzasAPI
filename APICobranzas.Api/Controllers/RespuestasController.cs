using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;

namespace APICobranzas.Api.Controllers
{
    [Route("api/respuestas")]
    [ApiController]
    public class RespuestasController : ControllerBase
    {
        private readonly IRespuestaService _service;
        public RespuestasController(IRespuestaService service)
        {
            _service = service;
        }
        /// <summary>
        /// Devuelve las distintas respuestas que pueden surgir al contactar a la obra social deudora
        /// </summary>
        /// <returns>Una lista de respuestas posibles</returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RespuestaDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        public async Task<ActionResult<IList<RespuestaDTO>>> Get()
        {
            try
            {
                var list = await _service.GetRespuestas();
                if (!list.Any()) return NotFound("No hay respuestas registradas");
                return Ok(list);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
