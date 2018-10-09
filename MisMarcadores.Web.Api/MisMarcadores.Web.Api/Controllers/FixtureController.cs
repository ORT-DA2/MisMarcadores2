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
    [Route("api/Fixture")]

    [ServiceFilter(typeof(BaseFilter))]
    [ServiceFilter(typeof(AutenticacionFilter))]
    public class FixtureController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }

        public FixtureController(IEncuentrosService encuentrosService)
        {
            _encuentrosService = encuentrosService;
        }

        // POST: api/Encuentros
        public IActionResult Post([FromBody]AgregarFixture fixtureModelo)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");

            try
            {
                if (this._encuentrosService.FixtureGenerado(fixtureModelo.FechaInicio, fixtureModelo.Deporte, fixtureModelo.Tipo))
                    return Ok("Fixture generado con exito!");
                return StatusCode(409, "El fixture no se pudo armar.");
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

    }
}