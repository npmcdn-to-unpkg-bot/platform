namespace Website.Error
{
    using System.Web.Mvc;

    public class ErrorController : CustomController
    {
        [Authorize]
        [ValidateInput(false)]
        public ActionResult Index(Model model)
        {
            // TODO: Log to the database
            return View(model);
        }
    }
}