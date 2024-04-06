using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        Context c = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var values = c.SatisHarekets.ToList();
            return View(values);
        }
        public PartialViewResult GetEkler()
        {
            List<SelectListItem> cari =  (from x in c.Carilers.Where(x=> x.CariStatus == true).ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.CariAd + " " + x.CariSoyad,
                                             Value = x.Cariid.ToString()
                                         }).ToList();
            ViewBag.Cari = cari;

            List<SelectListItem> urun = (from x in c.Uruns.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.UrunAd,
                                             Value = x.Urunid.ToString()
                                         }).ToList();
            ViewBag.urun = urun;

            List<SelectListItem> personel = (from x in c.Personels.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.PersonelAd,
                                             Value = x.Personelid.ToString()
                                         }).ToList();
            ViewBag.personel = personel;

            return PartialView();
        }


        [HttpGet]
        public ActionResult AddSatis()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSatis(SatisHareket p)
        {
            p.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            c.SatisHarekets.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditSatis(int id)
        {
            List<SelectListItem> cari = (from x in c.Carilers.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.CariAd + " " + x.CariSoyad,
                                             Value = x.Cariid.ToString()
                                         }).ToList();
            ViewBag.Cari = cari;

            List<SelectListItem> urun = (from x in c.Uruns.ToList()
                                         select new SelectListItem
                                         {
                                             Text = x.UrunAd,
                                             Value = x.Urunid.ToString()
                                         }).ToList();
            ViewBag.urun = urun;

            List<SelectListItem> personel = (from x in c.Personels.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = x.PersonelAd,
                                                 Value = x.Personelid.ToString()
                                             }).ToList();
            ViewBag.personel = personel;


            var values = c.SatisHarekets.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditSatis(SatisHareket p)
        {
            var values = c.SatisHarekets.Find(p.Satisid);
            values.Urunid = p.Urunid;
            values.Cariid = p.Cariid;
            values.Personelid = p.Personelid;
            values.Adet = p.Adet;
            values.Fiyat = p.Fiyat;
            values.ToplamTutar = p.ToplamTutar;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}