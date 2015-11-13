using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicantWeb.Models
{
    public class AttachmentFields
    {
        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
        ]
        public string Type { get; set; }

        [
            Required(ErrorMessageResourceType = typeof(ApplicantWeb.App_LocalResources.Applicant),
                  ErrorMessageResourceName = "RequiredFieldError"),
            Display(Name = "Title", ResourceType = typeof(ApplicantWeb.App_LocalResources.Attachment))
        ]
        public string Name { get; set; }

        [ScaffoldColumn(false)]
        public byte[] Attach { get; set; }
        public AttachmentFields() : base() { }
        public AttachmentFields(string name, string type, byte[] attach)
        {
            Attach = attach;
            Name = name;
            Type = type;
        }
    }
    public class Attachment : AttachmentFields
    {
        [Key]
        public int AttachmentId { get; set; }
        [
            ForeignKey("Applicant"),
            ScaffoldColumn(false)
        ]
        public int? ApplicantId { get; set; }

        [Display(Name = "Соискатель")]
        public Applicant Applicant { get; set; }
        [
            ScaffoldColumn(false),
            ForeignKey("History")
        ]
        public int? HistoryId { get; set; }

        [Display(Name = "Собеседование")]
        public History History { get; set; }
        public Attachment() : base() { }
        public Attachment(AttachmentFields attachmentFields,int? applicantId, Applicant applicant,int? historyId, History history)
            : base(attachmentFields.Name, attachmentFields.Type, attachmentFields.Attach)
        {
            ApplicantId = applicantId;
            Applicant = Applicant;
            HistoryId = historyId;
            History = history;
        }
        public Attachment(AttachmentFields attachmentFields,int? applicantId, Applicant applicant)
            : base(attachmentFields.Name, attachmentFields.Type, attachmentFields.Attach)
        {
            ApplicantId = applicantId;
            Applicant = Applicant;
        }
        public void EditName(string name)
        {
            Name = name;
        }
    }
}