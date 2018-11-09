using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Web.Api.Filters;
using MisMarcadores.Web.Api.Models;

namespace MisMarcadores.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]

    [ServiceFilter(typeof(BaseFilter))]
    public class ParticipantesController : Controller
    {
        private IParticipantesService _participantesService { get; set; }
        private ISesionesService _sesionesService { get; set; }

        public ParticipantesController(IParticipantesService participantesService, ISesionesService sesionesService)
        {
            _participantesService = participantesService;
            _sesionesService = sesionesService;
        }

        // GET: api/Participantes
        [HttpGet]
        public IActionResult Get([FromQuery]FiltroOrden filtroOrden)
        {
            IEnumerable<Participante> participantes = _participantesService.ObtenerParticipantes();
            if (participantes == null)
            {
                return NotFound();
            }
            string filtro = filtroOrden.Filtro;
            string orden = filtroOrden.Orden;
            if (!EsValido(filtro) && !EsValido(orden))
                return Ok(participantes);
            if (!EsValido(filtro)) {
                filtro = "";
            }
            else
            {
                participantes = participantes.Where(e => e.Nombre.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >=0);
            }
            if (EsValido(orden)) { 
                if (orden.ToUpper() != "ASC" && orden.ToUpper() != "DESC") {
                    return BadRequest("El valor del orden debe ser ASC o DESC");
                }
                else {
                    if (orden.ToUpper() == "ASC") {
                        participantes = participantes.OrderBy(e => e.Nombre);
                    }
                    else {
                        participantes = participantes.OrderByDescending(e => e.Nombre);
                    }
                }
            }
            return Ok(participantes);
        }

        private bool EsValido(string campo)
        {
            return !string.IsNullOrEmpty(campo);
        }

        // GET: api/Participantes
        [HttpGet("{id}", Name = "GetParticipante")]
        public IActionResult Get(Guid id)
        {
            Participante participante = _participantesService.ObtenerParticipantePorId(id);
            if (participante == null)
            {
                return NotFound();
            }
            return Ok(participante);
        }

        // POST: api/participantes
        [ServiceFilter(typeof(AutenticacionFilter))]
        public IActionResult Post([FromBody]AgregarParticipante participanteModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                Participante participante = participanteModelo.TransformarAParticipante();
                Guid idCreado = this._participantesService.AgregarParticipante(participante);
                participante.Id = idCreado;
                return CreatedAtRoute("GetParticipante", new { id = idCreado }, participante);
            }
            catch (FormatException)
            {
                return BadRequest("La imagen debe tener un formato de base 64.");
            }
            catch (ParticipanteDataExceptiom)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El nombre del deporte no existe en la BD.");
            }
            catch (ExisteParticipanteException)
            {
                return StatusCode(409, "El nombre del participante ya existe para este deporte en la BD.");
            }
        }

        // PUT: api/Participantes/Rampla
        [ServiceFilter(typeof(AutenticacionFilter))]
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody]ActualizarParticipante participante)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                this._participantesService.ModificarParticipante(id, participante.TransformarAParticipante());
                return Ok();
            }
            catch (FormatException)
            {
                return BadRequest("La imagen debe tener un formato de base 64.");
            }
            catch (ParticipanteDataExceptiom)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteParticipanteException)
            {
                return BadRequest("El participante no existe en la BD.");
            }
            catch (ExisteParticipanteException)
            {
                return StatusCode(409, "El nuevo nombre del participante ya existe en la BD.");
            }
        }


        // DELETE: api/Participantes/Rampla
        [ServiceFilter(typeof(AutenticacionFilter))]
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            try
            {
                this._participantesService.BorrarParticipante(id);
                return Ok();
            }
            catch (NoExisteParticipanteException)
            {
                return BadRequest("El participante no existe en la BD.");
            }
        }

        // POST: api/Participantes/{idParticipante}/follow
        [HttpPost("{idParticipante}/follow")]
        public IActionResult PostFavorito(Guid idParticipante)
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
                this._participantesService.AgregarFavorito(idParticipante, usuario.NombreUsuario);
                return Ok();
            }
            catch (NoExisteParticipanteException)
            {
                return BadRequest("El participante no existe en la BD.");
            }
            catch (ExisteFavoritoException)
            {
                return BadRequest("El usuario ya sigue a dicho participante.");
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST: api/Participantes/{idParticipante}/unfollow
        [HttpDelete("{idParticipante}/unfollow")]
        public IActionResult DeleteFavorito(Guid idParticipante)
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
                this._participantesService.BorrarFavorito(idParticipante, usuario.NombreUsuario);
                return Ok();
            }
            catch (NoExisteParticipanteException)
            {
                return BadRequest("El participante no existe en la BD.");
            }
            catch (NoExisteFavoritoException)
            {
                return BadRequest("El usuario no sigue a dicho participante.");
            }
        }
    }
}