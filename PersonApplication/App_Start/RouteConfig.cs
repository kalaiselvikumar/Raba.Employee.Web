using System.Web.Mvc;
using System.Web.Routing;

namespace PersonApplication
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Person", action = "EmployeeDetails", id = UrlParameter.Optional }
            );
            routes.MapRoute(
        "DisplayDetails",                               // Route name
        "Person/Page/{page}",                           // URL with params
        new { controller = "Person", action = "DisplayDetails" } // Param defaults
    );

        }
    }
}
