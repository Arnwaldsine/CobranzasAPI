using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Interfaces;

namespace APICobranzas.Api.Controllers
{
    [Route("api/contactos")]
    [ApiController]
    public class ContactosController : BaseController
    {

        private readonly IContactoService _service;

        public ContactosController(IContactoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Devuelve los tipos de contacto, es decir, el rol que ocupa la persona contactada para el cobro de facturas
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Resultado exitoso, hay al menos un contacto registrado</response>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ContactoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        [HttpGet]
        public async Task<ActionResult<IList<ContactoDTO>>> Get()
        {
            try
            {
                var list = await _service.GetContactos();
                if (!list.Any()) return NotFound("No hay contactos registrados.");
                return Ok(list);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
    }
}
