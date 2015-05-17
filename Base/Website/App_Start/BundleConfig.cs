namespace Website
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
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

            // Used by Identity
            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/Scripts/jquery.validate*"));
        }
    }
}
