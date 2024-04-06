using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class YapilacakController : Controller
    {
        Context c = new Context();
        // GET: Yapilacak
        public ActionResult Index()
        {
            var deger = c.SatisHarekets.Count().ToString();
            ViewBag.Deger = deger;

            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            var dager2 = c.Uruns.Count().ToString();
            ViewBag.d2 = dager2;

            var deger3 = c.Kategoris.Count().ToString();
            ViewBag.d3 = deger3;

            var yapicaklar = c.Yapilacaks.OrderByDescending(x=> x.OlusturmaSaat).ToList();
            return View(yapicaklar);
        }
    }
}