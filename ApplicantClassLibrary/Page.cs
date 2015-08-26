using System;
using System.Collections.Generic;
using System.Linq;
using ApplicantClassLibrary;
namespace ApplicantClassLibrary
{
    public class Page
    {
        //Текущая страница
        public int NowPage { get; set; }

        //Количество страниц
        public int CountPages { get; set; }

        //Количество выводимых элементов на страницу
        public int CountElementsInPage { get; set; }

        //Строка поиска
        public string Search { get; set; }

        //Поле сортировки
        public string PoleSort { get; set; }

        //Метод сортировки
        public OrderSort OrderSort { get; set; }

        //Следующая страница
        public bool Next { get; set; }

        //Предыдущая страница
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