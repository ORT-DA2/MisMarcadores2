using System;
using System.Collections.Generic;
using System.Linq;
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
    public class ComentariosController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }
        private IEquiposService _equiposService { get; set; }
        private ISesionesService _sesionesService { get; set; }

        public ComentariosController(IEncuentrosService encuentrosService, ISesionesService sesionesService, IEquiposService equiposService)
        {
            _encuentrosService = encuentrosService;
            _sesionesService = sesionesService;
            _equiposService = equiposService;
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
                IEnumerable<Guid> idEquipos = this._equiposService.ObtenerFavoritosPorUsuario(usuario.NombreUsuario);
                foreach (Guid id in idEquipos)
                {
                    IEnumerable<Encuentro> encuentrosEquipoActual = this._encuentrosService.ObtenerEncuentrosDeEquipo(id).OrderBy(e => e.FechaHora);
                    foreach (Encuentro encuentro in encuentrosEquipoActual)
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