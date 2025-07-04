using System.Web;
using System.Web.Optimization;

namespace WebBanDoTrangMieng
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-3.7.0.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new Bundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.min.js"));

            // Optimized CSS Bundle - Removed duplicates and organized by priority
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",      // Bootstrap framework (priority 1)
                      "~/Content/Site.css",           // Global site styles (priority 2)
                      "~/Content/layout.css",         // Layout-specific styles (priority 3)
                      "~/Content/components.css",     // Reusable components (priority 4)
                      "~/Content/product.css",        // Product-specific styles (priority 5)
                      "~/Content/cart.css",           // Cart-specific styles (priority 6)
                      "~/Content/auth-modal.css"));   // Auth modal styles (priority 7)
        }
    }
}
