using System.Web.Mvc;

namespace LeadManagement.Web.Extensions
{
    public static class HomeUrlHelperExtensions
    {
        public static string HomePageUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Index", "Home");
        }

        public static string AboutUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("About", "Home");
        }

        public static string ContactUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Contact", "Home");
        }
    }
}