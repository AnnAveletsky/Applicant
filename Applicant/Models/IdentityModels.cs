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
        public List<Applicant> ApplicantsInPage(Page page)
        {
            var applicants = from applicant in Applicants
                             select applicant;
            applicants = Search(page.SortSearch, applicants);
            applicants = Sort(page.SortSearch, applicants);
            applicants = ToPage(page, applicants);
            return applicants.ToList();
        }
        public IQueryable<Applicant> Search(SortSearch sortSearch, IQueryable<Applicant> applicants)
        {
            if (sortSearch.Search != "" && sortSearch.Search != null)
            {
                return from applicant in applicants
                       where applicant.FirstName == sortSearch.Search ||
                           applicant.LastName == sortSearch.Search ||
                           applicant.MiddleName == sortSearch.Search
                       select applicant;
            }
            else
            {
                return applicants;
            }
        }
        public IQueryable<Applicant> Sort(SortSearch sortSearch,IQueryable<Applicant> applicants)
        {
            if (sortSearch.PoleSort == PoleSort.Фамилия && sortSearch.OrderSort == OrderSort.Прямой)
            {
                return (from applicant in applicants
                              orderby applicant.FirstName
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Фамилия && sortSearch.OrderSort == OrderSort.Обратный)
            {
                return (from applicant in applicants
                              orderby applicant.FirstName descending
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Имя && sortSearch.OrderSort == OrderSort.Прямой)
            {
                return (from applicant in applicants
                              orderby applicant.MiddleName
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Имя && sortSearch.OrderSort == OrderSort.Обратный)
            {
                return (from applicant in applicants
                              orderby applicant.MiddleName descending
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Отчество && sortSearch.OrderSort == OrderSort.Прямой)
            {
                return (from applicant in applicants
                              orderby applicant.LastName
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Отчество && sortSearch.OrderSort == OrderSort.Обратный)
            {
                return (from applicant in applicants
                              orderby applicant.LastName descending
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Возраст && sortSearch.OrderSort == OrderSort.Прямой)
            {
                return (from applicant in applicants
                              orderby applicant.Birthday
                              select applicant);
            }
            else if (sortSearch.PoleSort == PoleSort.Возраст && sortSearch.OrderSort == OrderSort.Обратный)
            {
                return (from applicant in applicants
                              orderby applicant.Birthday descending
                              select applicant);
            }
            else
            {
                throw new Exception("Ошибка запроса");
            }
        }
        public IQueryable<Applicant> ToPage(Page page, IQueryable<Applicant> applicants)
        {
            return applicants.Skip((page.NowPage - 1) * page.SortSearch.CountElementsInPage)
                .Take((page.NowPage - 1) * page.SortSearch.CountElementsInPage + page.SortSearch.CountElementsInPage);
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