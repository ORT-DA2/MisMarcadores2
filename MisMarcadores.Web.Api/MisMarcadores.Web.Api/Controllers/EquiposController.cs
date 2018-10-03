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
        [HttpGet("{nombreEquipo}", Name = "GetEquipo")]
        public IActionResult Get(string nombreEquipo)
        {
            Equipo equipo = _equiposService.ObtenerEquipoPorNombre(nombreEquipo);
            if (equipo == null)
            {
                return NotFound();
            }
            return Ok(equipo);
        }

        // POST: api/equipos
        public IActionResult Post([FromBody]AgregarEquipo equipo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                this._equiposService.AgregarEquipo(equipo.TransformarAEquipo());
                return CreatedAtRoute("GetEquipo", new { nombreEquipo = equipo.Nombre }, equipo);
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
        [HttpPut("{nombreEquipo}")]
        public IActionResult Put(string nombreEquipo, [FromBody]ActualizarEquipo equipo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                this._equiposService.ModificarEquipo(nombreEquipo, equipo.TransformarAEquipo());
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
        [HttpDelete("{nombreEquipo}")]
        public IActionResult Delete(string nombreEquipo)
        {
            try
            {
                this._equiposService.BorrarEquipo(nombreEquipo);
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