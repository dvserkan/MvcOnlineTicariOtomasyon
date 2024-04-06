using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        Context c = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult Loginadd()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult Loginadd(Cariler p)
        {
            p.CariStatus = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return PartialView();
        }


        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Cariler p)
        {
            var sorgu = c.Carilers.FirstOrDefault(x => x.CariMail == p.CariMail && x.CariSifre == p.CariSifre);

            if (sorgu != null)
            {
                FormsAuthentication.SetAuthCookie(sorgu.CariMail, false);
                Session["CariMail"] = sorgu.CariMail.ToString();
                return RedirectToAction("Giris", "CariPanel");
            }
            else 
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpGet]
        public ActionResult LoginPer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LoginPer(Admin p)
        {
            var sorgu = c.Admins.FirstOrDefault(x => x.KullaniciAd == p.KullaniciAd && x.Sifre == p.Sifre);

            if (sorgu != null)
            {
                FormsAuthentication.SetAuthCookie(sorgu.KullaniciAd, false);
                Session["KullaniciAd"] = sorgu.KullaniciAd.ToString();
                return RedirectToAction("Index", "istatistik");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Login");
        }


    }
}