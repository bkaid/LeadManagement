using System.Web.Mvc;
using System.Web.Routing;
using LeadManagement.Web;
using LeadManagement.Web.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcRouteTester;

namespace LeadManagement.Tests.Web
{
    [TestClass]
    public class AccountUrlTests : TestBase
    {
        private UrlHelper _urlHelper;
        
        [TestInitialize]
        public override void Initialization()
        {
            var routeCollection = new RouteCollection();
            routeCollection.MapAttributeRoutesInAssembly(typeof(RouteConfig).Assembly);
            var context = HttpContextTestHelper.FakeHttpContext();
            _urlHelper = new UrlHelper(new RequestContext(context, new RouteData()), routeCollection);
        }

        [TestMethod]
        public void CanGetRegisterUrl()
        {
            Assert.AreEqual("/register", _urlHelper.RegisterUrl());
        }
    }
}
