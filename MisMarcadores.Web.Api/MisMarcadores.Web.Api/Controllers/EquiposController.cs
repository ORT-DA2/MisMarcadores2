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
    public class EquiposController : Controller
    {
        private IEquiposService _equiposService { get; set; }

        public EquiposController(IEquiposService equiposService)
        {
            _equiposService = equiposService;
        }

        // GET: api/Equipos
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Equipo> equipos = _equiposService.ObtenerEquipos();
            if (equipos == null)
            {
                return NotFound();
            }
            return Ok(equipos);
        }

        // GET: api/Equipos
        [HttpGet("{id}", Name = "GetEquipo")]
        public IActionResult Get(Guid id)
        {
            Equipo equipo = _equiposService.ObtenerEquipoPorId(id);
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        // POST: api/equipos
        public IActionResult Post([FromBody]AgregarEquipo equipoModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                Equipo equipo = equipoModelo.TransformarAEquipo();
                Guid idCreado = this._equiposService.AgregarEquipo(equipo);
                equipo.Id = idCreado;
                return CreatedAtRoute("GetEquipo", new { id = idCreado }, equipo);
            }
            catch (EquipoDataExceptiom)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El nombre del deporte no existe en la BD.");
            }
            catch (ExisteEquipoException)
            {
                return StatusCode(409, "El nombre del equipo ya existe para este deporte en la BD.");
            }
        }

        // PUT: api/Equipos/Rampla
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]ActualizarEquipo equipo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                this._equiposService.ModificarEquipo(id, equipo.TransformarAEquipo());
                return Ok();
            }
            catch (EquipoDataExceptiom)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteEquipoException)
            {
                return BadRequest("El equipo no existe en la BD.");
            }
            catch (ExisteEquipoException)
            {
                return StatusCode(409, "El nuevo nombre del equipo ya existe en la BD.");
            }
        }


        // DELETE: api/Equipos/Rampla
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this._equiposService.BorrarEquipo(id);
                return Ok();
            }
            catch (NoExisteEquipoException)
            {
                return BadRequest("El equipo no existe en la BD.");
            }
            catch (RepositoryException)
            {
                return BadRequest("El equipo no existe en la BD.");
            }
        }
    }
}