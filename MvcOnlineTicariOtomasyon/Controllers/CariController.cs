using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        Context c = new Context();
        // GET: Cari
        public ActionResult Index()
        {
            var values = c.Carilers.Where(x => x.CariStatus == true).ToList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddCari()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCari(Cariler p)
        {
            p.CariStatus = true;
            c.Carilers.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCari(int id)
        {
            var values = c.Carilers.Find(id);
            values.CariStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditCari(int id)
        {
            var values = c.Carilers.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditCari(Cariler p)
        {
            if (!ModelState.IsValid)
            {
                return View("EditCari");
            }
            var values = c.Carilers.Find(p.Cariid);
            values.CariAd = p.CariAd;
            values.CariSoyad = p.CariSoyad;
            values.CariMail = p.CariMail;
            values.CariSehir = p.CariSehir;
            values.CariUnvan = p.CariUnvan;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariDetay(int id)
        {
            var values = c.SatisHarekets.Where(x => x.Cariid == id).ToList();

            var valuesname = c.Carilers.Where(x => x.Cariid == id).Select(y => y.CariAd + " " + y.CariSoyad + " " + " " + " " + " / " + " Cari Ünvanı : " + y.CariUnvan).FirstOrDefault();
            ViewBag.name = valuesname;
            return View(values);
        }
    }
}