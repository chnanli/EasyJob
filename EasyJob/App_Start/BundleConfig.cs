using System.Web;
using System.Web.Optimization;

namespace EasyJob
{
    public class BundleConfig
    {
        private const string MapApiAk = "DzXjMDztNrEV9SwCEtIqty9x";

        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));

            //####################################################################################################

            bundles.Add(new ScriptBundle("~/angular/js").Include(
            "~/Scripts/js/angular.js"
            ));

            bundles.Add(new ScriptBundle("~/jquery/js").Include(
            "~/Scripts/js/jquery-2.1.4-min.js",
            "~/Scripts/js/jquery.json-min.js",
            "~/Scripts/js/ajax-upload-file.js"
            ));

            bundles.Add(new ScriptBundle("~/bootstrap/js").Include(
                "~/Scripts/js/bootstrap.js",
                "~/Scripts/js/bootstrap-modal.js"
                ));

            bundles.Add(new StyleBundle("~/bootstrap/css").Include(
                "~/Scripts/css/bootstrap.css",
                "~/Scripts/css/main.css"
                ));

            bundles.Add(new StyleBundle("~/bui/assets/css").Include(
                "~/Scripts/bui/assets/css/dpl-min.css",
                "~/Scripts/bui/assets/css/bui-min.css",
                "~/Scripts/bui/assets/css/main-min.css"
                ));

            bundles.Add(new ScriptBundle("~/bui/assets/js").Include(
                "~/Scripts/bui/assets/js/jquery-1.8.1.js",
                "~/Scripts/bui/assets/js/bui-min.js",
                "~/Scripts/bui/assets/js/common/main-min.js",
                "~/Scripts/bui/assets/js/config-min.js"
                ));

            bundles.Add(new StyleBundle("~/bui/css").Include(
                "~/Scripts/bui/Css/bootstrap.css",
                "~/Scripts/bui/Css/bootstrap-responsive.css",
                "~/Scripts/bui/Css/style.css"
                ));

            bundles.Add(new ScriptBundle("~/bui/js").Include(
                "~/Scripts/bui/Js/jquery.js",
                "~/Scripts/bui/Js/jquery.sorted.js",
                "~/Scripts/bui/Js/bootstrap.js",
                "~/Scripts/bui/Js/ckform.js",
                "~/Scripts/bui/Js/common.js"
                ));

            bundles.Add(new ScriptBundle("~/errorCode/js").Include(
                "~/Scripts/js/errorCode.js"
                ));

            bundles.Add(new ScriptBundle("~/angularFuncs/js").Include(
                "~/Scripts/js/angularFuncs.js"
                ));

            bundles.Add(new StyleBundle("~/easyui/css").Include(
                "~/Scripts/js/easyui/themes/default/easyui.css",
                "~/Scripts/js/easyui/themes/icon.css"
                ));

            bundles.Add(new ScriptBundle("~/easyui/js").Include(
                "~/Scripts/js/easyui/jquery-min.js",
                "~/Scripts/js/easyui/jquery.easyui-min.js"
                ));
        }
    }
}