using System.Web;
using System.Web.Optimization;

namespace Nihongo
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();

            #region Scripts

            #region Jquery

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                        "~/Scripts/modernizr-{version}.min.js",
                        "~/Scripts/jquery-{version}.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/lazyload").Include(
                        "~/Scripts/jquery.lazyload.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.min.js"));
            bundles.Add(new ScriptBundle("~/bundles/jquerycookie").Include(
                        "~/Scripts/js.cookie.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.min.js",
                        "~/Scripts/jquery.validate.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/dr").Include(
                        "~/Scripts/sketch.min.js",
                        "~/Scripts/raphael-min.js",
                        "~/Scripts/dmak/dmak.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/isotope").Include(
                        "~/Scripts/assets/js/jquery.isotope.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/lo").Include(
                        "~/Scripts/mine/lo.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/mi").Include(
                        "~/Scripts/mine/mi.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/le").Include(
                        "~/Scripts/mine/le.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/lenot").Include(
                        "~/Scripts/mine/lenot.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/pr").Include(
                        "~/Scripts/mine/pr.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/prnot").Include(
                        "~/Scripts/mine/prnot.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/ts").Include(
                        "~/Scripts/mine/ts.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/session").Include(
                        "~/Scripts/mine/learningsession.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/fts").Include(
                        "~/Scripts/mine/fts.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/phi").Include(
                        "~/Scripts/mine/phi.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/pka").Include(
                        "~/Scripts/mine/pka.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/hi").Include(
                        "~/Scripts/mine/hi.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/ka").Include(
                        "~/Scripts/mine/ka.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/cp").Include(
                        "~/Scripts/mine/cp.min.js"
                        ));
            #endregion

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.min.js"));

            #region Custom scripts
            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                        "~/Scripts/bootstrap.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/slider").Include(
                //
                        "~/Scripts/assets/js/jquery.cslider.min.js",
                        "~/Scripts/assets/js/jquery.isotope.min.js",
                        "~/Scripts/assets/js/custom.min.js",
                        //"~/Scripts/jquery.mb.audio.min.js",
                        "~/Scripts/mine/mi.min.js",
                        "~/Scripts/mine/load.min.js",
                        "~/Scripts/mine/cp.min.js",
                        "~/Scripts/mine/keepsessionalive.min.js"
                        ));
            bundles.Add(new ScriptBundle("~/bundles/log").Include(
                        "~/Scripts/mine/lo.min.js",
                        "~/Scripts/mine/flog.min.js",
                        "~/Scripts/mine/glog.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/select2").Include(
                        "~/Scripts/select2.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/magnify").Include(
                        "~/Scripts/mine/magnify.min.js"
                        ));

            bundles.Add(new ScriptBundle("~/bundles/audio").Include(
                        "~/Scripts/jquery.mb.audio.min.js"
                        ));

            #endregion

            #endregion

            #region CSS

            //+++ Custom css in here +++
            bundles.Add(new StyleBundle("~/Content/css").Include(
                //My CSS
                        "~/Content/myStyle.min.css",
                "~/Content/assets/css/da-slider.min.css",
                "~/Content/assets/css/font-awesome.min.css",
                "~/Content/assets/css/style.min.css",
                        "~/Content/assets/css/style2.min.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/bootstrap").Include(
                        "~/Content/bootstrap.min.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/select2").Include(
                        "~/Content/select2.min.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/slider").Include(
                //
                        "~/Content/assets/css/da-slider.min.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/font").Include(
                        "~/Content/assets/css/font-awesome.min.css"
                        ));
            bundles.Add(new StyleBundle("~/Content/style").Include(
                        //"~/Content/assets/css/style.min.css",
                        //"~/Content/assets/css/style2.min.css"
                        ));
            //+++ Custom css +++
            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

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

            #endregion
        }
    }
}