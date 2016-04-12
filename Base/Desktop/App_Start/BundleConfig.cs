﻿namespace Web
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            // BundleTable.EnableOptimizations = true;

            // Css
            bundles.Add(new StyleBundle("~/bundles/css")
               .Include("~/lib/bootstrap/dist/css/bootstrap.css")
               .Include("~/lib/bootstrap/dist/css/bootstrap-theme.css")
               .Include("~/lib/ui-select/dist/select.css")
               .Include("~/lib/angular-chart.js/dist/angular-chart.css")
               .Include("~/lib/angular-toastr/dist/angular-toastr.css")
               .Include("~/lib/angular-loading-bar/build/loading-bar.css")
               .Include("~/lib/angular-toggle-switch/angular-toggle-switch.css")
               .Include("~/lib/angular-toggle-switch/angular-toggle-switch-bootstrap.css")
               .Include("~/lib/awesome-bootstrap-checkbox/awesome-bootstrap-checkbox.css")
               .Include("~/lib/ng-img-crop-full-extended/compile/unminified/ng-img-crop.css")
               .Include("~/lib/textAngular/dist/textAngular.css")
               .Include("~/Content/default.css"));

            // Common
            bundles.Add(
                new ScriptBundle("~/bundles/common")
               .Include("~/lib/jquery/dist/jquery.js")
               .Include("~/lib/lodash/lodash.js")
               .Include("~/lib/moment/moment.js")
               .Include("~/lib/Chart.js/Chart.js"));

            // Angular
            bundles.Add(
                new ScriptBundle("~/bundles/angular")
               .Include("~/lib/angular/angular.js")
               .Include("~/lib/angular-cookies/angular-cookies.js")
               .Include("~/lib/angular-animate/angular-animate.js")
               .Include("~/lib/angular-bootstrap/ui-bootstrap-tpls.js")
               .Include("~/lib/angular-toastr/dist/angular-toastr.tpls.js")
               .Include("~/lib/angular-toggle-switch/angular-toggle-switch.js")
               .Include("~/lib/angular-translate/angular-translate.js")
               .Include("~/lib/angular-translate-loader-url/angular-translate-loader-url.js")
               .Include("~/lib/angular-loading-bar/build/loading-bar.js")
               .Include("~/lib/angular-breadcrumb/release/angular-breadcrumb.js")
               .Include("~/lib/angular-drag-and-drop-lists/angular-drag-and-drop-lists.js")
               .Include("~/lib/angular-chart.js/dist/angular-chart.js")
               .Include("~/lib/angular-dynamic-locale/dist/tmhDynamicLocale.js")
               .Include("~/lib/angular-ui-router/release/angular-ui-router.js")
               .Include("~/lib/ui-select/dist/select.js")
               .Include("~/lib/ng-img-crop-full-extended/compile/unminified/ng-img-crop.js")
               .Include("~/lib/textAngular/dist/textAngular-sanitize.min.js")
               .Include("~/lib/textAngular/dist/textAngular-rangy.min.js")
               .Include("~/lib/textAngular/dist/textAngular.min.js"));

            // Allors Client
            bundles.Add(new ScriptBundle("~/bundles/allors")
                .IncludeDirectory("~/allors/client/Base/Workspace/", "*.js", true)
                // module and base classes
                .Include("~/allors/client/Base/Angular/allors.module.js")
                .Include("~/allors/client/Base/Angular/components/bootstrap/internal/Field.js")
                // by domain
                .IncludeDirectory("~/allors/client/Base/Angular/", "*.js", true)
                .IncludeDirectory("~/allors/client/Generated/", "*.js", true)
                .IncludeDirectory("~/allors/client/Custom/", "*.js", true)
                .IncludeDirectory("~/allors/client/", "*.js", true));

            // App
            bundles.Add(new ScriptBundle("~/bundles/app")
                .Include("~/app/app.js")
                .IncludeDirectory("~/app/", "*.js",  true));

            // Tests
            bundles.Add(new ScriptBundle("~/bundles/tests")
                .Include("~/Scripts/tsUnit/tsUnit.js")
                .Include("~/Tests/app.js")
                .IncludeDirectory("~/Tests/", "*.js", true));
        }
    }
}
