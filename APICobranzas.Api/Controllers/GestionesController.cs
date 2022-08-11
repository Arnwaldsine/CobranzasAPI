using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.Gestion;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICobranzas.Api.Controllers
{
    [Route("api/gestiones")]
    [ApiController]
    public class GestionesController : BaseController
    {
        private readonly IGestionService _service;

        public GestionesController(IGestionService service)
        {
            _service = service;
        }
        /// <summary>
        /// Devuelve el detalle de las gestiones realizadas para poder cobrar las facturas de una obra social deudora
        /// </summary>
        /// <param name="obraSocialId"></param>
        /// <returns></returns>
        // GET: api/<GestionesController>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(DetalleCompletoGestionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("obraSocial/{obraSocialId}")]
        public async Task<ActionResult<DetalleCompletoGestionDTO>> Get(int obraSocialId)
        {
            try
            {
                var det  = await _service.GetGestiones(obraSocialId,Usuario.PuntoVentaId);
                if (!det.Gestiones.Any() || !det.Facturas.Any()) 
                    return NotFound("No se pueden hallar las gestiones de la obra social seleccionada");
                return Ok(det);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Obtiene la lista completa de gestiones pendientes
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<GestionDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        [HttpGet]
        public async Task<ActionResult<List<GestionDTO>>> GetPendientes()
        {
            try
            {
                var list = await _service.GetGestionesPendientes();
                if (!list.Any())
                {
                    return NotFound("No hay gestiones pendientes pendientes para hoy. Felicidades!");
                }

                return Ok(list);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Devuelve una gestion por su clave primaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<GestionesController>/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GestionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        [HttpGet("{id}")]
        public async Task<ActionResult<GestionDTO>> GetById(int id)
        {
            try
            {
                var nota = await _service.GetGestion(id);
                if (nota is null)
                {
                    return NotFound("No existe la gestion de facturas solicitada");
                }

                return Ok(nota);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Agrega una nueva gestion para el cobro de facturas adeudadas
        /// </summary>
        /// <param name="gestion"></param>
        /// <returns></returns>
        // POST api/<GestionesController>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(DetalleCompletoGestionDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<GestionDTO>> Post([FromBody] GestionRequest gestion)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Select(z => z.Value.Errors).ToList());
            try
            {
                var creada = await _service.AddGestion(gestion,Usuario.Id);
                return CreatedAtAction("GetById", new { id =creada.Id }, creada);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Modifica una gestion de cobranza especifica
        /// </summary>
        /// <param name="id"></param>
        /// <param name="gestion"></param>
        /// <returns></returns>
        // PUT api/<GestionesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> UpdateGestion(int id, [FromBody] GestionRequest gestion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Select(z => z.Value.Errors).ToList());
            try
            {
                await _service.UpdateGestion(id, gestion);
                return Ok("Modificado exitosamente");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        /// <summary>
        /// Elimina una gestion de cobranza de facturas
        /// </summary>
        /// <param name="id">La clave primaria de la gestion</param>
        /// <returns></returns>
        // DELETE api/<GestionesController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [Authorize]

        public async Task<ActionResult> Delete(int id)
        {
            try
            {

                if (await _service.RemoveGestion(id)) return Ok("Gestion eliminada exitosamente");
                return NoContent();

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

            
        }
    }
}
