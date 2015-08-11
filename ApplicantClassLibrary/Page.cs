using System;
using System.Collections.Generic;
using System.Linq;
using ApplicantClassLibrary;
namespace ApplicantClassLibrary
{
    public class Page
    {
        public int NowPage { get; set; }
        public int CountPages { get; set; }
        public int CountElementsInPage { get; set; }
        public string Search { get; set; }
        public string PoleSort { get; set; }
        public OrderSort OrderSort { get; set; }
        public bool Next { get; set; }
        public bool Back { get; set; }
        public Page(int countApplicants, 
            int? page = 1, int? countElementsInPage = 5, 
            string poleSort = "FirstName", OrderSort orderSort=OrderSort.Прямой, 
            string search = "")
        {
            CountElementsInPage = (int)countElementsInPage;
            NowPage = (int)page;
            CountPages = countApplicants / CountElementsInPage + 1;
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
            PoleSort = poleSort;
            OrderSort = orderSort;
            Search = search;
        }
    }
}