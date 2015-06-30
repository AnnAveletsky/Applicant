using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace Applicant.Models
{
    public class Attachment
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public Stream Attach { get; set; }
    }
}