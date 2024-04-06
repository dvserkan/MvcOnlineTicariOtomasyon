using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class CariPanelController : Controller
    {

        // GET: CariPanel
        Context c = new Context();
        [HttpGet]
        public ActionResult Index()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x => x.CariMail == mail);
            ViewBag.d = degerler;
            return View(degerler);
        }
        [HttpPost]
        public ActionResult Index(Cariler p)
        {
            var values = c.Carilers.Find(p.Cariid);
            values.CariAd = p.CariAd;
            values.CariSoyad = p.CariSoyad;
            values.CariUnvan = p.CariUnvan;
            values.CariSifre = p.CariSifre;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Siparislerim()
        {
            var mail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x => x.CariMail == mail.ToString()).Select(y => y.Cariid).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.Cariid == id).ToList();
            return View(degerler);
        }
        public ActionResult GelenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Alıcı == mail).OrderByDescending(x => x.MesajID).ToList();

            var gelensayisi = c.Mesajlars.Count(y => y.Alıcı == mail).ToString();
            ViewBag.count = gelensayisi;

            var gidensayisi = c.Mesajlars.Count(y => y.Gönderici == mail).ToString();
            ViewBag.count1 = gidensayisi;

            return View(mesajlar);
        }
        public ActionResult GidenMesajlar()
        {
            var mail = (string)Session["CariMail"];
            var mesajlar = c.Mesajlars.Where(x => x.Gönderici == mail).OrderByDescending(x => x.MesajID).ToList();

            var gidensayisi = c.Mesajlars.Count(y => y.Gönderici == mail).ToString();
            ViewBag.count1 = gidensayisi;

            var gelensayisi = c.Mesajlars.Count(y => y.Alıcı == mail).ToString();
            ViewBag.count = gelensayisi;

            return View(mesajlar);
        }

        public ActionResult MesajDetay(int id)
        {
            var degerler = c.Mesajlars.Where(x => x.MesajID == id).ToList();

            var mail = (string)Session["CariMail"];
            var gidensayisi = c.Mesajlars.Count(y => y.Gönderici == mail).ToString();
            ViewBag.count1 = gidensayisi;

            var gelensayisi = c.Mesajlars.Count(y => y.Alıcı == mail).ToString();
            ViewBag.count = gelensayisi;

            return View(degerler);
        }

        [HttpGet]
        public ActionResult YeniMesaj()
        {
            var mail = (string)Session["CariMail"];
            var gidensayisi = c.Mesajlars.Count(y => y.Gönderici == mail).ToString();
            ViewBag.count1 = gidensayisi;

            var gelensayisi = c.Mesajlars.Count(y => y.Alıcı == mail).ToString();
            ViewBag.count = gelensayisi;
            return View();
        }

        [HttpPost]
        public ActionResult YeniMesaj(Mesajlar p)
        {
            var mail = (string)Session["CariMail"];
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            p.Gönderici = mail;
            c.Mesajlars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTakip(string m, int t = 1)
        {
            var values = from x in c.KargoDetays select x;
            values = values.Where(x => x.TakipKodu.Contains(m));
            return View(values.Where(u => u.Status == true).ToList().ToPagedList(t, 10)); 
        }
        public ActionResult KargoDetay(string id)
        {
            var values = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            var deger = c.KargoDetays.Where(x => x.TakipKodu == id).Select(y => y.TakipKodu).FirstOrDefault();
            ViewBag.id = deger;

            return View(values);
        }
        public ActionResult Giris()
        {
            var mail = (string)Session["CariMail"];
            var degerler = c.Mesajlars.Where(x => x.Alıcı == mail).ToList();

            var mailid = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.Cariid).FirstOrDefault();
            ViewBag.mailid = mailid;

            var ünvan = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariUnvan).FirstOrDefault();
            ViewBag.ünvan = ünvan;

            var sehir = c.Carilers.Where(x => x.CariMail == mail).Select(y => y.CariSehir).FirstOrDefault();
            ViewBag.sehir = sehir;

            var toplam = c.SatisHarekets.Where(x => x.Cariid == mailid).Count();
            ViewBag.toplam = toplam;

            var ödenen = c.SatisHarekets.Where(x=> x.Cariid == mailid).Sum(y=> y.ToplamTutar).ToString("n2");
            ViewBag.ödenen = ödenen;

            var ürünsayisi = c.SatisHarekets.Where(x => x.Cariid == mailid).Sum(y => y.Adet).ToString();
            ViewBag.ürünsayisi = ürünsayisi;

            var adsoyad = c.Carilers.Where(x=> x.CariMail == mail).Select(y=> y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.adsoyad = adsoyad;

            var mailler = c.Carilers.Where ( x=> x.CariMail == mail).Select(y=> y.CariMail).FirstOrDefault();
            ViewBag.mail = mailler;

            ViewBag.d = degerler;
            return View(degerler);
        }
        public PartialViewResult Partial2()
        {
            var veriler = c.Mesajlars.Where(x => x.Gönderici == "admin").ToList();

            return PartialView(veriler);
        }
    }
}
