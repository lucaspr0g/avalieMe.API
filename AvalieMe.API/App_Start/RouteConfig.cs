using System.Web.Mvc;
using System.Web.Routing;

namespace AvalieMe.API
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{area}/{controller}/{action}/{id}",
            //    defaults: new { area = "Help", controller = "Help", action = "Index", id = UrlParameter.Optional }
            //);

            routes.MapRoute(
                "Help Area",
                "",
                new { controller = "Help", action = "Index" }
            ).DataTokens = new RouteValueDictionary(new { area = "HelpPage" });
        }
    }
}
