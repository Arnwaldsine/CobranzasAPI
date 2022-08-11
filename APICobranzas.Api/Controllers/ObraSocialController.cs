using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.ObraSocial;
using APICobranzas.Application.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICobranzas.Api.Controllers
{
    [Route("api/obrasSociales")]
    [ApiController]
    public class ObraSocialController : BaseController
    {
        private readonly IObraSocialService _obraSocialService;
        private readonly ITipoPrestadorService _tipoService;
        public ObraSocialController(IObraSocialService obraSocialService,ITipoPrestadorService tipoService)
        {
            _obraSocialService = obraSocialService;
            _tipoService = tipoService;
        }
        /// <summary>
        /// Devuelve un listado de las obras sociales
        /// </summary>
        /// <returns></returns>
        // GET: api/<ObraSocialController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<ObraSocialDTO>>> Get()
        {
            try
            {
                var obras = await _obraSocialService.GetObrasSociales();
                if (obras != null)
                {
                    return Ok(obras);
                }
                else
                {
                    return NotFound("No se encuentran Obras Sociales registradas");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
            
        }
        [HttpGet("tipo/{tipoId:int}")]
        [Authorize]
        public async Task<ActionResult<IList<ObraSocialDTO>>> GetByTipo(int tipoId)
        {
            try
            {
                var obras = await _obraSocialService.GetByTipo(tipoId);
                if (obras != null)
                {
                    return Ok(obras);
                }
                else
                {
                    return NotFound("No se encuentran Obras Sociales registradas");
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }
        /// <summary>
        /// Obtiene una obra social por su clave primaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<ObraSocialController>/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ObraSocialDTO>> GetbyId(int id)
        {
            try
            {
                var obra = await _obraSocialService.GetObraSocial(id);
                if (obra != null)
                    return Ok(obra);
                else
                    return NotFound("Obra social no encontrada");
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        }
        /// <summary>
        /// Agrega una obra social
        /// </summary>
        /// <param name="obra">Datos de la obra social</param>
        /// <returns>Obra social creada</returns>
        // POST api/<ObraSocialController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] ObraSocialRequestModel obra)
        {
            if (_tipoService.GetTipoPrestador(obra.TipoPrestadorId) is null)
                return BadRequest("El tipo de prestador insertado no es valido");
            if (!ModelState.IsValid) 
                return BadRequest(ModelState.Select(z => z.Value.Errors).ToList());
            try
            {
                var creada = await _obraSocialService.AddObrasocial(obra);
                return CreatedAtAction("GetById", new {Id = creada.Id}, creada);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);

            }
        }
        /// <summary>
        /// Modifica una obra social
        /// </summary>
        /// <param name="id">Clave primaria de la obra social</param>
        /// <param name="obra">Datos actualizados</param>
        /// <returns></returns>
        // PUT api/<ObraSocialController>/5
        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<ActionResult> Put(int id,[FromBody] ObraSocialRequestModel obra)
        {
            if (await _tipoService.GetTipoPrestador(obra.TipoPrestadorId) is null)
            {
                return BadRequest("El tipo de prestador insertado no es valido");
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                var update = await _obraSocialService.UpdateObraSocial(id, obra);
                if (update)
                {
                    return Ok("Datos actualizados exitosamente");
                }
                else
                    return BadRequest("No se han podido actualizar los datos");
            }
        }
        /// <summary>
        /// Elimina una obra social indicando la clave primaria
        /// </summary>
        /// <param name="id">Clave primaria de la obra social</param>
        /// <returns></returns>
        // DELETE api/<ObraSocialController>/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Delete(int id)
        {
            var delete = await _obraSocialService.RemoveObraSocial(id);
            if (delete)
            {
                return Ok("Obra Social eliminada exitosamente");
            }
            else
            {
                return BadRequest("No se han podido actualizar los datos");
            }
        }
    }
}
