using APICobranzas.Domain.Models;
using APICobranzas.Infra.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.DTOs.NotaCredito;
using APICobranzas.Application.Helpers;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICobranzas.Api.Controllers
{
    

    [Route("api/notasCredito")]
    [ApiController]
    public class NotasCreditoController : BaseController
    {
       private readonly INotaCreditoService _service;
        public NotasCreditoController(INotaCreditoService service)
        {
            _service = service;
        }
        /// <summary>
        /// Devuelve la lista completa de notas de credito
        /// </summary>
        /// <returns></returns>
        // GET: api/<NotasCreditoController>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<NotaCreditoItemDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<NotaCreditoItemDTO>>> Get()
        {
            try
            {
                var list = await _service.GetNotas();
                if (!list.Any())
                {
                    return NotFound("No hay notas de credito registradas");
                }

                return Ok(list);
            }
            catch(Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Devuelve una nota de credito segun su clave primaria, junto al detalle completo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<NotasCreditoController>/5
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GestionDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpGet("{id}")]
        public async  Task<ActionResult<NotaCreditoDTO>> GetById(int id)
        {
            try
            {
                var nota = await _service.GetNota(id);
                if (nota is null)
                {
                    return NotFound("No existe la nota de credito solicitada");
                }

                return Ok(nota);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Crea una nota de credito, insertando los detalles y devuelve la nota creada, con datos completos
        /// </summary>
        /// <param name="nota"></param>
        /// <returns></returns>
        // POST api/<NotasCreditoController>
        [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(NotaCreditoItemDTO))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<NotaCreditoDTO>> Post([FromBody] NotaCreditoRequest nota)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.Select(z => z.Value.Errors).ToList());
            try
            {
                var creada = await _service.AddNota(nota);
                if (!creada.Item1) return BadRequest("Los valores a acreditar se exceden de los valores de las facturas");
                return CreatedAtAction("GetById", new { id = creada.Item2.Id }, creada);
            }
            catch (Exception exception)
            {
                return Problem(exception.Message);
            }
        }
        /// <summary>
        /// Anula una nota de credito indicando la clave primaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        [HttpPatch("{id}")]
        public async Task<ActionResult> Anular(int id)
        {
            try
            {
                if (await _service.GetNota(id) is null)
                    return NotFound("La nota de credito que intenta anular no existe");
               await  _service.AnularNota(id);
                return Ok("nota de credito anulada");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
       /* // PUT api/<NotasCreditoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<NotasCreditoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }*/
    }
}
