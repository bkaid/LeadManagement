using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LeadManagement.Data;
using LeadManagement.Service.Mapping;

namespace LeadManagement.Web
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            SampleData.Initialize();
            AutoMapperConfig.RegisterMappings();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
