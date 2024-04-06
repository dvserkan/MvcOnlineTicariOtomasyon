using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        Context c = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var values = c.Personels.ToList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddPersonel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPersonel(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }
            c.Personels.Add(p);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public PartialViewResult DepartmanGet()
        {
            List<SelectListItem> departman = (from x in c.Departmans.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.DepartmanAd,
                                                  Value = x.Departmanid.ToString()
                                              }).ToList();
            ViewBag.depart = departman;
            return PartialView();
        }
        [HttpGet]
        public ActionResult EditPersonel(int id)
        {
            List<SelectListItem> departman = (from x in c.Departmans.ToList()
                                              select new SelectListItem
                                              {
                                                  Text = x.DepartmanAd,
                                                  Value = x.Departmanid.ToString()
                                              }).ToList();
            ViewBag.depart = departman;

            var values = c.Personels.Find(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult EditPersonel(Personel p)
        {
            if (Request.Files.Count > 0)
            {
                string dosyaadi = Path.GetFileName(Request.Files[0].FileName);
                string uzanti = Path.GetExtension(Request.Files[0].FileName);
                string yol = "~/Image/" + dosyaadi + uzanti;
                Request.Files[0].SaveAs(Server.MapPath(yol));
                p.PersonelGorsel = "/Image/" + dosyaadi + uzanti;
            }

            var values = c.Personels.Find(p.Personelid);
            values.PersonelAd = p.PersonelAd;
            values.PersonelGorsel = p.PersonelGorsel;
            values.Departmanid = p.Departmanid;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}