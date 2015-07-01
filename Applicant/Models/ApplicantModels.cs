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

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Фамилия")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Имя")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов")]
        [Display(Name = "Отчество")]
        public string LastName { get; set; }

        [EmailAddressAttribute]
        [Display(Name = "E-mail")]
        public string Email { get; set; }

        [Display(Name = "Телефон")]
        [PhoneAttribute]
        public string Phone { get; set; }

        [MaxLength(500)]
        [Display(Name = "Комментарии")]
        public string HistoryComments { get; set; }

        [Display(Name = "Зарплата")]
        public int Salary { get; set; }

        public virtual ICollection<Attachment> Attachment { get; set; }
        public virtual ICollection<History> History { get; set; } 
    }
}