using System.Web.Mvc;

namespace LeadManagement.Web.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        public ActionResult Index()
        {
            return View();
        }

        [Route("about")]
        public ActionResult About()
        {
            return View();
        }

        [Route("contact")]
        public ActionResult Contact()
        {
            return View();
        }
    }
}