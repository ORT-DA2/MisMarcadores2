using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using System;

namespace MisMarcadores.Web.Api.Filters
{
    public class AutenticacionFilter : IActionFilter
    {
        private ISesionesService _sesionesService { get; set; }

        public AutenticacionFilter() { }

        public AutenticacionFilter(ISesionesService sesionesService)
        {
            _sesionesService = sesionesService;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            try
            {
                var request = actionContext.HttpContext.Request;
                Guid token = ObtenerTokenHeader(request);
                Sesion sesion = _sesionesService.ObtenerPorToken(token);
                if (sesion == null)
                {
                    actionContext.Result = new ContentResult()
                    {
                        Content = "Unauthorized",
                        StatusCode = 401
                    };
                }
                else
                {
                    Usuario usuario = _sesionesService.ObtenerUsuarioPorToken(token);
                    if (!usuario.Administrador) {
                        actionContext.Result = new ContentResult()
                        {
                            Content = "Forbidden",
                            StatusCode = 403
                        };
                    }
                }
                
            }
            catch (ArgumentNullException)
            {
                actionContext.Result = new ContentResult()
                {
                    Content = "Debe incluir un token en el header",
                    StatusCode = 400
                };
            }
            catch (FormatException)
            {
                actionContext.Result = new ContentResult()
                {
                    Content = "El token no es correcto",
                    StatusCode = 400
                };
            }
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {

        }

        private Guid ObtenerTokenHeader(HttpRequest request)
        {
            var headers = request.Headers;
            string tokenStr = headers["tokenSesion"];
            return new Guid(tokenStr);
        }
    }
}
