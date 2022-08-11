using APICobranzas.Application.DTOs;
using APICobranzas.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Application.DTOs.Factura;
using APICobranzas.Application.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace APICobranzas.Api.Controllers
{
    [Route("api/facturas")]
    [ApiController]
    public class FacturasController : BaseController
    {
        private readonly IFacturaService _facturaService;
        
        public FacturasController(IFacturaService facturaService)
        {
            _facturaService = facturaService;
        }
        // GET: api/<FacturasController>
        /// <summary>
        /// Obtiene la lista de todas las facturas registradas
        /// </summary>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FacturaItemDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<FacturaItemDTO>>> Get()
        {
            try
            {
                var facturas = await _facturaService.GetFacturas();
                if (!facturas.Any())
                    return NotFound("No se hallan registros");
                return Ok(facturas);
            }
            catch(Exception ex)
            {
                return  Problem(ex.Message);
            }
         
        }

        // GET: api/<FacturasController>
        /// <summary>
        /// Obtiene la lista de todas las facturas emitidas a una obra social, indicando su clave primeria
        /// </summary>
        /// <param name="id">Clave primaria de la obra social</param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FacturaItemDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [HttpGet("obraSocial/{obraSocialId:int}")]
        [Authorize]
        public async Task<ActionResult<List<FacturaItemDTO>>> GetByEstado(int obraSocialId)
        {
            try
            {
                var facturas = await _facturaService.GetFacturasOS(obraSocialId);
                if (!facturas.Any())
                    return NotFound("No se hallan registros");
                return Ok(facturas);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }

        }


        // GET api/<FacturasController>/5
        /// <summary>
        /// Obtiene una factura de acuerdo a su clave primaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna un objeto Factura con sus entidades relacionadas</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<FacturaDTO>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]

        public async Task<ActionResult<FacturaDTO>> GetById(int id)
        {
            try
            {
                var factura = await _facturaService.GetFactura(id);
                if (factura == null)
                    return NotFound("No se ha podido encontrar la factura pedida");
                return Ok(factura);
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
        
        }

        // POST api/<FacturasController>
        /// <summary>
        /// Crea una factura, insertando los datos correspondientes
        /// </summary>
        /// <param name="factura">La factura a crear</param>
        /// <returns></returns>
        ///<response code="201">Creado Satisfactoriamente</response> 
        ///<response code="400">Hay errores de validacion</response> 
        ///<response code="401">La solicitud no esta autenticada</response> 
        ///<response code="500">Error del servidor</response> 
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] FacturaRequestModel factura)
        {
            if (ModelState.IsValid)
            {
                
                var fac = await _facturaService.AddFactura(factura,Usuario.PuntoVentaId);

                return CreatedAtAction("GetById",new {id = fac.Id },fac);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
       
        /// <summary>
        /// Elimina la factura, no recomendado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // DELETE api/<FacturasController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Authorize]
        public async Task<ActionResult<bool>> Delete(int id)
        {
            try
            {
                var remove = await _facturaService.RemoveFactura(id);
                if (remove == true)
                {
                    return Ok("Factura eliminada");
                }
                else
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }
           

        }
        /// <summary>
        /// Anula la factura indicada por su clave promaria
        /// </summary>
        /// <param name="id"></param>
        /// <returns>anulado (true) o no anulado (false)</returns>
        [HttpPatch("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Authorize]

        public async Task<ActionResult<bool>>Anular(int id)
        {
            try
            {
                var fac = await _facturaService.GetFactura(id);
                if (fac != null && fac.ImporteCobrado == 0)
                {
                    var anular = await _facturaService.AnularFactura(id);
                    if (anular == true)
                    {
                        return Ok("Factura anulada");
                    }
                    else
                    {
                        return BadRequest("No se ha podido anular la Factura");
                    }

                }
                else
                {
                    return NotFound("NO PUEDE ANULAR FACTURAS YA COBRADAS");
                }

            }
            catch (Exception ex)
            {
                return Problem(ex.Message);
            }



        }
    }
}
