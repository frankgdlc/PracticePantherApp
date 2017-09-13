using System.Web.Optimization;

namespace CalendarApp.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Kendo UI (JS files)
            bundles.Add(new ScriptBundle("~/bundles/kendo-ui").Include(
                        "~/Scripts/kendo-ui/kendo.web.min.js",
                        "~/Scripts/kendo-ui/kendo.aspnetmvc.min.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // Kendo UI (CSS files)
            bundles.Add(new StyleBundle("~/Content/kendo").Include(
                      "~/Content/kendo/kendo.common.min.css",
                      "~/Content/kendo/kendo.default.min.css"));
        }
    }
}
