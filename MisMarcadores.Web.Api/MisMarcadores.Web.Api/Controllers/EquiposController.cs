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

    public class EquiposController : Controller
    {
        private IEquiposService _equiposService { get; set; }
        private ISesionesService _sesionesService { get; set; }

        public EquiposController(IEquiposService equiposService, ISesionesService sesionesService)
        {
            _equiposService = equiposService;
            _sesionesService = sesionesService;
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
        [ServiceFilter(typeof(AutenticacionFilter))]
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
        [ServiceFilter(typeof(AutenticacionFilter))]
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
        [ServiceFilter(typeof(AutenticacionFilter))]
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
        [ServiceFilter(typeof(AutenticacionFilter))]
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

        // POST: api/Equipos/{idEquipo}/follow
        [HttpPost("{idEquipo}/follow")]
        public IActionResult PostFavorito(Guid idEquipo)
        {
            var headers = Request.Headers;
            Guid token = new Guid(headers["tokenSesion"]);
            Usuario usuario = _sesionesService.ObtenerUsuarioPorToken(token);
            if (usuario == null)
            {
                return BadRequest();
            }
            try
            {
                this._equiposService.AgregarFavorito(idEquipo, usuario.NombreUsuario);
                return Ok();
            }
            catch (NoExisteEquipoException)
            {
                return BadRequest("El equipo no existe en la BD.");
            }
            catch (ExisteFavoritoException)
            {
                return BadRequest("El usuario ya sigue a dicho equipo.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/Equipos/{idEquipo}/unfollow
        [HttpDelete("{idEquipo}/unfollow")]
        public IActionResult DeleteFavorito(Guid idEquipo)
        {
            var headers = Request.Headers;
            Guid token = new Guid(headers["tokenSesion"]);
            Usuario usuario = _sesionesService.ObtenerUsuarioPorToken(token);
            if (usuario == null)
            {
                return BadRequest();
            }
            try
            {
                this._equiposService.BorrarFavorito(idEquipo, usuario.NombreUsuario);
                return Ok();
            }
            catch (NoExisteEquipoException)
            {
                return BadRequest("El equipo no existe en la BD.");
            }
            catch (NoExisteFavoritoException)
            {
                return BadRequest("El usuario no sigue a dicho equipo.");
            }
            catch (RepositoryException)
            {
                return BadRequest();
            }
        }
    }
}