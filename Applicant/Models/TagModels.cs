using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class TagFields
    {
        [Key]
        public int TagId { get; set; }
        [
            Required(ErrorMessage = "Поле должно быть установлено"),
            StringLength(50, MinimumLength = 2, ErrorMessage = "Длина строки должна быть от 2 до 50 символов"),
            Display(Name = "Тег"),
        ]
        public string TagName { get; set; }
        public TagFields() { }
        public TagFields(string tagName)
        {
            TagName = tagName;
        }
    }
    public class Tag : TagFields
    {
        public virtual ICollection<Applicant> Applicants { get; set; }
        public Tag(TagFields tagFields)
            : base(tagFields.TagName)
        {
        }
        public Tag(TagFields tagFields, ICollection<Applicant> applicants)
            : base(tagFields.TagName)
        {
            Applicants = applicants;
        }
        public Tag() : base() { }
    }
    public class TagCreate : TagFields
    {
        [Required]
        public int ApplicantId { get; set; }
        public Applicant Applicant { get; set; }
        public TagCreate() : base() { }
        public TagCreate(TagFields tagFields, int applicantId)
            : base(tagFields.TagName)
        {
            ApplicantId = applicantId;
        }
    }
    public class TagList
    {
        public  IEnumerable<Tag> Tags { get; set; }
        public int ApplicantId { get; set; }
    }

}