namespace Website
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // Mvc bundles
            bundles.Add(new ScriptBundle("~/bundles/js/default/pre").Include(
                        "~/Scripts/modernizr-*",
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/js/default/post").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/moment-with-locales.js",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-select.js",
                        "~/Scripts/bootstrap-datetimepicker.js",
                        "~/Scripts/jasny-bootstrap.js",
                        "~/Scripts/tinymce/tinymce.js",
                        "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/bundles/css/default").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/bootstrap-select.css",
                        "~/Content/bootstrap-datetimepicker.css",
                        "~/Content/jasny-bootstrap.css",
                        "~/Content/site.css"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));

            // Angular bundles
            bundles.Add(new ScriptBundle("$/bundles/angular/app")
                .Include("~/app/" + "~/app/app.module.js.js")
                .IncludeDirectory("~/app/", "*.js",  true));

            bundles.Add(new ScriptBundle("$/bundles/angular/allors")
                .IncludeDirectory("~/allors/client/", "*.js", true));
        }
    }
}
