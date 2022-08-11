using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICobranzas.Domain.Models;

namespace APICobranzas.Api.Controllers
{
    
    [Controller]
    public abstract class BaseController : ControllerBase
    {
        public Usuario Usuario => (Usuario)HttpContext.Items["Account"];
    }
}
