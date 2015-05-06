namespace Website
{
    using System.Web.Mvc;

    public class ViewConfig
    {
        public static void Register()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new AllorsRazorViewEngine());
        }

        private class AllorsRazorViewEngine : RazorViewEngine
        {
            public AllorsRazorViewEngine()
            {
                this.ViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Allors/Tests/Default/{1}/{0}.cshtml",
"~/Allors/Base/Default/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Allors/Tests/Default/Shared/{0}.cshtml",
"~/Allors/Base/Default/Shared/{0}.cshtml",
};

                this.MasterLocationFormats = this.ViewLocationFormats;

                this.PartialViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Allors/Tests/Default/{1}/{0}.cshtml",
"~/Allors/Base/Default/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Allors/Tests/Default/Shared/{0}.cshtml",
"~/Allors/Base/Default/Shared/{0}.cshtml",
};
            }
        }
    }
}
