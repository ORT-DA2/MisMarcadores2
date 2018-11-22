using System;
using System.Collections.Generic;
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
    public class EncuentrosController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }
        private ISesionesService _sesionesService { get; set; }
        private IParticipantesService _participantesService { get; set; }

        public EncuentrosController(IEncuentrosService encuentrosService, ISesionesService sesionesService, IParticipantesService participantesService)
        {
            _encuentrosService = encuentrosService;
            _sesionesService = sesionesService;
            _participantesService = participantesService;
        }

        // GET: api/Encuentros
        [HttpGet]
        public IActionResult Get()
        {
            IEnumerable<Encuentro> encuentros = _encuentrosService.ObtenerEncuentros();
            List<MostrarEncuentro> enceuntromodel = new List<MostrarEncuentro>();
            foreach (Encuentro item in encuentros)
            {
                enceuntromodel.Add(new MostrarEncuentro(item));
            }
            if (encuentros == null)
            {
                return NotFound();
            }
            return Ok(enceuntromodel);
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
            MostrarEncuentro retorno = new MostrarEncuentro(encuentro);

            return Ok(retorno);
        }

        // GET: api/Encuentros/deporte/futbol
        [HttpGet("deporte/{nombre}", Name = "GetEncuentrosPorDeporte")]
        public IActionResult GetEncuentrosPorDeporte(String nombre)
        {
            IEnumerable<Encuentro> encuentros = _encuentrosService.ObtenerEncuentrosPorDeporte(nombre);
            if (encuentros == null)
            {
                return NotFound();
            }
            List<Encuentro> encuentrosActualizados = new List<Encuentro>();
            foreach (Encuentro encuentro in encuentros)
            {
                Encuentro e = encuentro;
                foreach (var pe in e.ParticipanteEncuentro)
                {
                    pe.Participante = _participantesService.ObtenerParticipantePorId(pe.ParticipanteId);
                }
                encuentrosActualizados.Add(e);
            }
            List<MostrarEncuentro> enceuntromodel = new List<MostrarEncuentro>();
            foreach (Encuentro item in encuentrosActualizados)
            {
                enceuntromodel.Add(new MostrarEncuentro(item));
            }
            return Ok(enceuntromodel);
        }

        // GET: api/Encuentros/equipo/idequipo
        [HttpGet("equipo/{id}", Name = "GetEncuentrosPorParticipante")]
        public IActionResult GetEncuentrosPorParticipante(Guid id)
        {
            IEnumerable<Encuentro> encuentros = _encuentrosService.ObtenerEncuentrosPorParticipante(id);
            if (encuentros == null)
            {
                return NotFound();
            }
            List<Encuentro> encuentrosActualizados = new List<Encuentro>();
            foreach (Encuentro encuentro in encuentros)
            {
                Encuentro e = encuentro;
                foreach (var pe in e.ParticipanteEncuentro)
                {
                    pe.Participante = _participantesService.ObtenerParticipantePorId(pe.ParticipanteId);
                }
                encuentrosActualizados.Add(e);
            }
            List<MostrarEncuentro> enceuntromodel = new List<MostrarEncuentro>();
            foreach (Encuentro item in encuentrosActualizados)
            {
                enceuntromodel.Add(new MostrarEncuentro(item));
            }
            return Ok(enceuntromodel);
        }

        // POST: api/Encuentros
        [ServiceFilter(typeof(AutenticacionFilter))]
        public IActionResult Post([FromBody]AgregarEncuentro encuentroModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");

            try
            {
                Encuentro encuentro = encuentroModelo.TransformarAEncuentro();
                Guid idCreado = this._encuentrosService.AgregarEncuentro(encuentro);
                encuentro.Id = idCreado;
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
            catch (NoExisteParticipanteException)
            {
                return BadRequest("El/los equipos no existen en la BD.");
            }
            catch (ExisteEncuentroEnFechaException)
            {
                return StatusCode(409, "Ya existe un encuentro en esa fecha para el/los equipos seleccionados.");
            }
            catch (CantidadIncorrectaDePartcipantesException)
            {
                return BadRequest("Se ingreso una cantidad incorrecta de participantes para el encuentro");
            }
            catch (NoCoincideDeporteException)
            {
                return BadRequest("El deporte del encuentro no coincide con el de los participantes");
            }
            catch (ParticipantesRepetidoException)
            {
                return BadRequest("Se ha ingresado un participante duplicado");
            }
            catch (ExisteEncuentroMismoDiaException)
            {
                return BadRequest("Algun participante tiene un encuentro ya fijado para la fecha del encuentro actual");
            }
            catch (ResultadoIncorrectoException)
            {
                return BadRequest("Los resultados ingresados no son correctos");
            }
        }

        // POST: api/Encuentros/{idEncuentro}/comentario
        [HttpPost("{idEncuentro}/comentario")]
        public IActionResult PostComentario(Guid idEncuentro, [FromBody]AgregarComentario comentarioModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            var headers = Request.Headers;
            Guid token = new Guid(headers["tokenSesion"]);
            Usuario usuario = _sesionesService.ObtenerUsuarioPorToken(token);
            if (usuario == null)
            {
                return BadRequest();
            }
            try
            {
                this._encuentrosService.AgregarComentario(idEncuentro, usuario.NombreUsuario, comentarioModelo.Comentario);
                return Ok();
            }
            catch (Exception)
            {
                return StatusCode(409, "Ya existe un encuentro en esa fecha para el/los equipos seleccionados.");
            }
        }

        // PUT: api/Encuentros/id
        [ServiceFilter(typeof(AutenticacionFilter))]
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
            catch (NoExisteParticipanteException)
            {
                return BadRequest("El/los equipos no existen en la BD.");
            }
            catch (ExisteEncuentroEnFechaException)
            {
                return StatusCode(409, "Ya existe un encuentro en esa fecha para el/los equipos seleccionados.");
            }
            catch (ExisteEncuentroMismoDiaException)
            {
                return BadRequest("Algun participante tiene un encuentro ya fijado para la fecha del encuentro actual");
            }
        }

        // DELETE: api/Encuentros/id
        [ServiceFilter(typeof(AutenticacionFilter))]
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
        }

        [ServiceFilter(typeof(AutenticacionFilter))]
        [HttpDelete]
        public IActionResult Delete()
        {
            this._encuentrosService.BorrarTodos();
            return Ok();
        }
    }
}