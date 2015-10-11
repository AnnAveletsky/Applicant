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

        // POST: Attachments/List/
        public ActionResult List(int applicantId)
        {
           
                var attachments = db.Applicants.Find(applicantId).Attachments;
                return PartialView("PartialList", attachments.ToList());
           
        }
        public ActionResult ListToHistory(int historyId)
        {
            
                return PartialView("PartialListToHistory", db.Histories.Find(historyId).Attachments.ToList());
       
        }
        // GET: Attachments/Load
        public JsonResult Load(int applicantId ,IEnumerable<HttpPostedFileBase> file_data)
        {
            foreach (var i in file_data.ToList())
            {
                db.Attachments.Add(db.AddAttachmentInApplicant(i, applicantId));
               db.SaveChanges();
            }
            return Json("");
        }
        public JsonResult LoadToHistory(int historyId, IEnumerable<HttpPostedFileBase> file_data)
        {
            foreach (var i in file_data.ToList())
            {
                db.Histories.Find(historyId).Attachments.Add(db.AddAttachmentInApplicant(i,null));
                db.SaveChanges();
            }
            return Json("");
        }
        // POST: Attachments/Download
        public FileResult Download(int id, int applicantId)
        {
            Attachment attach=db.Attachments.Find(id);
            return File(attach.Attach,attach.Type,attach.Name);
        }
        public ActionResult AddToApplicant(int attachmentId)
        {
            Attachment attachment = db.Attachments.Find(attachmentId);
            attachment.History = db.Histories.Find(attachment.HistoryId);
            Applicant.Models.Applicant applicant = db.Applicants.Find(attachment.History.ApplicantId);
            attachment.ApplicantId = applicant.ApplicantId;
            attachment.Applicant = applicant;
            applicant.Attachments.Add(attachment);
            db.SaveChanges();
            return PartialView("PartialListToHistory", attachment.History.Attachments.ToList());
        }
        public ActionResult DeleteToApplicant(int attachmentId)
        {
            Attachment attachment = db.Attachments.Find(attachmentId);
            attachment.History = db.Histories.Find(attachment.HistoryId);
            Applicant.Models.Applicant applicant = db.Applicants.Find(attachment.History.ApplicantId);
            attachment.ApplicantId = null;
            attachment.Applicant = null;
            applicant.Attachments.Remove(attachment);
            db.SaveChanges();
            return PartialView("PartialListToHistory", applicant.Attachments.ToList());
        }
        public ActionResult DeleteToApplicantToHistory(int attachmentId)
        {
            Attachment attachment = db.Attachments.Find(attachmentId);
            attachment.History = db.Histories.Find(attachment.HistoryId);
            Applicant.Models.Applicant applicant = db.Applicants.Find(attachment.History.ApplicantId);
            attachment.ApplicantId = null;
            attachment.Applicant = null;
            applicant.Attachments.Remove(attachment);
            db.SaveChanges();
            return PartialView("PartialListToHistory", attachment.History.Attachments.ToList());
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
            return PartialView("PartialDelete", attachment);
        }

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Attachment attachment = db.Attachments.Find(id);
            if (attachment.ApplicantId != null)
            {
                Applicant.Models.Applicant applicant=db.Applicants.Find(attachment.ApplicantId);
                db.Attachments.Remove(attachment);
                db.SaveChanges();
                return PartialView("PartialList", applicant.Attachments.ToList());
            }
            else if(attachment.HistoryId!=null)
            {
                History history = db.Histories.Find(attachment.HistoryId);
                db.Attachments.Remove(attachment);
                db.SaveChanges();
                return PartialView("PartialListToHistory", history.Attachments.ToList());
            }
            else
            {
                return HttpNotFound();
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
