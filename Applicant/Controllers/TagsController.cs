﻿using System;
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
    public class TagsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult List(IEnumerable<Tag> tags,int aplicantId)
        {
            ViewBag.ApplicantId = aplicantId;
            return PartialView("PartialList",tags);
        }

        // POST: Tags/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TagCreate tagCreate)
        {

            db.CreateTagAndAddTagInApplicant(tagCreate);
            if (ModelState.IsValid)
            {
                tagCreate.Applicant = db.Applicants.Find(tagCreate.ApplicantId);
            }
            return PartialView("PartialList", db.Applicants.Find(tagCreate.ApplicantId).Tags.ToList());
        }
        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id, int? applicantId)
        {
            if (id == null||applicantId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            Applicant.Models.Applicant applicant = db.Applicants.ToList().Find(p => p.ApplicantId == applicantId);
            if (tag == null||applicant==null)
            {
                return HttpNotFound();
            }
            ViewBag.Tags = db.Tags.ToList().Where(p => p.Applicants.Contains(applicant));
            ViewBag.ApplicantId = applicantId;
            return PartialView("PartialDelete",tag);
        }
        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int applicantId)
        {
            Tag tag = db.Tags.Find(id);
            Applicant.Models.Applicant applicant=db.Applicants.Find(applicantId);
            tag.Applicants.Remove(applicant);
            if (tag.Applicants.Count == 0)
            {
                db.Tags.Remove(tag);
            }
            db.SaveChanges();
            var applicant1 = db.Applicants.ToList().Find(p => p.ApplicantId == applicantId);
            var tags1 = db.Tags.ToList().Where(p => p.Applicants.Contains(applicant1));
            ViewBag.ApplicantId = applicantId;
           return PartialView("PartialList", tags1);
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
