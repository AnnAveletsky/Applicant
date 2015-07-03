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
    public class AttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attachments
        public ActionResult Index()
        {
            var attachments = db.Attachments.Include(a => a.History);
            return View(attachments.ToList());
        }
        public ActionResult List(IEnumerable<Applicant.Models.Attachment> attachments)
        {
            if (attachments == null)
            {
                attachments = db.Attachments.Include(a => a.History);
            }
            return PartialView(attachments.ToList());
        }
        // GET: Attachments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // GET: Attachments/Create
        public ActionResult Create()
        {
            ViewBag.HistoryId = new SelectList(db.Histories, "HistoryId", "HistoryComments");
            return PartialView();
        }

        // POST: Attachments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttachmentId,Name,ApplicantId,HistoryId,Attach")] Attachment attachment)
        {
            var attachments=db.Attachments.Where(p=>p.ApplicantId==attachment.ApplicantId);
            foreach (var i in db.Applicants.ToList())
            {
                if (i.AplicantID == attachment.ApplicantId)
                {
                    i.Attachments.Add(attachment);
                    break;
                }
            }
            db.Attachments.Add(attachment);
            db.SaveChanges();
            
            return PartialView("PartialList", attachments);
        }

        // GET: Attachments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            ViewBag.HistoryId = new SelectList(db.Histories, "HistoryId", "HistoryComments", attachment.HistoryId);
            return View(attachment);
        }

        // POST: Attachments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttachmentId,Name,ApplicantId,HistoryId,Attach")] Attachment attachment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attachment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HistoryId = new SelectList(db.Histories, "HistoryId", "HistoryComments", attachment.HistoryId);
            return View(attachment);
        }

        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            if (attachment == null)
            {
                return HttpNotFound();
            }
            return View(attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attachment attachment = db.Attachments.Find(id);
            db.Attachments.Remove(attachment);
            db.SaveChanges();
            return RedirectToAction("Index");
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
