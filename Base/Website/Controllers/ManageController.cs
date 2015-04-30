using System.Web.Mvc;

namespace Website.Controllers
{
    using Allors.Web.Identity;

    [Authorize]
    public class ManageController : BaseManageController
    {
        public ManageController()
        {
        }

        public ManageController(IdentityUserManager userManager, IdentitySignInManager signInManager)
            : base(userManager, signInManager)
        {
        }
    }
}