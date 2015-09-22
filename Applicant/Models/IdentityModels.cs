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
        public IQueryable<Applicant> Sort(SortSearch sortSearch, IQueryable<Applicant> applicants)
        {
            if (sortSearch.PoleSort == PoleSort.Фамилия)
            {
                if (sortSearch.OrderSort == OrderSort.Прямой)
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.FirstName
                                  select applicant);
                }
                else
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.FirstName descending
                                  select applicant);
                }

            }
            else if (sortSearch.PoleSort == PoleSort.Имя)
            {
                if (sortSearch.OrderSort == OrderSort.Прямой)
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.MiddleName
                                  select applicant);
                }
                else
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.MiddleName descending
                                  select applicant);
                }
            }
            else if (sortSearch.PoleSort == PoleSort.Отчество)
            {
                if (sortSearch.OrderSort == OrderSort.Прямой)
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.LastName
                                  select applicant);
                }
                else
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.LastName descending
                                  select applicant);
                }
            }
            else if (sortSearch.PoleSort == PoleSort.Возраст)
            {
                if (sortSearch.OrderSort == OrderSort.Прямой)
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.Birthday
                                  select applicant);
                }
                else
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.Birthday descending
                                  select applicant);
                }
            }
            else if (sortSearch.PoleSort == PoleSort.Зарплата)
            {
                if (sortSearch.OrderSort == OrderSort.Прямой)
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.Salary
                                  select applicant);
                }
                else
                {
                    applicants = (from applicant in applicants
                                  orderby applicant.Salary descending
                                  select applicant);
                }
            }
            else
            {
                throw new Exception("Ошибка запроса");
            }
            return applicants;
        }
        public IQueryable<Applicant> ToPage(Page page, IQueryable<Applicant> applicants)
        {
            return applicants.Skip((page.NowPage - 1) * page.SortSearch.CountElementsInPage)
                   .Take(page.SortSearch.CountElementsInPage);
        }

        public int CountApplicant(SortSearch sortSearch)
        {
            var applicants = from applicant in Applicants
                             select applicant;
            return Search(sortSearch, applicants).Count();
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