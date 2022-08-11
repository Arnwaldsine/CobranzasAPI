using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Results;
using APICobranzas.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;


namespace APICobranzas.Application.Helpers
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private readonly IList<Rol> _roles;

        public AuthorizeAttribute(params Rol[] roles)
        {
            _roles = roles ?? new Rol[] { };
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var cuenta = (Usuario)context.HttpContext.Items["Account"];
            if (cuenta == null || (_roles.Any() && !_roles.Contains(cuenta.Rol)))
            {
                // not logged in or role not authorized
                context.Result = new JsonResult(new { message = "Sin Autorizacion." }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}
