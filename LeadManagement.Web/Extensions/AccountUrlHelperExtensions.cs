using System.Web.Mvc;
using LeadManagement.Web.Controllers;

namespace LeadManagement.Web.Extensions
{
    public static class AccountUrlHelperExtensions
    {
        public static string RegisterUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Register", "Account");
        }

        public static string LoginUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("Login", "Account");
        }

        public static string LogoffUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("LogOff", "Account");
        }

        public static string ProfileUrl(this UrlHelper urlHelper, AccountController.ManageMessageId? message = null)
        {
            return urlHelper.Action("Manage", "Account", new { message });
        }

        public static string ForgotPasswordUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("ForgotPassword", "Account");
        }

        public static string ConfirmEmailUrl(this UrlHelper urlHelper, string userId, string code)
        {
            return urlHelper.Action("ConfirmEmail", "Account", new { userId, code }, urlHelper.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string ResetPasswordUrl(this UrlHelper urlHelper, string userId, string code)
        {
            return urlHelper.Action("ResetPassword", "Account", new { userId, code }, urlHelper.RequestContext.HttpContext.Request.Url.Scheme);
        }

        public static string ForgotPasswordConfirmationUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("ForgotPasswordConfirmation", "Account");
        }

        public static string ResetPasswordConfirmationUrl(this UrlHelper urlHelper)
        {
            return urlHelper.Action("ResetPasswordConfirmation", "Account");
        }
    }
}
