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
    public class ApplicantController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Applicant
        public ActionResult Index()
        {
            return View(db.Applicants.ToList());
        }

        // GET: Applicant/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantModel applicantModel = db.Applicants.Find(id);
            if (applicantModel == null)
            {
                return HttpNotFound();
            }
            return View(applicantModel);
        }

        // GET: Applicant/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Applicant/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AplicantID,FirstName,MiddleName,LastName,Email,Phone,Comments,Salary")] ApplicantModel applicantModel)
        {
            if (ModelState.IsValid)
            {
                db.Applicants.Add(applicantModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(applicantModel);
        }

        // GET: Applicant/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantModel applicantModel = db.Applicants.Find(id);
            if (applicantModel == null)
            {
                return HttpNotFound();
            }
            return View(applicantModel);
        }

        // POST: Applicant/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AplicantID,FirstName,MiddleName,LastName,Email,Phone,Comments,Salary")] ApplicantModel applicantModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicantModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(applicantModel);
        }

        // GET: Applicant/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicantModel applicantModel = db.Applicants.Find(id);
            if (applicantModel == null)
            {
                return HttpNotFound();
            }
            return View(applicantModel);
        }

        // POST: Applicant/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplicantModel applicantModel = db.Applicants.Find(id);
            db.Applicants.Remove(applicantModel);
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
