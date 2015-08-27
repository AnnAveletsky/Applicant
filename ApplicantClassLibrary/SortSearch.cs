using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicantClassLibrary
{
    public class SortSearch
    {
        //Строка поиска
        public string Search { get; set; }

        //Поле сортировки
        public PoleSort PoleSort { get; set; }

        //Метод сортировки
        public OrderSort OrderSort { get; set; }

        //Количество выводимых элементов на страницу
        public int CountElementsInPage { get; set; }
        public SortSearch() { }
        public SortSearch(String search="",
            PoleSort poleSort=PoleSort.Фамилия,
            OrderSort orderSort=OrderSort.Прямой,
            int? countElementsInPage=5)
        {
            Search = search;
            PoleSort = poleSort;
            OrderSort = orderSort;
            CountElementsInPage = (int)countElementsInPage;
        }
    }
}
