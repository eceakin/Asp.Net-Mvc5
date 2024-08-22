using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class PersonelController : Controller
    {
        // GET: Personel
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Personels.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult PersonelEkle()
        {
            List<SelectListItem> deger1 =
               (from x in c.Departmans.ToList()
                select
                new SelectListItem
                {
                    Text = x.DepartmanAd,
                    Value = x.DepartmanID.ToString()
                }).ToList();

            ViewBag.deger = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult PersonelEkle(Personel personel) { 
        
            c.Personels.Add(personel);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult PersonelGetir(int id)
        {
            List<SelectListItem> deger1 =
              (from x in c.Departmans.ToList()
               select
               new SelectListItem
               {
                   Text = x.DepartmanAd,
                   Value = x.DepartmanID.ToString()
               }).ToList();

            ViewBag.deger1 = deger1;
            var pers = c.Personels.Find(id);
            return View("PersonelGetir",pers);
        }
        public ActionResult PersonelGuncelle(Personel personel)
        {
            var p = c.Personels.Find(personel.PersonelID);
            p.PersonelAd = personel.PersonelAd;
            p.PersonelSoyad = personel.PersonelSoyad;
            p.PersonelGorsel = personel.PersonelGorsel;
            p.DepartmanID = personel.DepartmanID;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult PersonelListe()
        {
            var sorgu = c.Personels.ToList();
            return View(sorgu);
        }
    }
}