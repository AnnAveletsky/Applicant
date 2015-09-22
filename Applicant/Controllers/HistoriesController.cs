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
    public class HistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Histories/List/histories
        public ActionResult List(int applicantId)
        {
            return PartialView("PartialList", db.Applicants.Find(applicantId).Histories.ToList());
        }
        // GET: Histories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }


        // POST: Histories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HistoryCreate historyCreate)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    db.Histories.Add(new History(historyCreate));
                    db.SaveChanges();
                    return PartialView("PartialList", db.Histories.Where(p => p.ApplicantId == historyCreate.ApplicantId));
                }
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Histories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return View(history);
        }

        // POST: Histories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HistoryEdit historyEdit)
        {
            History history = db.Histories.Find(historyEdit.HistoryId);
            history.Edit(historyEdit);
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return View("Details", history);
            }
            return HttpNotFound();
        }

        // GET: Histories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            History history = db.Histories.Find(id);
            if (history == null)
            {
                return HttpNotFound();
            }
            return PartialView("PartialDelete",history);
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            History history = db.Histories.Find(id);
            Applicant.Models.Applicant applicant = db.Applicants.Find(history.ApplicantId);

            db.DeleteAttachments(history);
            db.Histories.Remove(history);
            db.SaveChanges();
            return PartialView("PartialList", applicant.Histories.ToList());
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
