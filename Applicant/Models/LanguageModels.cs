using ApplicantClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicantWeb.Models
{
    public class Language
    {
        public Lang CurrentLang { get; set; }
        public static Lang ToLang(string str)
        {
            Lang lang=new Lang();
            return (Lang)Enum.Parse(lang.GetType(), str);
        }
    }
}