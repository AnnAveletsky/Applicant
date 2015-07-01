﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class Attachment
    {
        [Key]
        public int AttachmentId { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public int? ApplicantId { get; set; }

        [Display(Name = "Соискатель")]
        public Applicant Applicant { get; set; }

        [ScaffoldColumn(false)]
        public int? HistoryId { get; set; }

        [Display(Name = "Собеседование")]
        public History History { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Attach { get; set; }
    }
}