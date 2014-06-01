using System.Web.Mvc;
using System.Web.Routing;

namespace LeadManagement.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.MapMvcAttributeRoutes();
        }
    }
}
