namespace Allors.Web.Server
{
    using System.Web.Mvc;

    public class ServerController : Web.Controller
    {
        [HttpPost]
        public ActionResult Load(LoadRequest loadRequest)
        {
            var result = new LoadResponse();

            result.Objects = new LoadObjectResponse[0];

            return Json(result);
        }

        [HttpPost]
        public ActionResult Save(SaveRequest loadRequest)
        {
            var result = "ok";

            return Json(result);
        }
    }
}