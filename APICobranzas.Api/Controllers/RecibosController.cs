using APICobranzas.Application.DTOs.Recibo;
using APICobranzas.Application.Interfaces;
using APICobranzas.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs;
using APICobranzas.Application.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICobranzas.Api.Controllers
{
    [Route("api/recibos")]
    [ApiController]
    public class RecibosController : BaseController
    {
        private readonly IReciboService _reciboService;
        public RecibosController(IReciboService reciboService)
        {
            _reciboService = reciboService;
        }
        /// <summary>
        /// Obtiene la lista completa de recibos emitidos
        /// </summary>
        /// <returns></returns>
        // GET: api/<RecibosController>
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<ReciboDTO>>> Get()
        {
            
            var recibos = await _reciboService.GetRecibos();
            if (recibos == null)
                return NotFound("No se hallan registros de recibos");
            else
                return Ok(recibos);
        }
        /// <summary>
        /// Obtiene un recibo por su clave primaria, junto a las facturas asociadas
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET api/<RecibosController>/5
        [Authorize]
        [HttpGet("{id}")]

        public async Task<ActionResult<ReciboDetalleDTO>> GetById(int id)
        {
            var recibo = await _reciboService.GetRecibo(id);
            if (recibo == null)
                return NotFound("No se ha podido encontrar la factura pedida");
            return Ok(recibo);
        }
        [HttpGet("{nro}")]
        public async Task<ActionResult<ReciboDTO>> GetByNro(string nro)
        {
            var recibo = await _reciboService.GetReciboNro(nro);
            if (recibo == null)
                return NotFound("No se ha podido encontrar el recibo");
            return recibo;
        }
        /// <summary>
        /// Crea un recibo, incluyendo el detalle de facturas cobradas
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns></returns>
        // POST api/<RecibosController>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<ReciboDetalleDTO>> Post([FromBody] ReciboRequestModel recibo)
        {

            if (!ModelState.IsValid) return BadRequest("Hay errores de validacion. Revise sus datos");

            
            Tuple<bool, ReciboDetalleDTO> creado;
            try
            {
                creado = await _reciboService.AddRecibo(recibo, Usuario.PuntoVentaId);
                if (!creado.Item1)
                {
                    return BadRequest("Existen montos que exceden los montos a cobrar de las facturas");
                }
                return CreatedAtAction("getById", new { Id = creado.Item2.Id }, creado.Item2);

            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }

            

        }
        /// <summary>
        /// modifica un recibo
        /// </summary>
        /// <param name="recibo"></param>
        /// <returns></returns>
        // PUT api/<RecibosController>/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult> Put([FromBody] ReciboRequestModel recibo)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _reciboService.UpdateRecibo(recibo);
                    return Ok("Recibo modificado exitosamente");
                }
                catch (Exception ex)
                {
                    return Problem("Error del servidor. Intente nuevamente", ex.Message);
                }
                
            }
            else
            {
                return BadRequest("Hay problemas de validación. Revise sus datos");
            }
        }
        /// <summary>
        /// Elimina un recibo
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<RecibosController>/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var recibo = _reciboService.GetRecibo(id);
            if(recibo != null)
            {
                var deleted = await _reciboService.RemoveRecibo(id);
                if (deleted)
                {
                    return Ok("Recibo eliminado satisfactoriamente");
                }
                else
                {
                    return BadRequest("No se ha podido eliminar el recibo seleccionado");
                }
            }
            else
            {
                return BadRequest("El recibo pedido no existe");
            }
        }
    }
}
