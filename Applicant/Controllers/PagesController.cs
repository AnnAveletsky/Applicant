using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Applicant.Models;

namespace Applicant.Controllers
{
    public class PagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        [HttpPost]
        public ActionResult ToPage(int? toPage, string search, int? countElementsInPage, string poleSort)
        {
            if (toPage == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant.Page page = new Applicant.Page(db.Applicants, search, toPage, countElementsInPage, poleSort);
            return PartialView("~/View/Applicants/PartialPage.cshtml", page);
        }
    }
}