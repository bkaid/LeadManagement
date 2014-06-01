using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using LeadManagement.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;

namespace LeadManagement.Tests.Web.Controllers
{
    [TestClass]
    public abstract class BaseControllerTests : TestBase
    {
        public TController GetController<TController>(string url = null) where TController : Controller
        {
            var routeCollection = new RouteCollection();
            routeCollection.MapAttributeRoutesInAssembly(typeof(RouteConfig).Assembly);
            var context = HttpContextTestHelper.FakeHttpContext();
            var urlHelper = new UrlHelper(new RequestContext(context, new RouteData()), routeCollection);
            
            var controller = Container.Resolve<TController>();
            var controllerContext = new ControllerContext(urlHelper.RequestContext.HttpContext, new RouteData(), controller);
            var controllerName = controller.GetType().Name;
            controllerContext.RouteData.Values.Add("controller", controllerName.Remove(controllerName.LastIndexOf("Controller", StringComparison.Ordinal)));
            controller.ControllerContext = controllerContext;
            controller.Url = urlHelper;
            return controller;
        }
    }
}
