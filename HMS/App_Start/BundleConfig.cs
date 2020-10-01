using System.Web;
using System.Web.Optimization;

namespace HMS
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquerybundle").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/bootstrap-session-timeout.min.js"
                        ));

            //bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
            //            "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
            //          "~/Scripts/bootstrap.js",
            //          "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                     "~/Content/bootstrap*",
                      "~/Content/Site.css",
                      "~/Content/themes/base/*.css",
                       "~/Content/custom.css",
                      "~/Content/font-awesome.css",
                     "~/Scripts/js/morris/morris-0.4.3.min.css"));

            bundles.Add(new ScriptBundle("~/bundles/jsuserbundle").Include(
                      //"~/Scripts/js/jquery-1.10.2.js",
                      "~/Scripts/js/bootstrap.min.js",
                      "~/Scripts/bootbox.js",
                        "~/Scripts/js/jquery.metisMenu.js",
                        "~/Scripts/js/morris/raphael-2.1.0.min.js",
                       "~/Scripts/js/morris/morris.js",
                       "~/Scripts/autoNumeric/autoNumeric-min.js",
                       "~/Scripts/js/gener.js",
                       //"~/Scripts/bootstrap-datepicker.js",
                       "~/Scripts/js/custom.js"
                        ));

            bundles.Add(new StyleBundle("~/dataTables/bundles/cssbundle").Include(
                            "~/Datatables/dataTables.min.css"
                            //"~/DataTables/dataTables.bootstrap.min.css"
                            ));

            bundles.Add(new ScriptBundle("~/dataTables/bundles/js1bundle").Include(
                        "~/Datatables/datatables.min.js"
                        ));

        }
    }
}
