using System.Web;
using System.Web.Optimization;

namespace TaskMA
{
    public class BundleConfig
    {
        
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Scripts/bootstrap-formhelpers-phone.js",
                      "~/Scripts/bootstrap-datepicker.js"
                      ));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                       "~/Content/Site.css",
                      "~/Content/bootstrap-datepicker.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/bundles/datetime").Include(
                      "~/Scripts/moment*",
                      "~/Scripts/bootstrap-datetimepicker*"));

        }
    }
}
