using System.Web.Optimization;

namespace DynamicControls.Site
{
    /// <summary>
    /// The bundle config.
    /// </summary>
    public class BundleConfig
    {
        /// <summary>
        /// The register bundles.
        /// </summary>
        /// <param name="bundles">
        /// The bundles.
        /// </param>
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include("~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/dynamic").Include("~/Scripts/dynamic.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include("~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include("~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/dynamic-bootstrap").Include("~/Scripts/dynamic.bootstrap.js"));            

            bundles.Add(new ScriptBundle("~/bundles/kendo").Include("~/Scripts/kendo/kendo.core.js", "~/Scripts/kendo/kendo.ui.core.js", "~/Scripts/kendo/kendo.list.js").IncludeDirectory("~/Scripts/kendo", "*.js"));

            bundles.Add(new ScriptBundle("~/bundles/dynamic-kendo").Include("~/Scripts/dynamic.kendo.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/bootstrap.css", "~/Content/bootstrap-theme.css", "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/kendo").IncludeDirectory("~/Content/kendo", "*.css"));
        }
    }
}
