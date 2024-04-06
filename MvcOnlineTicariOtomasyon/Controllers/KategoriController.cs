using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        Context c = new Context();
        // GET: Kategori
        public ActionResult Index()
        {
            var categorylist = c.Kategoris.ToList();
            return View(categorylist);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Kategori p)
        {
            c.Kategoris.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCategory(int id)
        {
            var values = c.Kategoris.Find(id);
            ViewBag.categori = values;
            return View(values);
        }
        [HttpPost]
        public ActionResult EditCategory(Kategori p)
        {
            var values = c.Kategoris.Find(p.KategoriID);
            values.KategoriAd = p.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult DeleteCategory(int id)
        {
            var values = c.Kategoris.Find(id);
            c.Kategoris.Remove(values);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Deneme()
        {
            Class2 cs = new Class2();

            cs.Kategoriler = new SelectList(c.Kategoris, "KategoriID", "KategoriAd");
            cs.Urunler = new SelectList(c.Uruns, "Urunid", "UrunAd");

            return View(cs);
        }
        public JsonResult UrunGetir(int p)
        {
            var urunlistesi = (from x in c.Uruns
                               join y in c.Kategoris
                               on x.Kategori.KategoriID equals y.KategoriID
                               where x.Kategori.KategoriID == p
                               select new
                               {
                                   Text = x.UrunAd,
                                   Value = x.Urunid.ToString()
                               }).ToList();

            return Json(urunlistesi,JsonRequestBehavior.AllowGet);

        }
    }
}