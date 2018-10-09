using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Web.Api.Filters;
using MisMarcadores.Web.Api.Models;

namespace MisMarcadores.Web.Api
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    [ServiceFilter(typeof(BaseFilter))]
    public class DeportesController : Controller
    {
        private IDeportesService _deportesService { get; set; }

        public DeportesController(IDeportesService deportesService)
        {
            _deportesService = deportesService;
        }

        // GET: api/Deportes
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Deporte> deportes = _deportesService.ObtenerDeportes();
            if (deportes == null)
            {
                return NotFound();
            }
            return Ok(deportes);
        }

        // GET: api/Deportes
        [HttpGet("{nombreDeporte}", Name = "GetDeporte")]
        public IActionResult Get(string nombreDeporte)
        {
            Deporte deporte = _deportesService.ObtenerDeportePorNombre(nombreDeporte);
            if (deporte == null)
            {
                return NotFound();
            }
            return Ok(deporte);
        }

        // POST: api/deportes
        [ServiceFilter(typeof(AutenticacionFilter))]
        public IActionResult Post([FromBody]AgregarDeporte deporte)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                this._deportesService.AgregarDeporte(deporte.TransformarADeporte());
                return CreatedAtRoute("GetDeporte", new { nombreDeporte = deporte.Nombre }, deporte);
            }
            catch (DeporteDataException)
            {
                return BadRequest("Datos invalidos");
            }
            catch (ExisteDeporteException)
            {
                return StatusCode(409, "El nombre del deporte ya existe en la BD.");
            }
        }

        // PUT: api/Deportes/Futbol
        [HttpPut("{nombreDeporte}")]
        [ServiceFilter(typeof(AutenticacionFilter))]
        public IActionResult Put(string nombreDeporte, [FromBody]ActualizarDeporte deporte)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                this._deportesService.ModificarDeporte(nombreDeporte, deporte.TransformarADeporte());
                return Ok();
            }
            catch (DeporteDataException)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El deporte no existe en la BD.");
            }
            catch (ExisteDeporteException)
            {
                return StatusCode(409, "El nuevo nombre del deporte ya existe en la BD.");
            }
        }

        // DELETE: api/Deportes/Futbol
        [HttpDelete("{nombreDeporte}")]
        [ServiceFilter(typeof(AutenticacionFilter))]
        public IActionResult Delete(string nombreDeporte)
        {
            try
            {
                this._deportesService.BorrarDeporte(nombreDeporte);
                return Ok();
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El deporte no existe en la BD.");
            }
        }
    }
}