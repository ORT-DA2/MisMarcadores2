using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Repository.Exceptions;
using MisMarcadores.Web.Api.Filters;
using MisMarcadores.Web.Api.Models;

namespace MisMarcadores.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    [ServiceFilter(typeof(AutenticacionFilter))]
    public class EncuentrosController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }

        public EncuentrosController(IEncuentrosService encuentrosService)
        {
            _encuentrosService = encuentrosService;
        }

        // GET: api/Encuentros
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Encuentro> encuentros = _encuentrosService.ObtenerEncuentros();
            if (encuentros == null)
            {
                return NotFound();
            }
            return Ok(encuentros);
        }

        // GET: api/Encuentros
        [HttpGet("{id}", Name = "GetEncuentro")]
        public IActionResult Get(Guid id)
        {
            Encuentro encuentro = _encuentrosService.ObtenerEncuentroPorId(id);
            if (encuentro == null)
            {
                return NotFound();
            }
            return Ok(encuentro);
        }

        // POST: api/Encuentros
        public IActionResult Post([FromBody]AgregarEncuentro encuentroModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");

            try
            {
                Encuentro encuentro = encuentroModelo.TransformarAEncuentro();
                Guid idCreado = this._encuentrosService.AgregarEncuentro(encuentro);
                encuentro.Id = idCreado;
                return CreatedAtRoute("GetEncuentro", new { id = idCreado }, encuentro);
            }
            catch (EncuentroDataException)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El nombre del deporte no existe en la BD.");
            }
            catch (NoExisteEquipoException)
            {
                return BadRequest("El/los equipos no existen en la BD.");
            }
            catch (ExisteEncuentroEnFecha) {
                return StatusCode(409, "Ya existe un encuentro en esa fecha para el/los equipos seleccionados.");
            }
        }

        // PUT: api/Encuentros/id
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]ActualizarEncuentro encuentro)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");

            try
            {
                this._encuentrosService.ModificarEncuentro(id, encuentro.TransformarAEncuentro());
                return Ok();
            }
            catch (EncuentroDataException)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El nombre del deporte no existe en la BD.");
            }
            catch (NoExisteEquipoException)
            {
                return BadRequest("El/los equipos no existen en la BD.");
            }
            catch (ExisteEncuentroEnFecha)
            {
                return StatusCode(409, "Ya existe un encuentro en esa fecha para el/los equipos seleccionados.");
            }
        }

        // DELETE: api/Encuentros/id
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this._encuentrosService.BorrarEncuentro(id);
                return Ok();
            }
            catch (NoExisteEncuentroException)
            {
                return BadRequest("El encuentro no existe en la BD.");
            }
            catch (RepositoryException)
            {
                return BadRequest("Error en la BD.");
            }
        }

        [HttpDelete]
        public IActionResult Delete()
        {
            try
            {
                this._encuentrosService.BorrarTodos();
                return Ok();
            }
            catch (RepositoryException)
            {
                return BadRequest("Error en la BD.");
            }
        }
    }
}