using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunlerController : Controller
    {
        // GET: Urunler
        Context c = new Context();

        public ActionResult Index()
        {
            var values = c.Uruns.ToList();
            return View(values);
        }
        [HttpGet]
        public PartialViewResult AddUrun()
        {
            List<SelectListItem> getlist = (from x in c.Kategoris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.KategoriAd,
                                                Value = x.KategoriID.ToString()
                                            }).ToList();
            ViewBag.Uruns = getlist;
            return PartialView();
        }
        [HttpPost]
        public ActionResult AddUrun(Urun p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.UrunGorsel = "/Image/" + dosyaadi + uzanti;
            }

            p.Durum = true;
            c.Uruns.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteUrun(int id)
        {
            var values = c.Uruns.Find(id);
            values.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");

        }
        [HttpGet]
        public ActionResult EditUrun(int id)
        {
            List<SelectListItem> getlist = (from x in c.Kategoris.ToList()
                                            select new SelectListItem
                                            {
                                                Text = x.KategoriAd,
                                                Value = x.KategoriID.ToString()
                                            }).ToList();
            ViewBag.Uruns = getlist;

            var values = c.Uruns.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditUrun(Urun p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.UrunGorsel = "/Image/" + dosyaadi + uzanti;
            }

            var values = c.Uruns.Find(p.Urunid);
            values.UrunAd = p.UrunAd;
            values.Kategoriid = p.Kategoriid;
            values.Marka = p.Marka;
            values.Stok = p.Stok;
            values.AlisFiyat = p.AlisFiyat;
            values.SatisFiyat = p.SatisFiyat;
            values.Durum = p.Durum;
            values.UrunGorsel = p.UrunGorsel;
            
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}