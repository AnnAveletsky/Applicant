using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ApplicantClassLibrary;
using System.Data.Entity;
using System.Resources;
using System.Globalization;
namespace ApplicantWeb.Models
{
    public class ApplicantFields
    {
        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "FirstName", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant)),
        ]
        public string FirstName { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "MiddleName", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant)),
        ]
        public string MiddleName { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "BaseProfileJob", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string BaseProfileJob { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "Gender", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public int Gender { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true),
            Display(Name = "Birthday", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public DateTime Birthday { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "City", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string City { get; set; }

        [
            Display(Name = "TypeSalary", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant)),
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
        ]
        public int TypeSalary{ get; set; }
        [
            Range(1, 1000000, ErrorMessage = "Необходимо установить от 1 до 1 000 000"),
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "Salary", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public int Salary { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "TypeMoney", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public int TypeMoney { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "TypeСooperation", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public int TypeСooperation { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "TypeEmployment", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public int TypeEmployment { get; set; }

        [
            DataType(DataType.MultilineText),
            MaxLength(500),
            Display(Name = "Comments", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string Comments { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            EmailAddressAttribute,
            Display(Name = "Email", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant)),
            RegularExpression(@"[0-9a-zA-Z.-]+[@][0-9a-zA-Z]+[.][0-9a-zA-Z]+", ErrorMessage = "Некорректный адрес"),
            DataType(DataType.EmailAddress)
        ]
        public string Email { get; set; }

        [
            Display(Name = "Skype", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string Skype { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "Phone", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant)),
            PhoneAttribute
        ]
        public string Phone { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "WebSite", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]

        public string WebSite { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "Repository", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string Repository { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "Linkedin", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string Linkedin { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "Facebook", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string Facebook { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "VKontakte", ResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant))
        ]
        public string VKontakte { get; set; }

        public byte[] Photo { get; set; }

        public ApplicantFields() { }
        public ApplicantFields(string firstName, string middleName, 
            string  baseProfileJob,
            int gender, DateTime birthday, string city, string email,
            string skype, string personalRepository, string linkedin,
            string phone, string comments, 
            int salary,int typeSalary,int typeMoney,
            int typeСooperation, int typeEmployment,
            string website, string facebook, string vkontakte)
        {
            FirstName = firstName;
            MiddleName = middleName;
            BaseProfileJob = baseProfileJob;
            Birthday = birthday;
            Gender = gender;
            City = city;
            Email = email;
            Skype = skype;
            Repository = personalRepository;
            Linkedin = linkedin;
            Phone = phone;
            Comments = comments;
            Salary = salary;
            TypeSalary= typeSalary;
            TypeMoney = typeMoney;
            TypeСooperation = typeСooperation;
            TypeEmployment = typeEmployment;
            WebSite = website;
            Facebook = facebook;
            VKontakte = vkontakte;
        }
    }
    public class ApplicantEdit : ApplicantFields
    {
        [Key]
        public int ApplicantId { get; set; }
        public ApplicantEdit() { }
        public ApplicantEdit(ApplicantFields applicantFields)
            : base(applicantFields.FirstName, applicantFields.MiddleName,applicantFields.BaseProfileJob,
                applicantFields.Gender, applicantFields.Birthday, applicantFields.City, applicantFields.Email,
                applicantFields.Skype, applicantFields.Repository, applicantFields.Linkedin, applicantFields.Phone,
                applicantFields.Comments,
            applicantFields.Salary, applicantFields.TypeSalary,applicantFields.TypeMoney,
            applicantFields.TypeСooperation, applicantFields.TypeEmployment,
            applicantFields.WebSite, applicantFields.Facebook, applicantFields.VKontakte) { }
        public void Edit(ApplicantFields applicantFields)
        {
            FirstName = applicantFields.FirstName;
            MiddleName = applicantFields.MiddleName;
            BaseProfileJob = applicantFields.BaseProfileJob;
            Birthday = applicantFields.Birthday;
            Gender = applicantFields.Gender;
            City = applicantFields.City;
            Email = applicantFields.Email;
            Skype = applicantFields.Skype;
            Repository = applicantFields.Repository;
            Linkedin = applicantFields.Linkedin;
            Phone = applicantFields.Phone;
            Comments = applicantFields.Comments;
            Salary = applicantFields.Salary;
            TypeSalary = applicantFields.TypeSalary;
            TypeMoney = applicantFields.TypeMoney;
            TypeСooperation = applicantFields.TypeСooperation;
            TypeEmployment = applicantFields.TypeEmployment;
            WebSite = applicantFields.WebSite;
            Facebook = applicantFields.Facebook;
            VKontakte = applicantFields.VKontakte;
        }
    }
    public class Applicant : ApplicantEdit
    {
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ICollection<Tag> Tags { get; set; }
        public virtual ICollection<History> Histories { get; set; }
        public Applicant() { }
        public Applicant(ApplicantFields applicantFields)
            : base(applicantFields)
        {
            Attachments = new List<Attachment>();
            Tags = new List<Tag>();
            Histories = new List<History>();
        }
        public Applicant(ApplicantFields applicantFields, ICollection<Tag> tags,
            ICollection<History> histories, ICollection<Attachment> attachments)
            : base(applicantFields)
        {
            Tags = tags;
            Histories = histories;
            Attachments = attachments;
        }
    }
}