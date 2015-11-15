﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicantWeb.Models;
using System.Drawing;
using System.IO;
using ApplicantWeb.Filters;

namespace ApplicantWeb.Controllers
{
    [Culture]
    public class AttachmentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // POST: Attachments/List/
        public JsonResult List(int? id, int? idHistory)
        {
            if (idHistory == null && id != null)
            {
                var json = (from attach in db.Attachments
                            where attach.ApplicantId == id
                            select new
                            {
                                AttachmentId = attach.AttachmentId,
                                Name = attach.Name,
                                Picture = ((attach.Name.EndsWith(".png") || attach.Name.EndsWith(".jpg") ||     attach.Name.EndsWith(".jpeg")) ? 1 : 0),
                                HistoryId=attach.HistoryId
                            });
                return Json(new { data = json }, JsonRequestBehavior.AllowGet);
            }
            else if (idHistory != null && id == null)
            {
                var history = db.Histories.Find(idHistory);
                if (history != null)
                {
                    var json = (from attach in db.Attachments
                                where attach.HistoryId == idHistory
                                select new
                                {
                                    AttachmentId = attach.AttachmentId,
                                    Name = attach.Name,
                                    ApplicantId = attach.ApplicantId
                                });
                    return Json(new { data = json }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        public ActionResult PartialList(int id)
        {
            if (Request.IsAuthenticated)
            {
            return PartialView("PartialList", db.Applicants.Find(id));
             }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        public ActionResult ListToHistory(int historyId)
        {
            if (Request.IsAuthenticated)
            {
             return PartialView("PartialListToHistory", db.Histories.Find(historyId));
            }
            else
            {
                return Redirect(Url.Action("Login", "Account"));
            }
        }
        // GET: Attachments/Load
        public JsonResult Load(int applicantId ,IEnumerable<HttpPostedFileBase> file_data)
        {
            foreach (var i in file_data.ToList())
            {
                try 
                {
                    db.Attachments.Add(db.AddAttachmentInApplicant(i, applicantId));
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                    return Json("{error: 'You are not allowed to upload such a file. ('"+ except.Message+")}");
                }
            }
            return Json("");
        }
        public JsonResult LoadToHistory(int historyId, IEnumerable<HttpPostedFileBase> file_data)
        {
            foreach (var i in file_data.ToList())
            {
                try
                {
                    db.Histories.Find(historyId).Attachments.Add(db.AddAttachmentInApplicant(i, null));
                    db.SaveChanges();
                }
                catch (Exception except)
                {
                    return Json("{error: 'You are not allowed to upload such a file. ('" + except.Message + ")}");
                }
               
            }
            return Json("");
        }
        
        // POST: Attachments/Download
        public FileResult Download(int id)
        {
            Attachment attach=db.Attachments.Find(id);
            return File(attach.Attach,attach.Type,attach.Name);
        }
        public ActionResult AddToApplicant(int attachmentId)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    Attachment attachment = db.Attachments.Find(attachmentId);
                    attachment.History = db.Histories.Find(attachment.HistoryId);
                    ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(attachment.History.ApplicantId);
                    attachment.ApplicantId = applicant.ApplicantId;
                    attachment.Applicant = applicant;
                    applicant.Attachments.Add(attachment);
                    db.SaveChanges();
                    return PartialView("PartialListToHistory", attachment.History);
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
        public ActionResult DeleteToApplicant(int attachmentId)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    Attachment attachment = db.Attachments.Find(attachmentId);
                    attachment.History = db.Histories.Find(attachment.HistoryId);
                    ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(attachment.History.ApplicantId);
                    attachment.ApplicantId = null;
                    attachment.Applicant = null;
                    applicant.Attachments.Remove(attachment);
                    db.SaveChanges();
                    return PartialView("PartialList", applicant);
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
        public ActionResult DeleteToApplicantToHistory(int attachmentId)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    Attachment attachment = db.Attachments.Find(attachmentId);
                    attachment.History = db.Histories.Find(attachment.HistoryId);
                    ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(attachment.History.ApplicantId);
                    attachment.ApplicantId = null;
                    attachment.Applicant = null;
                    History history = db.Histories.Find(attachment.HistoryId);
                    applicant.Attachments.Remove(attachment);
                    db.SaveChanges();
                    return PartialView("PartialListToHistory", history);
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
        // GET: Attachments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Request.IsAuthenticated)
            {
                try
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

        // POST: Attachments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Request.IsAuthenticated)
            {
                try
                {
                    Attachment attachment = db.Attachments.Find(id);
                    if (attachment.HistoryId != null)
                    {
                        History history = db.Histories.Find(attachment.HistoryId);
                        db.Attachments.Remove(attachment);
                        db.SaveChanges();
                        return PartialView("PartialListToHistory", history);
                    }else
                    if (attachment.ApplicantId != null)
                    {
                        ApplicantWeb.Models.Applicant applicant = db.Applicants.Find(attachment.ApplicantId);
                        db.Attachments.Remove(attachment);
                        db.SaveChanges();
                        return PartialView("PartialList", applicant);
                    }
                    else
                    {
                        return HttpNotFound();
                    }
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
