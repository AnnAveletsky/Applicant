using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Applicant.Filters;
using ApplicantWeb.Models;
using ApplicantClassLibrary;

namespace Applicant.Controllers
{
     [Culture]
    public class LanguagesController : Controller
    {
         [HttpPost]
         public ActionResult ChangeCulture(Language language)
         {
             string returnUrl = Request.UrlReferrer.AbsolutePath;
             
             // Сохраняем выбранную культуру в куки
             HttpCookie cookie = Request.Cookies["lang"];
             if (cookie != null)
             {
                 cookie.Value =LangCulture.LangToCulture[language.CurrentLang].ToString();   // если куки уже установлено, то обновляем значение
             }
             else
             {
                 cookie = new HttpCookie("lang");
                 cookie.HttpOnly = false;
                 cookie.Value =LangCulture.LangToCulture[language.CurrentLang].ToString();
                 cookie.Expires = DateTime.Now.AddYears(1);
             }
             Response.Cookies.Add(cookie);
             return Redirect(returnUrl);
         }
         public ActionResult Details()
         {
             HttpCookie cookie = Request.Cookies["lang"];
             if (cookie == null || cookie.Value == null)
             {
                 cookie = new HttpCookie("lang", Culture.ru.ToString());
             }
             return PartialView("Details", new Language() { CurrentLang = CultureLang.CultureToLang[CultureLang.StringToCulture[cookie.Value]] });
         }
    }
}