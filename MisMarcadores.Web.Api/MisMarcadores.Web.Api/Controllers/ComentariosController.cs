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
    public class ComentariosController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }
        private IParticipantesService _participantesService { get; set; }
        private ISesionesService _sesionesService { get; set; }

        public ComentariosController(IEncuentrosService encuentrosService, ISesionesService sesionesService, IParticipantesService participantesService)
        {
            _encuentrosService = encuentrosService;
            _sesionesService = sesionesService;
            _participantesService = participantesService;
        }

        // GET: api/comentarios
        [HttpGet]
        public IActionResult Get()
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
                List<Comentario> comentarios = new List<Comentario>();
                IEnumerable<Guid> idParticipantes = this._participantesService.ObtenerFavoritosPorUsuario(usuario.NombreUsuario);
                foreach (Guid id in idParticipantes)
                {
                    IEnumerable<Encuentro> encuentrosParticipanteActual = this._encuentrosService.ObtenerEncuentrosDeParticipante(id).OrderBy(e => e.FechaHora);
                    foreach (Encuentro encuentro in encuentrosParticipanteActual)
                    {
                        if (!comentarios.Any(c => c.IdEncuentro == encuentro.Id))
                        {
                            List<Comentario> comentariosEncuentro = this._encuentrosService.ObtenerComentarios(encuentro.Id);
                            foreach (Comentario comentario in comentariosEncuentro)
                            {
                                comentarios.Add(comentario);
                            }
                        }
                        
                    }
                }
                return Ok(comentarios);
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