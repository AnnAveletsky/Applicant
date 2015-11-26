using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Linq;
using System.Collections.Generic;
using ApplicantClassLibrary;
using System.Web;
using System.Web.Mvc;

namespace ApplicantWeb.Models
{
    // Чтобы добавить данные профиля для пользователя, можно добавить дополнительные свойства в класс ApplicationUser. Дополнительные сведения см. по адресу: http://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
			//ConnectionStringHelper.GetConnectionString()
            : base("DefaultConnection", throwIfV1Schema: false) { }
        public System.Data.Entity.DbSet<Applicant> Applicants { get; set; }

        public System.Data.Entity.DbSet<Attachment> Attachments { get; set; }

        public System.Data.Entity.DbSet<History> Histories { get; set; }

        public System.Data.Entity.DbSet<Tag> Tags { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public List<Tag> ToList(int ApplicantId)
        {
            return (from tag in Applicants.Find(ApplicantId).Tags orderby tag.TagName select tag).ToList();
        }
        
        public List<Attachment> FindAttachmentsApplicant(int? applicantId)
        {
            return (from attachment in Attachments
                    where attachment.ApplicantId == applicantId
                    select attachment).ToList();
        }

        public Tag CreateTagAndAddTagInApplicant(TagCreate tagAddApplicant)
        {
            var linqTag = (from iTag in Tags
                           where iTag.TagName.ToLower().CompareTo(tagAddApplicant.TagName.ToLower()) == 0
                           select iTag);
            if (linqTag.Count() == 0)
            {
                Tag tag = new Tag(tagAddApplicant);
                tag.Applicants = new List<Applicant>();
                tag.Applicants.Add(Applicants.Find(tagAddApplicant.ApplicantId));
                Tags.Add(tag);
                SaveChanges();
                return tag;
            }
            else if (linqTag.Count() == 1)
            {
                if (linqTag.First().Applicants.Where(p => p.ApplicantId == tagAddApplicant.ApplicantId).Count() == 0)
                {
                    linqTag.First().Applicants.Add(Applicants.Find(tagAddApplicant.ApplicantId));
                    SaveChanges();
                }
                return linqTag.First();
            }
            else
            {
                throw new Exception("Ошибка добавления тега. Несколько тегов с одним названием");
            }
        }

        public Attachment AddAttachmentInApplicant(HttpPostedFileBase filedata, int? applicantId)
        {
            Attachment attach = new Attachment();
            if (applicantId != null)
            {
                attach.ApplicantId = applicantId;
                attach.Applicant = Applicants.Find(applicantId);
            }
            attach.Attach = new byte[filedata.ContentLength];
            attach.Type = filedata.ContentType;
            filedata.InputStream.Read(attach.Attach, 0, filedata.ContentLength);
            attach.Name = filedata.FileName;
            return attach;
        }
        public void DeleteAttachments(Applicant applicant)
        {
            
                var attachments = (from attachment in Attachments
                                   where attachment.ApplicantId == applicant.ApplicantId||
                                   attachment.History.ApplicantId==applicant.ApplicantId
                                   select attachment);
                
                Attachments.RemoveRange(attachments);
            
            
        }
        public void DeleteAttachments(History history)
        {
            var attachments = (from attachment in Attachments
                               where attachment.HistoryId == history.HistoryId
                               select attachment);
            Attachments.RemoveRange(attachments);
        }
        public void DeleteHistories(Applicant applicant)
        {
            var histories = (from history in Histories
                             where history.ApplicantId == applicant.ApplicantId
                             select history);
            Histories.RemoveRange(histories);
        }
        public void DeleteTag(TagCreate tagCreate)
        {
            Applicant applicant = Applicants.Find(tagCreate.ApplicantId);
            Tag tag = Tags.Find(tagCreate.TagId);
            applicant.Tags.Remove(tag);
            if (tag.Applicants.Count == 0)
            {
                Tags.Remove(tag);
            }
        }
        public void DeleteTags(Applicant applicant)
        {
            foreach(Tag tag in Tags){
                DeleteTag(new TagCreate(tag,applicant.ApplicantId));
            }
        }
    }
}