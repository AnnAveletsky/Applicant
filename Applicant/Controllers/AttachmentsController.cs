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
            if (Request.IsAjaxRequest())
            {
                return PartialView("PartialList",db.Applicants.Find(applicantId).Attachments.ToList());
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        // GET: Attachments/Load
        public JsonResult Load(int applicantId ,IEnumerable<HttpPostedFileBase> file_data)
        {
            foreach (var i in file_data.ToList())
            {
                db.AddAttachmentInApplicant(applicantId, i);
            }
            return Json("");
        }
        // POST: Attachments/Download
        public FileResult Download(int id, int applicantId)
        {
            Attachment attach=db.Attachments.Find(id);
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
            return PartialView("PartialList", db.Attachments.Where(p => p.ApplicantId == applicantId));
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
