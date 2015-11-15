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
    public class HistoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public JsonResult List(int id)
        {
            var json = (from hist in db.Histories
                        where hist.ApplicantId == id
                        select new
                        {
                            HistoryId = hist.HistoryId,
                            CommunicationDate = new { 
                                Day = hist.CommunicationDate.Day, 
                                Month = hist.CommunicationDate.Month, 
                                Year = hist.CommunicationDate.Year 
                            },
                            TypeCommunication = hist.TypeCommunication,
                            HistoryComments = ((hist.HistoryComments.Length > 20) ? hist.HistoryComments.Substring(0, 20)+"..." : hist.HistoryComments)
                        });
            return Json(new { data = json }, JsonRequestBehavior.AllowGet);
        }
        // GET: Histories/List/histories
        public ActionResult PartialList(int applicantId)
        {
            if (Request.IsAuthenticated)
            {
                return PartialView("PartialList", db.Applicants.Find(applicantId));
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        // GET: Histories/Details/5
        public ActionResult Details(int? id)
        {
            if (Request.IsAuthenticated)
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
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }


        // POST: Histories/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(HistoryCreate historyCreate)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        db.Histories.Add(new History(historyCreate));
                        db.SaveChanges();
                        return PartialView("PartialList", db.Applicants.Find(historyCreate.ApplicantId));
                    }
                    return View(historyCreate);
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

        // GET: Histories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Request.IsAuthenticated)
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
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        // POST: Histories/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HistoryEdit historyEdit)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    History history = db.Histories.Find(historyEdit.HistoryId);
                    history.Edit(historyEdit);
                    if (ModelState.IsValid)
                    {
                        db.Entry(history).State = EntityState.Modified;
                        db.SaveChanges();
                        return View("Details", history);
                    }
                    return View(historyEdit);
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

        // GET: Histories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
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
                return View("Delete", history);
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }

        // POST: Histories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    History history = db.Histories.Find(id);
                    var idApplicant = history.ApplicantId;
                    db.DeleteAttachments(history);
                    db.Histories.Remove(history);
                    db.SaveChanges();
                    return Redirect(Url.Action("Details", "Applicants", new { id = idApplicant }));
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
