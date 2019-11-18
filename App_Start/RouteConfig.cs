using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace DebugToolCSharp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}",
                defaults: new { controller = "Login", action = "Index" }
                );

            routes.MapRoute(
                name: "UserManagement",
                url: "{controller}/{action}",
                defaults: new { controller = "UserManagement", action = "Index" }
                );

            routes.MapRoute(
                name: "AddUser",
                url: "{controller}/{action}",
                defaults: new { controller = "UserManagement", action = "AddUser" }
                );
        }
    }
}
