using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "Тег"), 
        ]
        public string TagName { get; set; }
    }
}