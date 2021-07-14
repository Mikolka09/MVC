using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Controllers
{
    public class RoutesController : Controller
    {
        public object Index()
        {
            return RouteData
                .Routers
                .OfType<RouteCollection>()
                .FirstOrDefault();
        }
    }
}
