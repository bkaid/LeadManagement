using System.Web.Routing;
using LeadManagement.Web;
using LeadManagement.Web.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using MvcRouteTester;

namespace LeadManagement.Tests.Web
{
    [TestClass]
    public class RouteTests : TestBase
    {
        private RouteCollection _routeCollection;

        [TestInitialize]
        public override void Initialization()
        {
            base.Initialization();
            _routeCollection = new RouteCollection();
            _routeCollection.MapAttributeRoutesInAssembly(typeof(RouteConfig).Assembly);
        }
        
        [TestMethod]
        public void VerifyHomeControllerRoutes()
        {
            _routeCollection.ShouldMap("~/").To<HomeController>(x => x.Index());
            _routeCollection.ShouldMap("~/contact").To<HomeController>(x => x.Contact());
            _routeCollection.ShouldMap("~/about").To<HomeController>(x => x.About());
        }

        [TestMethod]
        public void VerifyAccountControllerRoutes()
        {
            _routeCollection.ShouldMap("~/register").To<AccountController>(x => x.Register());
            _routeCollection.ShouldMap("~/login").To<AccountController>(x => x.Login(It.IsAny<string>()));
        }
    }
}
