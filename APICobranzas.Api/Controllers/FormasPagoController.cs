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
    [Route("api/formasPago")]
    public class FormasPagoController:BaseController
    {
        private readonly IFormaPagoService _service;

        public FormasPagoController(IFormaPagoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Devuelve una lista con las formas de pago disponibles cuando se emite un recibo
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FormaPagoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize()]
        [HttpGet]
        public async Task<ActionResult<List<FormaPagoDTO>>> Get()
        {
            try
            {
                var list =await  _service.GetFormasPago();
                if (!list.Any()) return NotFound("No hay formas de pago registradas en el sistema");
                return Ok(list);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
