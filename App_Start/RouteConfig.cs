using System.Web.Mvc;
using System.Web.Routing;

namespace Jikandesu
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{area}/{controller}/{action}/{a}/{b}/{c}",
                defaults: new
                {
                    area = "Home", controller = "Home", action = "Index",
                    a = UrlParameter.Optional,
                    b = UrlParameter.Optional,
                    c = UrlParameter.Optional
                }
            );
        }
    }
}
