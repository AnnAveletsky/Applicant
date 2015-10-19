using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Applicant
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "lang",
                url: "{lang}/{controller}/{action}/{id}",
                defaults: new { controller = "Applicants", action = "Index", id = UrlParameter.Optional },
                constraints: new { lang = @"ru|en" }
            );
            routes.MapRoute(

                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Applicants", action = "Index", id = UrlParameter.Optional, lang = "ru" }
            );
        }
    }
}
