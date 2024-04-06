using MvcOnlineTicariOtomasyon.Models.Siniflar;
using PagedList;
using QRCoder;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KargoController : Controller
    {
        Context c = new Context();
        string guid = Guid.NewGuid().ToString();
        // GET: Kargo
        public ActionResult Index(string m, int t = 1)
        {
            ViewBag.guid = guid;
            var values = from x in c.KargoDetays select x;

            if (!string.IsNullOrEmpty(m))
            {
                values = values.Where(x => x.TakipKodu.Contains(m));
            }

            return View(values.Where(u => u.Status == true).ToList().ToPagedList(t, 10));
        }
        public ActionResult KargoTakips(string id)
        {
            var values = c.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            var deger = c.KargoDetays.Where(x => x.TakipKodu == id).Select(y => y.TakipKodu).FirstOrDefault();
            ViewBag.id = deger;

            return View(values);
        }

        [HttpGet]
        public ActionResult KargoEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KargoEkle(KargoDetay p)
        {
            p.TakipKodu = guid;
            p.Status = true;
            c.KargoDetays.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KargoTamam(int id)
        {
            var values = c.KargoDetays.Find(id);
            values.Status = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult KargoDetayEkle()
        {

            return View();
        }
        [HttpPost]
        public ActionResult KargoDetayEkle(KargoTakip p)
        {
            p.Status = true;
            c.KargoTakips.Add(p);
            c.SaveChanges();
            return RedirectToAction($"KargoTakips/{p.TakipKodu}");
        }
   
        public ActionResult Qrcode(string kod, string id)
        {
            var deger = c.KargoDetays.Where(x => x.TakipKodu == id).Select(y => y.TakipKodu).FirstOrDefault();
            ViewBag.id = deger;


            using (MemoryStream ms = new MemoryStream())
            {
                QRCodeGenerator koduret = new QRCodeGenerator();
                QRCodeGenerator.QRCode karekod = koduret.CreateQrCode(deger, QRCodeGenerator.ECCLevel.Q);
                using (Bitmap resim = karekod.GetGraphic(10))
                {
                    resim.Save(ms, ImageFormat.Png);
                    ViewBag.karekodimage = ($"data:image/png;base64,{Convert.ToBase64String(ms.ToArray())}");
                }
                return View();
            }
        }

    }
}