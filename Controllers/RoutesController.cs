using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    [Route("CustomRoutes")]
    public class RoutesController : Controller
    {
        [Route("~/CustomIndex")]
        public object Index()
        {
            //return RouteData
            //    .Routers
            //    .OfType<RouteCollection>()
            //    .FirstOrDefault();
            return "Routes";
        }
    }
}
