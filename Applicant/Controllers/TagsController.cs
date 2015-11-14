using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicantWeb.Models;
using ApplicantWeb.Filters;

namespace ApplicantWeb.Controllers
{
    [Culture]
    public class TagsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // POST: Tags/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagCreate tagCreate)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    db.CreateTagAndAddTagInApplicant(tagCreate);
                    return PartialView("PartialList", new TagList() { ApplicantId = tagCreate.ApplicantId, Tags = db.ToList(tagCreate.ApplicantId) });
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
        public ActionResult ToList(int ApplicantId)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    return PartialView("PartialList", new TagList() { ApplicantId = ApplicantId, Tags = db.ToList(ApplicantId) });
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
        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id, int? applicantId)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    if (id == null || applicantId == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }
                    ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(applicantId);
                    if (applicant == null)
                    {
                        return HttpNotFound();
                    }
                    TagCreate tagCreate = new TagCreate() { TagId = db.Tags.Find(id).TagId, TagName = db.Tags.Find(id).TagName, ApplicantId = applicant.ApplicantId, Applicant = applicant };
                    if (tagCreate == null)
                    {
                        return HttpNotFound();
                    }
                    return PartialView("PartialDelete", tagCreate);
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
        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(TagCreate tagCreate)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    db.DeleteTag(tagCreate);
                    db.SaveChanges();

                    return PartialView("PartialList", new TagList() { Tags = db.ToList(tagCreate.ApplicantId), ApplicantId = tagCreate.ApplicantId });
                }
                catch 
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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
