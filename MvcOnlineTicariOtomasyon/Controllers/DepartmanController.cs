using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        Context c = new Context();
        // GET: Departman
        public ActionResult Index()
        {
            var values = c.Departmans.Where(x => x.DepartmanStatus == true).ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddDepartman()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddDepartman(Departman p)
        {
            p.DepartmanStatus = true;
            c.Departmans.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDepartman(int id)
        {
            var values = c.Departmans.Find(id);
            values.DepartmanStatus = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult EditDepartman(int id)
        {
            var values = c.Departmans.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditDepartman(Departman p)
        {
            var values = c.Departmans.Find(p.Departmanid);
            values.DepartmanAd = p.DepartmanAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult DepartmanDetay(int id , int p = 1)
        {
            //DEPARTMANA GÖRE PERSONELLER
            var values = c.Personels.Where(x => x.Departmanid == id).ToList().ToPagedList(p, 8);
            var dpt = c.Departmans.Where(x => x.Departmanid == id).Select(y => y.DepartmanAd).FirstOrDefault();
            ViewBag.name = dpt;

            return View(values);
        }
        public PartialViewResult PersonelDatalar()
        {
            //PERSONEL SATIŞ DATALARI
            var toplamtutar = c.SatisHarekets.Sum(y => y.ToplamTutar).ToString("n2");
            ViewBag.tutar = toplamtutar;

            var toplamadet = c.SatisHarekets.Select(y => y.Satisid).Count().ToString();
            ViewBag.adet = toplamadet;

            var toplamürünadet = c.SatisHarekets.Sum(y => y.Adet).ToString();
            ViewBag.ürünadet = toplamürünadet;

            return PartialView();
        }
        public ActionResult DepartmanPersonelSatis(int id)
        {
            var values = c.SatisHarekets.Where(x => x.Personelid == id).ToList();
            var name = c.Personels.Where(x => x.Personelid == id ).Select( y=> y.PersonelAd).FirstOrDefault();
            ViewBag.name = name;
            return View(values);
        }


    }
}