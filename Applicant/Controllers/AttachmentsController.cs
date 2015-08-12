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

        // GET: Attachments/List/
        public ActionResult List(IEnumerable<Attachment> attachments)
        {
            if (Request.IsAjaxRequest())
            {
                return PartialView(attachments.ToList());
            }
            return View(attachments.ToList());
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
            ViewBag.Attachments = db.Attachments.Where(p => p.ApplicantId == id);
            ViewBag.ApplicantId = id;
            return Json("");
        }
        // POST: Attachments/Download
        public FileResult Download(int id, int applicantId)
        {
            Attachment attach=db.Attachments.Find(id);
            ViewBag.Attachments = db.Attachments.Where(p => p.ApplicantId == applicantId);
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
            var applicant = db.Applicants.Find(applicantId);
            if (attachment == null || applicant==null)
            {
                return HttpNotFound();
            }
            ViewBag.Attachments = db.Attachments.Where(p => p.ApplicantId == applicantId);
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
            var attachments = db.Attachments.Where(p => p.ApplicantId == applicantId);
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
