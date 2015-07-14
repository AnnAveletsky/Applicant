﻿using System.Web;
using System.Web.Optimization;

namespace Applicant
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

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-tags").Include(
                "~/Scripts/k-bootstrap-tags.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-tags").Include(
                     "~/Scripts/bootstrap-tags/dist/css/bootstrap-tags.css"));

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
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
        }
    }
}
