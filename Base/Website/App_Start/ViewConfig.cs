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
"~/Areas/Default/Tests/{1}/{0}.cshtml",
"~/Areas/Default/Base/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Areas/Default/Tests/Shared/{0}.cshtml",
"~/Areas/Default/Base/Shared/{0}.cshtml",
};

                this.MasterLocationFormats = this.ViewLocationFormats;

                this.PartialViewLocationFormats = new[]
{
"~/Views/{1}/{0}.cshtml",
"~/Areas/Default/Tests/{1}/{0}.cshtml",
"~/Areas/Default/Base/{1}/{0}.cshtml",
"~/Views/Shared/{0}.cshtml",
"~/Areas/Default/Tests/Shared/{0}.cshtml",
"~/Areas/Default/Base/Shared/{0}.cshtml",
};
            }
        }
    }
}
