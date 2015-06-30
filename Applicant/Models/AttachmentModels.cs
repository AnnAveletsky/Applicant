using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class AttachmentModel
    {
        [Key]
        public int AttachmentId { get; set; }
        public int ApplicantId { get; set; }
        public int Name { get; set; }
        public DateTime DateTimeAdd { get; set; }
        public int ApplicantIdAdd { get; set; }
        public DateTime DataTimeDelete { get; set; }
        public int ApplicantIdDelete { get; set; }
        
    }
    public class AttachmentDateTimeChanges
    {
        [Key]
        public int AttachmentDateTimeChangesId { get; set; }
        public int AttachmentId { get; set; }
        public int ApplicantIdChange { get; set; }
    }
}