namespace Allors.Web.Identity
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using System.Web.Mvc;

    using Allors.Web.Identity.Models;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;
    using Microsoft.Owin.Security;

    [Authorize]
    public abstract class BaseManageController : Controller
    {
        private IdentitySignInManager _signInManager;
        private IdentityUserManager _userManager;

        public BaseManageController()
        {
        }

        public BaseManageController(IdentityUserManager userManager, IdentitySignInManager signInManager)
        {
            this.UserManager = userManager;
            this.SignInManager = signInManager;
        }

        public virtual IdentitySignInManager SignInManager
        {
            get
            {
                return this._signInManager ?? this.HttpContext.GetOwinContext().Get<IdentitySignInManager>();
            }
            private set 
            { 
                this._signInManager = value; 
            }
        }

        public virtual IdentityUserManager UserManager
        {
            get
            {
                return this._userManager ?? this.HttpContext.GetOwinContext().GetUserManager<IdentityUserManager>();
            }
            private set
            {
                this._userManager = value;
            }
        }

        //
        // GET: /Manage/Index
        public virtual ActionResult Index(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.SetTwoFactorSuccess ? "Your two-factor authentication provider has been set."
                : message == ManageMessageId.Error ? "An error has occurred."
                : message == ManageMessageId.AddPhoneSuccess ? "Your phone number was added."
                : message == ManageMessageId.RemovePhoneSuccess ? "Your phone number was removed."
                : "";

            var userId = this.User.Identity.GetUserId();
            var model = new IndexViewModel
            {
                HasPassword = this.HasPassword(),
                PhoneNumber = this.UserManager.GetPhoneNumber(userId),
                TwoFactor = this.UserManager.GetTwoFactorEnabled(userId),
                Logins = this.UserManager.GetLogins(userId),
                BrowserRemembered = this.AuthenticationManager.TwoFactorBrowserRemembered(userId)
            };
            return this.View(model);
        }

        //
        // POST: /Manage/RemoveLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult RemoveLogin(string loginProvider, string providerKey)
        {
            ManageMessageId? message;
            var result = this.UserManager.RemoveLogin(this.User.Identity.GetUserId(), new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                var user = this.UserManager.FindById(this.User.Identity.GetUserId());
                if (user != null)
                {
                    this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return this.RedirectToAction("ManageLogins", new { Message = message });
        }

        //
        // GET: /Manage/AddPhoneNumber
        public virtual ActionResult AddPhoneNumber()
        {
            return this.View();
        }

        //
        // POST: /Manage/AddPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult AddPhoneNumber(AddPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            // Generate the token and send it
            var code = this.UserManager.GenerateChangePhoneNumberToken(this.User.Identity.GetUserId(), model.Number);
            if (this.UserManager.SmsService != null)
            {
                var message = new IdentityMessage
                {
                    Destination = model.Number,
                    Body = "Your security code is: " + code
                };
                this.UserManager.SmsService.Send(message);
            }
            return this.RedirectToAction("VerifyPhoneNumber", new { PhoneNumber = model.Number });
        }

        //
        // POST: /Manage/EnableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult EnableTwoFactorAuthentication()
        {
            this.UserManager.SetTwoFactorEnabled(this.User.Identity.GetUserId(), true);
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            return this.RedirectToAction("Index", "Manage");
        }

        //
        // POST: /Manage/DisableTwoFactorAuthentication
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult DisableTwoFactorAuthentication()
        {
            this.UserManager.SetTwoFactorEnabled(this.User.Identity.GetUserId(), false);
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            return this.RedirectToAction("Index", "Manage");
        }

        //
        // GET: /Manage/VerifyPhoneNumber
        public virtual ActionResult VerifyPhoneNumber(string phoneNumber)
        {
            var code = this.UserManager.GenerateChangePhoneNumberToken(this.User.Identity.GetUserId(), phoneNumber);
            // Send an SMS through the SMS provider to verify the phone number
            return phoneNumber == null ? this.View("Error") : this.View(new VerifyPhoneNumberViewModel { PhoneNumber = phoneNumber });
        }

        //
        // POST: /Manage/VerifyPhoneNumber
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult VerifyPhoneNumber(VerifyPhoneNumberViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var result = this.UserManager.ChangePhoneNumber(this.User.Identity.GetUserId(), model.PhoneNumber, model.Code);
            if (result.Succeeded)
            {
                var user = this.UserManager.FindById(this.User.Identity.GetUserId());
                if (user != null)
                {
                    this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.AddPhoneSuccess });
            }
            // If we got this far, something failed, redisplay form
            this.ModelState.AddModelError("", "Failed to verify phone");
            return this.View(model);
        }

        //
        // GET: /Manage/RemovePhoneNumber
        public virtual ActionResult RemovePhoneNumber()
        {
            var result = this.UserManager.SetPhoneNumber(this.User.Identity.GetUserId(), null);
            if (!result.Succeeded)
            {
                return this.RedirectToAction("Index", new { Message = ManageMessageId.Error });
            }
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
            }
            return this.RedirectToAction("Index", new { Message = ManageMessageId.RemovePhoneSuccess });
        }

        //
        // GET: /Manage/ChangePassword
        public virtual ActionResult ChangePassword()
        {
            return this.View();
        }

        //
        // POST: /Manage/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }
            var result = this.UserManager.ChangePassword(this.User.Identity.GetUserId(), model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                var user = this.UserManager.FindById(this.User.Identity.GetUserId());
                if (user != null)
                {
                    this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                }
                return this.RedirectToAction("Index", new { Message = ManageMessageId.ChangePasswordSuccess });
            }
            this.AddErrors(result);
            return this.View(model);
        }

        //
        // GET: /Manage/SetPassword
        public virtual ActionResult SetPassword()
        {
            return this.View();
        }

        //
        // POST: /Manage/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult SetPassword(SetPasswordViewModel model)
        {
            if (this.ModelState.IsValid)
            {
                var result = this.UserManager.AddPassword(this.User.Identity.GetUserId(), model.NewPassword);
                if (result.Succeeded)
                {
                    var user = this.UserManager.FindById(this.User.Identity.GetUserId());
                    if (user != null)
                    {
                        this.SignInManager.SignIn(user, isPersistent: false, rememberBrowser: false);
                    }
                    return this.RedirectToAction("Index", new { Message = ManageMessageId.SetPasswordSuccess });
                }
                this.AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            return this.View(model);
        }

        //
        // GET: /Manage/ManageLogins
        public virtual ActionResult ManageLogins(ManageMessageId? message)
        {
            this.ViewBag.StatusMessage =
                message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user == null)
            {
                return this.View("Error");
            }
            var userLogins = this.UserManager.GetLogins(this.User.Identity.GetUserId());
            var otherLogins = this.AuthenticationManager.GetExternalAuthenticationTypes().Where(auth => userLogins.All(ul => auth.AuthenticationType != ul.LoginProvider)).ToList();
            this.ViewBag.ShowRemoveButton = user.PasswordHash != null || userLogins.Count > 1;
            return this.View(new ManageLoginsViewModel
            {
                CurrentLogins = userLogins,
                OtherLogins = otherLogins
            });
        }

        //
        // POST: /Manage/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public virtual ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, this.Url.Action("LinkLoginCallback", "Manage"), this.User.Identity.GetUserId());
        }

        //
        // GET: /Manage/LinkLoginCallback
        public virtual ActionResult LinkLoginCallback()
        {
            var loginInfo = this.AuthenticationManager.GetExternalLoginInfo(XsrfKey, this.User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
            }
            var result = this.UserManager.AddLogin(this.User.Identity.GetUserId(), loginInfo.Login);
            return result.Succeeded ? this.RedirectToAction("ManageLogins") : this.RedirectToAction("ManageLogins", new { Message = ManageMessageId.Error });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && this._userManager != null)
            {
                this._userManager.Dispose();
                this._userManager = null;
            }

            base.Dispose(disposing);
        }

#region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return this.HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                this.ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;
        }

        private bool HasPhoneNumber()
        {
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //if (user != null)
            //{
            //    return user.PhoneNumber != null;
            //}
            return false;
        }

        public enum ManageMessageId
        {
            AddPhoneSuccess,
            ChangePasswordSuccess,
            SetTwoFactorSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            RemovePhoneSuccess,
            Error
        }

#endregion
    }
}