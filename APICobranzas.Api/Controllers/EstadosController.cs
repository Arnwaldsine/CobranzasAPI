using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.Helpers;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICobranzas.Api.Controllers
{
    [Route("api/estados")]
    [ApiController]
    public class EstadosController : BaseController
    {
        private readonly IEstadoService _service;
        public EstadosController(IEstadoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Retorna los posibles estados de una factura. El valor por
        /// defecto es el de id 1.
        /// </summary>
        /// <returns></returns>
        // GET: api/<EstadosController>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<EstadoDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IList<EstadoDTO>>> Get()
        {
               try {
                var list = await _service.GetEstados();
                if (!list.Any()) return NotFound("No hay estados de factura registradas");
                return Ok(list);

               }catch (Exception ex) {
                return Problem(ex.Message);
               }
        }
        /*
        /// <summary>
        /// Retorna un estado y las facturas asociadas al mismo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<EstadosController>/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(EstadoDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoDTO>> Get(int id)
        {
            try
            {
                var es = await _service.GetEstados();
                if (es is null) return NotFound("El estado solicitado no existe");
                return Ok(es);

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }*/
    
    }
}
