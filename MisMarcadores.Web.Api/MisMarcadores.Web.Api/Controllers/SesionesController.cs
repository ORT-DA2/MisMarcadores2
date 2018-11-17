using System;
using Microsoft.AspNetCore.Mvc;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using MisMarcadores.Web.Api.Filters;
using MisMarcadores.Web.Api.Models;
using MisMarcadores.Log;

namespace MisMarcadores.Web.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class SesionesController : Controller
    {
        private ISesionesService _sesionesService { get; set; }
        private ILogService _logService { get; set; }


        public SesionesController(ISesionesService sesionesService, ILogService logService)
        {
            _sesionesService = sesionesService;
            _logService = logService;
        }

        // GET: api/sesiones/nombreUsuario
        [ServiceFilter(typeof(AutenticacionFilter))]
        [HttpGet("{nombreUsuario}")]
        public IActionResult Get(string nombreUsuario)
        {
            var headers = Request.Headers;
            Guid token = new Guid(headers["tokenSesion"]);
            Sesion sesion = _sesionesService.ObtenerPorToken(token);
            if (sesion != null && sesion.NombreUsuario.Equals(nombreUsuario))
            {
                return Ok();
            }
            return Unauthorized();
        }

        // POST: api/sesiones
        public IActionResult Post([FromBody]Login datosLogin)
        {
            if (!ModelState.IsValid) return BadRequest("Datos invalidos");
            try
            {
                Sesion sesionCreada = _sesionesService.Login(datosLogin.NombreUsuario, datosLogin.Contraseña);
                _logService.InsertarAccion(datosLogin.NombreUsuario);
                return Ok(new { token = sesionCreada.Token });
            }
            catch (NullReferenceException)
            {
                return BadRequest("Usuario o contraseña incorrectos.");
            }
        }

        // DELETE: api/sesiones/5
        [HttpDelete("{token}")]
        public IActionResult Delete(Guid token)
        {
            try
            {
                var headers = Request.Headers;
                string tokenStr = headers["tokenSesion"];
                if (tokenStr.Equals(token.ToString()))
                {
                    _sesionesService.Logout(token);
                    return Ok();
                }
            }
            catch (ArgumentNullException) {
                return Unauthorized();
            }
            return Unauthorized();
        }

    }
}