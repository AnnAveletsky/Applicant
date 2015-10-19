using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ApplicantClassLibrary;
using Applicant.Models;

namespace Applicant.Controllers
{
    public class LanguagesController : Controller
    {
        Language Language=new Language();
        public ActionResult ChangeLanguage(Lang currentLang)
        {
            Language.CurrentLang = currentLang;
            return Redirect("/");
        }
        public ActionResult Details()
        {
            return PartialView("Details", Language);
        }
    }
}