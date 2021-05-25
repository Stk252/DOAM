using System.Web;
using System.Web.Optimization;

namespace DOAM
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/javascript").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-migrate-3.0.1.min.js",
                        "~/Scripts/jquery-ui.js",
                        "~/Scripts/umd/popper.js",
                        "~/Scripts/bootstrap.js", 
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap4.js",
                        "~/Scripts/owl.carousel.min.js",
                        "~/Scripts/jquery.stellar.min.js",
                        "~/Scripts/jquery.countdown.min.js",
                        "~/Scripts/jquery.magnific-popup.min.js",
                        "~/Scripts/bootstrap-datepicker.min.js",
                        "~/Scripts/aos.js",
                        "~/Scripts/rangeslider.min.js",
                        "~/Content/Nusearch/js/typeahead.bundle.min.js",
                        "~/Content/Nusearch/js/jquery.timeago.js",
                        "~/Content/Nusearch/js/search.js",
                        "~/Scripts/main.js"));

            bundles.Add(new ScriptBundle("~/bundles/adminjs").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/bootstrap.bundle.js",
                        "~/Scripts/DataTables/jquery.dataTables.js",
                        "~/Scripts/DataTables/dataTables.bootstrap4.js",
                        "~/Content/Admin/js/adminlte.js",
                        "~/Scripts/bootbox.js",
                        "~/Content/Select2/js/select2.js"
                        ));


            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));


            bundles.Add(new StyleBundle("~/Content/css").Include(
                        "~/Content/bootstrap.css",
                        "~/Content/jquery-ui.css",
                        "~/Content/DataTables/css/dataTables.bootstrap4.css",
                        "~/Content/fontawesome-all.css",
                        "~/Content/Fonts/icomoon/style.css",
                        "~/Content/Fonts/flaticon/font/flaticon.css",
                        "~/Content/magnific-popup.css",
                        "~/Content/owl.carousel.min.css",
                        "~/Content/owl.theme.default.min.css",
                        "~/Content/bootstrap-datepicker.css",
                        "~/Content/aos.css",
                        "~/Content/rangeslider.css",
                        "~/Content/style.css",
                        "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/admincss").Include(
                            "~/Content/Admin/fontawesome-free/css/all.css",
                            "~/Content/DataTables/css/dataTables.bootstrap4.css",
                            "~/Content/Select2/css/select2.css",
                            "~/Content/Admin/css/adminlte.css",
                            "~/Content/Admin/admin.css",
                            "~/Content/admin-panel.css"
                        ));
        }
    }
}
