namespace Website.Controllers
{
    using System.Web.Mvc;

    using Allors.Web.Identity;

    [Authorize]
    public class AccountController : BaseAccountController
    {
    }
}