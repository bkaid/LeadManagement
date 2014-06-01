using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using LeadManagement.Model.Domain;
using LeadManagement.Model.ViewModels;
using LeadManagement.Service.Contracts;
using LeadManagement.Web.Extensions;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;

namespace LeadManagement.Web.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [Route("login")]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindAsync(model.Email, model.Password);
                if (user != null)
                {
                    await SignInAsync(user, model.RememberMe);
                    return RedirectToLocal(returnUrl);
                }

                ModelState.AddModelError("", "Invalid username or password.");
            }

            return View(model);
        }

        [AllowAnonymous]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User { UserName = model.Email, Email = model.Email };
                var result = await _userService.CreateAsync(user, model.Password);
                if (result.Success)
                {
                    await SignInAsync(user, false);

                    var code = await _userService.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.ConfirmEmailUrl(user.Id, code);
                    await _userService.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return Redirect(Url.HomePageUrl());
                }

                AddErrors(result);
            }

            return View(model);
        }

        [Route("confirm")]
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            var result = await _userService.ConfirmEmailAsync(userId, code);
            if (result.Success)
                return View("ConfirmEmail");

            AddErrors(result);
            return View();
        }

        [AllowAnonymous]
        [Route("forgot-password")]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [Route("forgot-password")]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "The user either does not exist or is not confirmed.");
                    return View();
                }

                var code = await _userService.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.ResetPasswordUrl(user.Id, code);
                await _userService.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                return Redirect(Url.ForgotPasswordConfirmationUrl());
            }

            return View(model);
        }

        [Route("forgot-password-confirmation")]
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        [Route("reset-password")]
        public ActionResult ResetPassword(string code)
        {
            if (code == null)
            {
                return View("Error");
            }
            return View();
        }

        [Route("reset-password")]
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.FindByNameAsync(model.Email);
                if (user == null)
                {
                    ModelState.AddModelError("", "No user found.");
                    return View();
                }

                var result = await _userService.ResetPasswordAsync(user.Id, model.Code, model.Password);
                if (result.Success)
                    return Redirect(Url.ResetPasswordConfirmationUrl());

                AddErrors(result);
                return View();
            }

            return View(model);
        }

        [Route("reset-password-comfirmation")]
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [Route("user")]
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.ReturnUrl = Url.ProfileUrl();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("user")]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            ViewBag.ReturnUrl = Url.ProfileUrl();
            if (ModelState.IsValid)
            {
                var result = await _userService.ChangePasswordAsync(User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
                if (result.Success)
                {
                    var user = await _userService.FindByIdAsync(User.Identity.GetUserId());
                    await SignInAsync(user, false);
                    return Redirect(Url.ProfileUrl(ManageMessageId.ChangePasswordSuccess));
                }
                
                AddErrors(result);
            }

            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("logoff")]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            return Redirect(Url.HomePageUrl());
        }

        private IAuthenticationManager AuthenticationManager { get { return HttpContext.GetOwinContext().Authentication; } }

        private async Task SignInAsync(User user, bool isPersistent)
        {
            var identity = await _userService.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(ValidationResultViewModel result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            return Redirect(Url.IsLocalUrl(returnUrl) ? returnUrl : Url.HomePageUrl());
        }
    }
}