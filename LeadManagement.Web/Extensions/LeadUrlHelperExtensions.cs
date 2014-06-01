using System.Web.Mvc;

namespace LeadManagement.Web.Extensions
{
    public static class LeadUrlHelperExtensions
    {
        public static string LeadUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Index", "Lead");
        }

        public static string ProcessLeadUrl(this UrlHelper urlHelper, int leadId)
        {
            return urlHelper.Action("ProcessLead", "Lead", new { leadId });
        }
    }
}