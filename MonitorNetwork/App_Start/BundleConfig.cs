using System.Web;
using System.Web.Optimization;

namespace MonitorNetwork
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

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Content/jquery.qtip-2.2.0.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/inputmask").Include(
                    //~/Scripts/Inputmask/dependencyLibs/inputmask.dependencyLib.js",  //if not using jquery
                    "~/Scripts/Inputmask/inputmask.min.js",
                    "~/Scripts/Inputmask/jquery.inputmask.min.js",
                    "~/Scripts/Inputmask/inputmask.extensions.min.js",
                    "~/Scripts/Inputmask/inputmask.date.extensions.min.js",
                    //and other extensions you want to include
                    "~/Scripts/Inputmask/inputmask.numeric.extensions.min.js",
                    "~/Scripts/Inputmask/inputmask.phone.extensions.min.js",
                    "~/Scripts/Inputmask/phone-codes/phone.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/cytoscape").Include(
                    "~/Scripts/cytoscape-3.2.8.js",
                    "~/Scripts/cytoscape-cose-bilkent-4.0.0.min.js",
                    "~/Scripts/cytoscape-qtip-2.7.0.js",
                    "~/Scripts/jquery.qtip.2.2.0.min.js"
                    ));
        }
    }
}
