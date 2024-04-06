using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class GrafikController : Controller
    {
        // GET: Grafik
        Context c = new Context();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Index2()
        {
            var grafikciz = new Chart(600, 600);
            grafikciz.AddTitle("Kategori - Ürün Stok Sayısı").AddLegend("Stok").AddSeries("Değerler", xValue: new[] { "Mobilya", "Laptop", "Laptop Çantası" }, yValues: new[] { 85, 66, 98 }).Write();
            return File(grafikciz.ToWebImage().GetBytes(), "image/jpeg");
        }
        public ActionResult Index3()
        {
            ArrayList xvalue = new ArrayList();
            ArrayList yvalue = new ArrayList();
            var sonuclar = c.Uruns.ToList();
            sonuclar.ToList().ForEach(x => xvalue.Add(x.UrunAd));
            sonuclar.ToList().ForEach(y => yvalue.Add(y.Stok));
            var grafik = new Chart(800, 800)
                .AddTitle("Stoklar")
                .AddSeries(chartType: "Pie", name: "Stok", xValue: xvalue, yValues: yvalue);
            return File(grafik.ToWebImage().GetBytes(), "image/jpeg");

        }
        public ActionResult Index4()
        {
            return View();
        }
        public ActionResult VisualizerUrunResult()
        {
            return Json(Urunlistesi(), JsonRequestBehavior.AllowGet);
        }
        public List<sinif1> Urunlistesi()
        {
            //List<sinif1> snf = new List<sinif1>();
            //snf.Add(new sinif1()
            //{
            //    urunad = "Bilgisayar",
            //    stok = 120

            //});
            //snf.Add(new sinif1()
            //{
            //    urunad = "Laptop",
            //    stok = 150

            //});
            //return snf;

            List<sinif1> snf = new List<sinif1>();
            using (var c = new Context())
            {
                snf = c.Uruns.Select(x => new sinif1
                {
                    urunad = x.UrunAd,
                    stok = x.Stok
                }).ToList();
            }
            return snf;
        }
    }
}