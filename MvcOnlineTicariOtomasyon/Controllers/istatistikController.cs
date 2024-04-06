using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class istatistikController : Controller
    {
        Context c = new Context();
        Class1 ca = new Class1();
        // GET: istatistik
        public ActionResult Index()
        {
            var deger1 = c.Carilers.Count().ToString();
            ViewBag.d1 = deger1;

            var deger2 = c.Uruns.Count().ToString();
            ViewBag.d2 = deger2;

            var deger3 = c.Personels.Count().ToString();
            ViewBag.d3 = deger3;

            var deger4 = c.Kategoris.Count().ToString();
            ViewBag.d4 = deger4;

            var deger5 = c.Uruns.Sum(x => x.Stok).ToString();
            ViewBag.d5 = deger5;

            var deger6 = c.Uruns.Select(x => x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;

            var deger7 = c.Uruns.Where(x => x.Stok <= 10).Count().ToString();
            ViewBag.d7 = deger7;

            var deger8 = (from x in c.Uruns orderby x.SatisFiyat descending select x.UrunAd).FirstOrDefault();
            ViewBag.d8 = deger8;

            var deger9 = (from x in c.Uruns orderby x.SatisFiyat ascending select x.UrunAd).FirstOrDefault();
            ViewBag.d9 = deger9;

            var deger10 = c.Uruns.Count(x => x.UrunAd == "Buz Dolabı").ToString();
            ViewBag.d10 = deger10;

            var deger11 = c.Uruns.Count(x => x.UrunAd == "Abra A7 V14.5.4 Intel Core i5").ToString();
            ViewBag.d11 = deger11;

            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            var deger13 = (c.Uruns.Where(u => u.Urunid == (c.SatisHarekets.GroupBy(x => x.Urunid).OrderByDescending(y => y.Count()).Select(t => t.Key).FirstOrDefault()))).Select(m => m.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;

            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString("n2") + " ₺";
            ViewBag.d14 = deger14;

            DateTime bugün = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugün).ToString();
            ViewBag.d15 = deger15;

            try
            {
                var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugün).Sum(y => y.ToplamTutar).ToString("n2") + " ₺";
                ViewBag.d16 = deger16;
            }
            catch (Exception)
            {

                ViewBag.d16 = "0.00 ₺";
            }

            //var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugün).Sum(y => (decimal?)y.ToplamTutar).ToString();
            //ViewBag.d16 = deger16;


            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = (from x in c.Uruns
                         group x by x.Kategori.KategoriAd into b
                         select new SinifKategori
                         {
                             KatAd = b.Key,
                             Count = b.Count()
                         });


            return View(sorgu);
        }
        public PartialViewResult KolayTablolars()
        {
            var sorgu = from x in c.Carilers
                        group x by x.CariSehir into g
                        select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return PartialView(sorgu.ToList());
        }
        public PartialViewResult KolayTablolars1()
        {
            var sorgu2 = from x in c.Personels
                         group x by x.Departman.DepartmanAd into g
                         select new SinifPersonel
                         {
                             Departman = g.Key,
                             Adet = g.Count()
                         };


            return PartialView(sorgu2.ToList());
        }
        public PartialViewResult KolayTablolars2(int p = 1)
        {
            var sorgu3 = c.Carilers.ToList().ToPagedList(p, 10);
            return PartialView(sorgu3);
        }
        public PartialViewResult KolayTablolars3(int p = 1)
        {
            var sorgu3 = c.Uruns.ToList().ToPagedList(p, 5);
            return PartialView(sorgu3);
        }
        public PartialViewResult KolayTablolars4()
        {
            var sorgu3 = from x in c.Uruns
                         group x by x.Marka into g
                         select new SinifMarka
                         {
                             Marka = g.Key,
                             Adet = g.Count()
                         };

            return PartialView(sorgu3.ToList());
        }
    }
}