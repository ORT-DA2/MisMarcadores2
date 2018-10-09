using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using MisMarcadores.Data.Entities;
using MisMarcadores.Logic;
using System;
using System.Data.SqlClient;

namespace MisMarcadores.Web.Api.Filters
{
    public class BaseFilter : IActionFilter
    {
        private ISesionesService _sesionesService { get; set; }

        public BaseFilter() { }

        public BaseFilter(ISesionesService sesionesService)
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
            }
            catch (SqlException)
            {
                actionContext.Result = new ContentResult()
                {
                    Content = "ERROR 500: El servidor esta caido o innaccesible en este momento.",
                    StatusCode = 500
                };
            }
            catch (DbUpdateException)
            {
                actionContext.Result = new ContentResult()
                {
                    Content = "Hubo un problema al intentar guardar en la base de datos.",
                    StatusCode = 500
                };
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
