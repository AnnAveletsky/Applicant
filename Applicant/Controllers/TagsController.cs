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
    public class TagsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Tags
        public ActionResult Index()
        {
            return View(db.Tags.ToList());
        }
        public ActionResult List(IEnumerable<Applicant.Models.Tag> tags,int aplicantId)
        {
            ViewBag.ApplicantId = aplicantId;
            return PartialView("PartialList",tags);
        }
        // GET: Tags/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TagId,TagName,Applicants")] Tag tag,int id)
        {
            
            if (ModelState.IsValid)
            {
                foreach (var i in db.Tags.ToList())
                {
                    if (String.Compare(i.TagName, tag.TagName) == 0)
                    {
                        var applicant = db.Applicants.ToList().Find(p => p.AplicantID == id);
                        if (i.Applicants == null)
                        {
                            i.Applicants = new List<Applicant.Models.Applicant>();
                        }
                        i.Applicants.Add(applicant);
                        db.SaveChanges();
                        var tags=db.Tags.ToList().Where(p=>p.Applicants.Contains(applicant));
                        ViewBag.Tags = tags;
                        ViewBag.ApplicantId = id;
                        return PartialView("PartialList", tags);
                    }
                }
                db.Tags.Add(tag);
                db.SaveChanges();
                foreach (var i in db.Tags.ToList())
                {
                    if (String.Compare(i.TagName, tag.TagName)==0)
                    {
                        var applicant = db.Applicants.ToList().Find(p=>p.AplicantID==id);
                        if (i.Applicants == null)
                        {
                            i.Applicants = new List<Applicant.Models.Applicant>();
                        }
                        i.Applicants.Add(applicant);
                        db.SaveChanges();
                        var tags=db.Tags.ToList().Where(p=>p.Applicants.Contains(applicant));
                        ViewBag.Tags = tags;
                        ViewBag.ApplicantId = id;
                        return PartialView("PartialList", tags);
                    }
                }
                
            }
            var applicant1 = db.Applicants.ToList().Find(p => p.AplicantID == id);
            var tags1 = db.Tags.ToList().Where(p => p.Applicants.Contains(applicant1));
            ViewBag.Tags = tags1;
            ViewBag.ApplicantId = id;
            return PartialView("PartialList", tags1);
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TagId,TagName")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tag).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tag);
        }


        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id, int applicantId)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tag tag = db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            var applicant1 = db.Applicants.ToList().Find(p => p.AplicantID == applicantId);
            ViewBag.Tags = db.Tags.ToList().Where(p => p.Applicants.Contains(applicant1));
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
            db.SaveChanges();
            var applicant1 = db.Applicants.ToList().Find(p => p.AplicantID == applicantId);
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
