using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Web.Api.Filters;
using MisMarcadores.Web.Api.Models;
using MisMarcadores.Log;

namespace MisMarcadores.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Fixture")]

    [ServiceFilter(typeof(BaseFilter))]
    [ServiceFilter(typeof(AutenticacionFilter))]
    public class FixtureController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }
        private ILogService _logService { get; set; }
        private ISesionesService _sesionesService { get; set; }

        public FixtureController(IEncuentrosService encuentrosService, ILogService logService, ISesionesService sesionesService)
        {
            _encuentrosService = encuentrosService;
            _logService = logService;
            _sesionesService = sesionesService;
        }

        // POST: api/Fixture
        public IActionResult Post([FromBody]AgregarFixture fixtureModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");

            try
            {
                if (this._encuentrosService.FixtureGenerado(fixtureModelo.FechaInicio, fixtureModelo.Deporte, fixtureModelo.Tipo))
                {
                    var re = Request;
                    var headers = re.Headers;
                    string tokenStr = headers["tokenSesion"];
                    Guid token = new Guid(tokenStr);
                    Usuario usuario = _sesionesService.ObtenerUsuarioPorToken(token);
                    _logService.InsertarAccion(usuario.NombreUsuario, "Fixture");
                    return Ok("Fixture generado con exito!");
                }
                return StatusCode(409, "El fixture no se pudo armar, existieron conflictos al generar los encuentros para uno o mas de los equipos participantes.");
            }
            catch (EncuentroDataException)
            {
                return BadRequest("Datos invalidos");
            }
            catch (NoExisteDeporteException)
            {
                return BadRequest("El nombre del deporte no existe en la BD.");
            }
            catch (NoExistenParticipantesException)
            {
                return BadRequest("Para armar un fixture en el deporte tienen que existir como minimo dos equipos");
            }
            catch (FixtureGruposDataException)
            {
                return StatusCode(409, "Para armar fixtures por fase de grupos la cantidad de equipos en el deporte debe ser multiplo de 4.");
            }
            catch (ExisteEncuentroEnFechaException)
            {
                return StatusCode(409, "Ya existe un encuentro en esa fecha para el/los equipos seleccionados.");
            }
            catch (TipoDeFixtureIncompatibleException)
            {
                return StatusCode(409, "El tipo de deporte no es compatible con el fixture deseado");
            }
            catch (ExisteEncuentroMismoDiaException)
            {
                return StatusCode(409, "Algun participante ya tiene un encuetro para las fechas fijadas");
            }
            

        }

    }
}