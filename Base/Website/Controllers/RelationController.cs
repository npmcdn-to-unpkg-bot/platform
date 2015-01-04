using System.Web.Mvc;

namespace Website.Controllers
{
    using Website.Areas.Admin.Controllers;

    [Authorize]
    public class RelationController : TestsRelationController
    {
    }
}