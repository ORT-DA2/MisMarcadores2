using Microsoft.AspNetCore.Mvc;
using MisMarcadores.Web.Api.Filters;
using MisMarcadores.Log;
using System.Collections.Generic;
using MisMarcadores.Data.Entities;
using System;

namespace MisMarcadores.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/Log")]

    [ServiceFilter(typeof(BaseFilter))]
    [ServiceFilter(typeof(AutenticacionFilter))]
    public class LogController : Controller
    {
        private MisMarcadores.Log.ILogService _logService { get; set; }

        public LogController(MisMarcadores.Log.ILogService logService)
        {
            _logService = logService;
        }



 
        // GET: api/Log
        [HttpGet]
        public IActionResult Get([FromBody] FilterDateTime filter)
        {
            IEnumerable<MisMarcadores.Data.Entities.Log> datos = _logService.ObtenerLogEntreFechas(filter);
            if (datos == null)
            {
                return NotFound();
            }
            return Ok(datos);
        }
    }

  
}