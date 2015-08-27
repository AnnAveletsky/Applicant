﻿using System;
using System.Collections.Generic;
using System.Linq;
using ApplicantClassLibrary;
using System.Web;
namespace ApplicantClassLibrary
{
    public class Page
    {
        //Текущая страница
        public int NowPage { get; set; }

        //Количество страниц
        public int CountPages { get; set; }

        //db.Applicants.Count() или найденых после поиска
        public int CountApplicants { get; set; }
       
        //Следующая страница
        public bool Next { get; set; }

        //Предыдущая страница
        public bool Back { get; set; }

        //Сортировка и поиск
        public SortSearch SortSearch { get; set; }

        public Page(SortSearch sortSearch, int countApplicants, int? nowPage = 1)
        {
            SortSearch = sortSearch;
            CountApplicants = countApplicants;
            NowPage = (int)nowPage;
            CountPages = countApplicants / sortSearch.CountElementsInPage + 1;
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
    }
}