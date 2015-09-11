namespace Website
{
    using System.Web.Optimization;

    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/allors").Include(
                        "~/allors/*.js"));
            bundles.Add(new ScriptBundle("~/bundles/generated").Include(
                        "~/Generated/*.js"));
        }
    }
}
