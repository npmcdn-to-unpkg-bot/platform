namespace Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Allors Client
            bundles.Add(new ScriptBundle("~/bundles/angular/allors")
                .IncludeDirectory("~/allors/client/Base/Workspace/", "*.js", true)
                .IncludeDirectory("~/allors/client/Base/Angular/", "app.module.js")
                .IncludeDirectory("~/allors/client/Base/Angular/", "*.js", true)
                .IncludeDirectory("~/allors/client/Generated/", "*.js", true)
                .IncludeDirectory("~/allors/client/Custom/", "*.js", true)
                .IncludeDirectory("~/allors/client/", "*.js", true));

            // Angular App
            bundles.Add(new ScriptBundle("~/bundles/angular/app")
                .Include("~/app/app.js")
                .IncludeDirectory("~/app/", "*.js",  true));

            // Angular Tests
            bundles.Add(new ScriptBundle("~/bundles/angular/tests")
                .Include("~/Tets/app.js")
                .IncludeDirectory("~/Tests/", "*.js", true));
        }
    }
}
