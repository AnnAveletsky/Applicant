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
    public class AttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Attachments/List/
        public ActionResult List(IEnumerable<Applicant.Models.Attachment> attachments)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(attachments.ToList());
            }
          
            if (Request.IsAjaxRequest())
            {
                return PartialView(attachments.ToList());
            }
            return View(attachments.ToList());
        }
        // GET: Attachments/ListAttachmentId/
        public ActionResult ListApplicantId(int id)
        {
            ViewBag.Attachments = db.Attachments.ToList().Where(p => p.ApplicantId == id);
            ViewBag.ApplicantId = id;
            if (Request.IsAjaxRequest())
            {
                return PartialView("PartialList",db.Attachments.Where(p=>p.ApplicantId==id));
            }
            return View("PartialList",db.Attachments.Where(p=>p.ApplicantId==id));
        }


        // POST: Attachments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AttachmentId,Name,ApplicantId,HistoryId,Attach")] Attachment attachment)
        {
            if (Request.IsAjaxRequest())
            {
                if (ModelState.IsValid)
                {
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
                }
                var attachments = db.Attachments.Where(p => p.ApplicantId == attachment.ApplicantId);
                return PartialView("PartialList", attachments);
            }
            if (ModelState.IsValid)
            {
                db.Attachments.Add(attachment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(attachment);
        }
        // GET: Attachments/Load
        public JsonResult Load(int id ,IEnumerable<HttpPostedFileBase> file_data)
        {
            foreach (var i in file_data.ToList())
            {
                Attachment attach=new Attachment();
                attach.ApplicantId = id;
                attach.Attach = new byte[i.ContentLength];
                attach.Type = i.ContentType;
                i.InputStream.Read(attach.Attach, 0, i.ContentLength);
                attach.Name = i.FileName;
                db.Attachments.Add(attach);
                db.SaveChanges();
            }
            ViewBag.Attachments = db.Attachments.ToList().Where(p => p.ApplicantId == id);
            ViewBag.ApplicantId = id;
            return Json("");
        }
        // POST: Attachments/Download
        public FileResult Download(int id, int applicantId)
        {
            Attachment attach=db.Attachments.Find(id);
            ViewBag.Attachments = db.Attachments.ToList().Where(p => p.ApplicantId == applicantId);
            ViewBag.ApplicantId = applicantId;
            return File(attach.Attach,attach.Type,attach.Name);
        }

        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id,int? applicantId)
        {
            if (id == null||applicantId==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Attachment attachment = db.Attachments.Find(id);
            var applicant = db.Applicants.ToList().Find(p => p.AplicantID == applicantId);
            if (attachment == null || applicant==null)
            {
                return HttpNotFound();
            }
            ViewBag.Attachments = db.Attachments.ToList().Where(p => p.ApplicantId == applicantId);
            ViewBag.ApplicantId = applicantId;
            return PartialView("PartialDelete", attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id,int applicantId)
        {
            Attachment attachment = db.Attachments.Find(id);
            db.Attachments.Remove(attachment);
            db.SaveChanges();
            var attachments = db.Attachments.ToList().Where(p => p.ApplicantId == applicantId);
            ViewBag.ApplicantId = applicantId;
            return PartialView("PartialList", attachments);
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
