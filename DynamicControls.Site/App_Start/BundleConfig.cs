using System.Web.Optimization;

namespace DynamicControls.Site
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/dynamic").Include("~/Scripts/dynamic.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/dynamic-bootstrap").Include("~/Scripts/dynamic.bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/kendo").IncludeDirectory("~/Scripts/kendo", "*.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").IncludeDirectory("~/Content/kendo", ".css"));
        }
    }
}
