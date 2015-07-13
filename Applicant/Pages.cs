using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Applicant
{
    public class Page
    {
        public int NowPage { get; set; }
        public int CountPages { get; set; }
        public int CountElementsInPage { get; set; }
        public string Search{ get; set; }
        public string PoleSort{ get; set; }
        public bool Next { get; set; }
        public bool Back { get; set; }
        public IEnumerable<Applicant.Models.Applicant> Applicants { get; set; }
        public Page(IEnumerable<Applicant.Models.Applicant> applicants, string search = "",
            int? page=1,int? countElementsInPage=10,
            string poleSort="FirstName")
        {
            Applicants = applicants;
            if (search != "")
            {
                Applicants = ToSearch(search, Applicants.ToList());
            }
            Applicants = ToPage(page, countElementsInPage, poleSort, Applicants.ToList());
            CountElementsInPage = (int)countElementsInPage;
            NowPage = (int)page;
            CountPages = applicants.ToList().Count() / CountElementsInPage+1;
            if (CountPages > 1)
            {
                Back = true;
            }
            else
            {
                Back = false;
            }
            if (CountPages < CountPages)
            {
                Next = true;
            }
            else
            {
                Next = false;
            }
        }
        public List<Applicant.Models.Applicant> ToPage(int? page, int? countElement, string pole, List<Applicant.Models.Applicant> applicants)
        {
            applicants = Sort(pole, applicants).Values.ToList();
            int j = 1;
            foreach (var i in applicants.ToList())
            {
                if (j <= ((page - 1) * countElement) || j > ((page - 1) * countElement + countElement))
                {
                    applicants.Remove(i);
                }
                j++;
            }
            return applicants;
        }
        public List<Applicant.Models.Applicant> ToSearch(string search, List<Applicant.Models.Applicant> applicants)
        {
            foreach (var i in applicants.ToList())
            {
                if (String.Compare(search, i.FirstName,true) < 0 &&
                    String.Compare(search, i.MiddleName, true) < 0 &&
                    String.Compare(search, i.LastName, true) < 0)
                {
                    applicants.Remove(i);
                }
            }
            return applicants;
        }
        public SortedList<string, Applicant.Models.Applicant> Sort(string pole, List<Applicant.Models.Applicant> applicants)
        {
            SortedList<string, Applicant.Models.Applicant> sortedApplicants = new SortedList<string, Models.Applicant>();
            if (pole == "FirstName")
            {
                foreach (var i in applicants.ToList())
                {
                    sortedApplicants.Add(i.FirstName, i);
                }
            }
            else if (pole == "MiddleName")
            {
                foreach (var i in applicants.ToList())
                {
                    sortedApplicants.Add(i.MiddleName, i);
                }
            }
            else if (pole == "LastName")
            {
                foreach (var i in applicants.ToList())
                {
                    sortedApplicants.Add(i.LastName, i);
                }
            }
            else
            {
                foreach (var i in applicants.ToList())
                {
                    sortedApplicants.Add(i.FirstName, i);
                }
            }
            return sortedApplicants;
        }
        
    }
}