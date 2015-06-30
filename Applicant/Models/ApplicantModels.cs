using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Applicant.Models
{
    public class ApplicantModels
    {

        [Key]
        public int AplicantID { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Comments { get; set; }
        public int Salary { get; set; }
    }
}