using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class History
    {
        [Key]
        public int HistoryId { get; set; }

        [
            ScaffoldColumn(false),
            ForeignKey("Applicant")
        ]
        public int? ApplicantId { get; set; }

        [Display(Name = "Соискатель")]
        public Applicant Applicant { get; set; }

        [
            DataType(DataType.Date),
            Display(Name = "Дата прохождения")
        ]
        public DateTime CommunicationDate { get; set; }

        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            Display(Name = "Тип встречи")
        ]
        public TypeHistory TypeCommunication { get; set; }

        [
            Display(Name = "Коментарии"),
            MaxLength(500)
        ]
        public string HistoryComments { get; set; }
        public virtual ICollection<Attachment> Attachments { get; set; }
    }
    public enum TypeHistory
    {
        Interview,
        Skype
    }
}