using System.Threading.Tasks;
using System.Web.Mvc;
using LeadManagement.Service.Contracts;
using LeadManagement.Web.Extensions;
using Microsoft.AspNet.Identity;

namespace LeadManagement.Web.Controllers
{
    [Authorize]
    public class LeadController : Controller
    {
        private readonly ILeadService _leadService;

        public LeadController(ILeadService leadService)
        {
            _leadService = leadService;
        }

        [Route("leads")]
        public async Task<ActionResult> Index()
        {
            var userId = User.Identity.GetUserId();
            var lead = await _leadService.GetLeadForUserAsync(userId);
            return View(lead);
        }

        [Route("process-lead/{leadId:int}")]
        [HttpPost]
        public async Task<ActionResult> ProcessLead(int leadId)
        {
            var userId = User.Identity.GetUserId();
            await _leadService.ProcessLeadAsync(leadId, userId);
            TempData["Flash"] = "Lead processed.";
            return Redirect(Url.LeadUrl());
        }
    }
}