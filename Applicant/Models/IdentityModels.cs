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

namespace Applicant.Models
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
            : base("DefaultConnection", throwIfV1Schema: false) { }
        public System.Data.Entity.DbSet<Applicant> Applicants { get; set; }

        public System.Data.Entity.DbSet<Attachment> Attachments { get; set; }

        public System.Data.Entity.DbSet<History> Histories { get; set; }

        public System.Data.Entity.DbSet<Tag> Tags { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public Page Page(int? page, int? countElements,string search, string pole = "FirstName", 
            OrderSort orderSort = OrderSort.Прямой)
        {
            return new Page(Applicants.Local.Count, page, countElements, pole, orderSort, search);
        }
        public List<Applicant> ApplicantsInPage(Page page)
        {
            var applicants = from applicant in Applicants
                             select applicant;
            if (page.Search != "")
            {
                applicants = from applicant in applicants
                             where applicant.FirstName == page.Search ||
                                 applicant.LastName == page.Search ||
                                 applicant.MiddleName == page.Search
                             select applicant;
            }
            if (page.PoleSort == "FirstName" && page.OrderSort == OrderSort.Прямой)
            {
                applicants = (from applicant in applicants
                              orderby applicant.FirstName
                              select applicant);
            }
            else if (page.PoleSort == "FirstName" && page.OrderSort == OrderSort.Обратный)
            {
                applicants = (from applicant in applicants
                              orderby applicant.FirstName descending
                              select applicant);
            }
            else if (page.PoleSort == "MiddleName" && page.OrderSort == OrderSort.Прямой)
            {
                applicants = (from applicant in applicants
                              orderby applicant.MiddleName
                              select applicant);
            }
            else if (page.PoleSort == "MiddleName" && page.OrderSort == OrderSort.Обратный)
            {
                applicants = (from applicant in applicants
                              orderby applicant.MiddleName descending
                              select applicant);
            }
            else if (page.PoleSort == "LastName" && page.OrderSort == OrderSort.Прямой)
            {
                applicants = (from applicant in applicants
                              orderby applicant.LastName
                              select applicant);
            }
            else if (page.PoleSort == "LastName" && page.OrderSort == OrderSort.Обратный)
            {
                applicants = (from applicant in applicants
                              orderby applicant.LastName descending
                              select applicant);
            }
            else
            {
                throw new Exception("Ошибка запроса");
            }
            applicants = applicants.Skip((page.NowPage-1) * page.CountElementsInPage)
                .Take((page.NowPage-1) * page.CountElementsInPage + page.CountElementsInPage);
            return applicants.ToList();
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

        public Attachment AddAttachmentInApplicant(HttpPostedFileBase filedata,int? applicantId)
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
    }
}