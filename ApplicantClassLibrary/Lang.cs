using System;
using System.Collections;
using System.Collections.Generic;

namespace ApplicantClassLibrary
{
    public  enum Lang
    {
        English = 0, Русский = 1
    }
    public enum Culture
    {
        en = 0, ru = 1
    }
    public static class LangCulture
    {
        public static SortedList<Lang, Culture> LangToCulture
        {
            get
            {
                SortedList<Lang, Culture> sl = new SortedList<Lang, Culture>();
                sl.Add(Lang.English, Culture.en);
                sl.Add(Lang.Русский, Culture.ru);
                return sl;
            }
            private set
            {
                LangToCulture=value;
            }
        }
    }
    public static class CultureLang
    {
        public static SortedList<Culture, Lang> CultureToLang
        {
            get
            { 
                SortedList<Culture,Lang> sl=new SortedList<Culture,Lang>();
                sl.Add(Culture.en, Lang.English);
                sl.Add(Culture.ru, Lang.Русский);
                return sl;
             }
            private set 
            { 
                CultureToLang = value; 
            }
        }
        public static SortedList<string, Culture> StringToCulture 
        {
            get
            {
                SortedList<string, Culture> sl = new SortedList<string, Culture>();
                sl.Add("en", Culture.en);
                sl.Add("ru", Culture.ru);
                return sl;
            }
            private set 
            { 
                StringToCulture = value; 
            }
        }
    }
}
