using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class FaturalarController : Controller
    {
        Context c = new Context();
        // GET: Faturalar
        public ActionResult Index()
        {
            Class3 cs = new Class3();

            cs.deger1 = c.Faturalars.ToList();
            cs.deger2 = c.FaturaKalems.ToList();

            return View(cs);
        }

        public ActionResult FaturaKaydet(string FaturaSeriNo, string FaturaSıraNo, DateTime Tarih, string VergiDairesi,
            string Saat, string TeslimEden, string TeslimAlan, string Toplam, FaturaKalem[] kalemler)
        {
            Faturalar f = new Faturalar();
            f.FaturaSeriNo = FaturaSeriNo;
            f.FaturaSıraNo = FaturaSıraNo;
            f.Tarih = Tarih;
            f.VergiDairesi = VergiDairesi;
            f.Saat = Saat;
            f.TeslimEden = TeslimEden;
            f.TeslimAlan = TeslimAlan;
            //f.Toplam = Convert.ToInt32(Toplam);
            c.Faturalars.Add(f);
            foreach (var x in kalemler)
            {
                FaturaKalem fk = new FaturaKalem();
                fk.Aciklama = x.Aciklama;
                fk.BirimFiyat = x.BirimFiyat;
                fk.Faturaid = x.FaturaKalemid;
                fk.Miktar = x.Miktar;
                fk.Tutar = x.Tutar;
                c.FaturaKalems.Add(fk);
            }
            c.SaveChanges();
            return Json("İşlem Başarılı", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AddFatura()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddFatura(Faturalar p)
        {
            c.Faturalars.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditFatura(int id)
        {
            var values = c.Faturalars.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditFatura(Faturalar p)
        {
            var values = c.Faturalars.Find(p.Faturaid);
            values.FaturaSeriNo = p.FaturaSeriNo;
            values.FaturaSıraNo = p.FaturaSıraNo;
            values.VergiDairesi = p.VergiDairesi;
            values.VergiNo = p.VergiNo;
            values.Tarih = p.Tarih;
            values.Saat = p.Saat;
            values.TeslimAlan = p.TeslimAlan;
            values.TeslimEden = p.TeslimEden;
            values.Toplam = p.Toplam;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult FaturaDetay(int id)
        {
            //DEPARTMANA GÖRE PERSONELLER
            var values = c.FaturaKalems.Where(x => x.Faturaid == id).ToList();

            var deger = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            ViewBag.id = deger;

            var seri = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.FaturaSeriNo).FirstOrDefault();
            ViewBag.seri = seri;

            var sıra = c.Faturalars.Where(x => x.Faturaid == id).Select(y => y.FaturaSıraNo).FirstOrDefault();
            ViewBag.sıra = sıra;

            return View(values);
        }
        [HttpGet]
        public ActionResult YeniKalem(int id)
        {
            var values = c.FaturaKalems.Where(x => x.Faturaid == id).Select(y => y.Faturaid).FirstOrDefault();
            ViewBag.faturaid = values;
            return View();
        }
        [HttpPost]
        public ActionResult YeniKalem(FaturaKalem p)
        {
            c.FaturaKalems.Add(p);
            c.SaveChanges();
            return RedirectToAction("FaturaDetay");
        }

      

    }
}

