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
    public class CalendarioController : Controller
    {
        private IEncuentrosService _encuentrosService { get; set; }

        public CalendarioController(IEncuentrosService encuentrosService)
        {
            _encuentrosService = encuentrosService;
        }

        // GET: api/Calendario
        [HttpGet]
        public IActionResult Get([FromQuery]Calendario calendario)
        {
            IEnumerable<Encuentro> encuentros = _encuentrosService.ObtenerEncuentros();
            if (encuentros == null)
            {
                return NotFound();
            }
            int mes = calendario.Mes;
            int año = calendario.Año;
            if (mes < 0 || año < 0)
            {
                return BadRequest("Ingrese fechas validas.");
            }
            if (mes >=1 && mes <=12 )
            {
                if (año != 0)
                    encuentros = encuentros.Where(e => e.FechaHora.Month.Equals(mes) && e.FechaHora.Year == año);
                encuentros = encuentros.Where(e => e.FechaHora.Month == mes);
            }
            if (año > 1)
            {
                encuentros = encuentros.Where(e => e.FechaHora.Year == año);
            }
            return Ok(encuentros);
        }
    }
}