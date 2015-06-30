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
    public class AttachmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attachment
        public ActionResult Index()
        {
            return View(db.AttachmentModels.ToList());
        }

        // GET: Attachment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttachmentModel attachmentModel = db.AttachmentModels.Find(id);
            if (attachmentModel == null)
            {
                return HttpNotFound();
            }
            return View(attachmentModel);
        }

        // GET: Attachment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Attachment/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttachmentId,ApplicantId,Name,DateTimeAdd,ApplicantIdAdd,DataTimeDelete,ApplicantIdDelete")] AttachmentModel attachmentModel)
        {
            if (ModelState.IsValid)
            {
                db.AttachmentModels.Add(attachmentModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(attachmentModel);
        }

        // GET: Attachment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttachmentModel attachmentModel = db.AttachmentModels.Find(id);
            if (attachmentModel == null)
            {
                return HttpNotFound();
            }
            return View(attachmentModel);
        }

        // POST: Attachment/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AttachmentId,ApplicantId,Name,DateTimeAdd,ApplicantIdAdd,DataTimeDelete,ApplicantIdDelete")] AttachmentModel attachmentModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(attachmentModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attachmentModel);
        }

        // GET: Attachment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AttachmentModel attachmentModel = db.AttachmentModels.Find(id);
            if (attachmentModel == null)
            {
                return HttpNotFound();
            }
            return View(attachmentModel);
        }

        // POST: Attachment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AttachmentModel attachmentModel = db.AttachmentModels.Find(id);
            db.AttachmentModels.Remove(attachmentModel);
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
