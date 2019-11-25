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

            routes.MapRoute(
                name: "RoleManagement",
                url: "{controller}/{action}",
                defaults: new { controller = "RoleManagement", action = "Index" }
                );

            routes.MapRoute(
                name: "AddRole",
                url: "{controller}/{action}",
                defaults: new { controller = "RoleManagement", action = "AddRole" }
                );

            routes.MapRoute(
                name: "EditRole",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "RoleManagement", action = "EditRole", id = UrlParameter.Optional }
                );

            routes.MapRoute(
                name: "AccessManagement",
                url: "{controller}/{action}",
                defaults: new { controller = "AccessManagement", action = "Index"}
                );

            routes.MapRoute(
                name: "EditAccess",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AccessManagement", action = "EditAccess", id = UrlParameter.Optional }
                );
        }
    }
}
