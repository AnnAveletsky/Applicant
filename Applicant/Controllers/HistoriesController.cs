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

        // GET: Histories
        public ActionResult Index()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(db.Histories.ToList());
            }
            return View(db.Histories.ToList());
        }
        // GET: Histories/List/histories
        public ActionResult List(IEnumerable<Applicant.Models.History> histories)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(histories.ToList());
            }
            return View(histories.ToList());
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

        // GET: Histories/Create
        public ActionResult Create()
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView();
            }
            return View();
        }

        // POST: Histories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "HistoryId,CommunicationDate,TypeCommunication,HistoryComments")] History history)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
                    foreach (var i in db.Applicants.ToList())
                    {
                        if (i.AplicantID == history.ApplicantId)
                        {
                            i.Histories.Add(history);
                            break;
                        }
                    }
                    db.Histories.Add(history);
                    db.SaveChanges();
                }
                var histories = db.Histories.Where(p => p.ApplicantId == history.ApplicantId);
                return PartialView("List", histories);
            }
            if (ModelState.IsValid)
            {
                db.Histories.Add(history);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
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
        public ActionResult Edit([Bind(Include = "HistoryId,CommunicationDate,TypeCommunication,HistoryComments")] History history)
        {
            if (ModelState.IsValid)
            {
                db.Entry(history).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(history);
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
            return View(history);
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            History history = db.Histories.Find(id);
            db.Histories.Remove(history);
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
