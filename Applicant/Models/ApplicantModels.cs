﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ApplicantClassLibrary;

namespace Applicant.Models
{
    public class ApplicantFields
    {
        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "Фамилия")
        ]
        public string FirstName { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "Имя")
        ]
        public string MiddleName { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "Отчество")
        ]
        public string LastName { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            Display(Name = "Пол")
        ]
        public Gender Gender { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            DataType(DataType.Date),
            DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true),
            Display(Name = "Дата рождения")
        ]
        public DateTime Birthday { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            Display(Name = "Город проживания")
        ]
        public string Residence { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            EmailAddressAttribute,
            Display(Name = "E-mail")
        ]
        public string Email { get; set; }

        [
            Display(Name = "Skype")
        ]
        public string Skype { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "Личный репозиторий")
        ]
        public string GitHub { get; set; }

        [
            DataType(DataType.Url),
            Display(Name = "LinkedIn")
        ]
        public string Linkedin { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            Display(Name = "Телефон"),
            PhoneAttribute
        ]
        public string Phone { get; set; }

        [
            DataType(DataType.MultilineText),
            MaxLength(500),
            Display(Name = "Комментарии")
        ]
        public string Comments { get; set; }

        [
            DataType(DataType.Currency),
            Range(1.0, 1000000.0, ErrorMessage = "Необходимо установить от 1 до 1 000 000 руб."),
            Display(Name = "Зарплата")
        ]
        public int Salary { get; set; }
        public ApplicantFields() { }
        public ApplicantFields(string firstName, string lastName, string middleName,
            Gender gender, DateTime birthday, string residence, string email,
            string skype, string gitHub, string linkedin,
            string phone, string comments, int salary)
        {
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            Birthday = birthday;
            Gender = gender;
            Residence = residence;
            Email = email;
            Skype = skype;
            GitHub = gitHub;
            Linkedin = linkedin;
            Phone = phone;
            Comments = comments;
            Salary = salary;
        }
    }
    public class ApplicantEdit : ApplicantFields
    {
        [Key]
        public int ApplicantId { get; set; }
        public ApplicantEdit() { }
        public ApplicantEdit(ApplicantFields applicantFields)
            : base(applicantFields.FirstName, applicantFields.LastName, applicantFields.MiddleName,
                applicantFields.Gender, applicantFields.Birthday, applicantFields.Residence, applicantFields.Email,
                applicantFields.Skype, applicantFields.GitHub, applicantFields.Linkedin, applicantFields.Phone,
                applicantFields.Comments, applicantFields.Salary) { }
        public void Edit(ApplicantFields applicantFields)
        {
            FirstName = applicantFields.FirstName;
            MiddleName = applicantFields.MiddleName;
            LastName = applicantFields.LastName;
            Birthday = applicantFields.Birthday;
            Gender = applicantFields.Gender;
            Residence = applicantFields.Residence;
            Email = applicantFields.Email;
            Skype = applicantFields.Skype;
            GitHub = applicantFields.GitHub;
            Linkedin = applicantFields.Linkedin;
            Phone = applicantFields.Phone;
            Comments = applicantFields.Comments;
            Salary = applicantFields.Salary;
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