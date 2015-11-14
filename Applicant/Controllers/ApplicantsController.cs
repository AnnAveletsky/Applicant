using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicantWeb.Models;
using ApplicantClassLibrary;
using System.Web.Script.Serialization;
using ApplicantWeb.Filters;

namespace ApplicantWeb.Controllers
{
    [Culture]
    public class ApplicantsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Applicants
        public ActionResult Index()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect(Url.Action("Login","Account"));
            }
        }
      
        public JsonResult List()
        {
            var json = db.Applicants.Select(i => new
            {
                ФамилияИмя = "<a class='"+(i.Gender==0?"man":"woman")+"' href=/Applicants/Details/" + i.ApplicantId + "><span class='glyphicon glyphicon-user' aria-hidden='true'></span> " + i.FirstName + " " + i.MiddleName + "</a>",
                Возраст = (DateTime.Now.Month < i.Birthday.Month || (DateTime.Now.Month == i.Birthday.Month && DateTime.Now.Day < i.Birthday.Day) ? DateTime.Now.Year - i.Birthday.Year - 1 : DateTime.Now.Year - i.Birthday.Year),
                Город = i.City,
                Теги =i.Tags.OrderBy(k=>k.TagName).Select(j => " <div class='label label-info'>" + j.TagName + "</div>")
            });
            return Json(new {data= json }, JsonRequestBehavior.AllowGet);
        }

        // GET: Applicants/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(id);
                if (applicant == null)
                {
                    return HttpNotFound();
                }
                return View(applicant);
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        public ActionResult Photo(int idApplicant)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var applicant = db.Applicants.Find(idApplicant);
                    return PartialView("PartialPhoto", applicant.Photo);
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        public ActionResult AttachmentToAva(int idAttachment)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    var attachment = db.Attachments.Find(idAttachment);
                    if (attachment != null)
                    {
                        var applicant = db.Applicants.Find(attachment.ApplicantId);
                        if (applicant != null)
                        {
                            applicant.Photo = attachment.Attach;
                            db.SaveChanges();
                            return PartialView("PartialPhoto", applicant.Photo);
                        }
                    }
                    return HttpNotFound();
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        
        // GET: Applicants/Create
        public ActionResult Create()
        {
            if (Request.IsAuthenticated)
            {
                return View();
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        // POST: Applicants/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApplicantFields applicantFields)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    ApplicantWeb.Models.Applicant applicant = new Models.Applicant(applicantFields);
                    if (ModelState.IsValid)
                    {
                        db.Applicants.Add(applicant);
                        db.SaveChanges();
                        return Redirect(Url.Action("Details", "Applicants", new { id = applicant.ApplicantId }));
                    }
                    return View(applicant);
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        // GET: Applicants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(id);
                if (applicant == null)
                {
                    return HttpNotFound();
                }
                return View(applicant);
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        // POST: Applicants/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ApplicantEdit applicantEdit)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(applicantEdit.ApplicantId);
                    if (ModelState.IsValid)
                    {
                        applicant.Edit(applicantEdit);
                        db.Entry(applicant).State = EntityState.Modified;
                        db.SaveChanges();
                        return Redirect(Url.Action("Details", "Applicants", new { id = applicant.ApplicantId }));
                    }
                    return View(applicant);
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        // GET: Applicants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(id);
                if (applicant == null)
                {
                    return HttpNotFound();
                }
                return View(applicant);
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(id);
                    db.DeleteAttachments(applicant);
                    db.DeleteHistories(applicant);
                    db.Applicants.Remove(applicant);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return HttpNotFound();
                }
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
