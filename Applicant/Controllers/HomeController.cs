using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Applicant.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return Redirect("/");
        }

        public ActionResult FAQ()
        {

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}