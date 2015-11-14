using System.Web;
using System.Web.Optimization;

namespace ApplicantWeb
{
    public class BundleConfig
    {
        //Дополнительные сведения об объединении см. по адресу: http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/fileinput").Include(
                "~/Scripts/fileinput.min.js",
                "~/Scripts/fileinput_locale_ru.js"));

            bundles.Add(new StyleBundle("~/Content/fileinput").Include(
                "~/Content/bootstrap-fileinput/css/fileinput.min.css"));

            // Используйте версию Modernizr для разработчиков, чтобы учиться работать. Когда вы будете готовы перейти к работе,
            // используйте средство сборки на сайте http://modernizr.com, чтобы выбрать только нужные тесты.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/bootstrap.css",
                "~/Content/site.css"));
            //Собственный скрипт
            bundles.Add(new ScriptBundle("~/bundles/advanced").Include(
                "~/Scripts/advanced_script.js"));

            bundles.Add(new StyleBundle("~/bundles/fonts").Include(
                "~/fonts/font-awesome.min.css"));
            
            bundles.Add(new StyleBundle("~/bundles/admin-lte").Include(
                "~/Content/admin-lte/css/AdminLTE.min.css",
                "~/Content/admin-lte/css/skins/_all-skins.min.css"));
        }
    }
}
