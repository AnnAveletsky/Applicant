using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class Applicant
    {
        [Key]
        public int AplicantID { get; set; }

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
        public GenderType Gender { get; set; }

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
            DataType (DataType.Url),
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
            MaxLength(500),
            Display(Name = "Комментарии")
        ]
        public string HistoryComments { get; set; }

        [
            DataType(DataType.Currency),
            Range(1, 1000000, ErrorMessage = "Необходимо установить от 1 до 1 000 000 руб."),
            Display(Name = "Зарплата")
        ]
        public decimal Salary { get; set; }

        public virtual ICollection<Attachment> Attachment { get; set; }
        public virtual ICollection<History> History { get; set; } 
    }

    public enum GenderType
    {
        Мужской,
        Женский
    }
}