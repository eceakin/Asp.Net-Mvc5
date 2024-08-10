using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariController : Controller
    {
        // GET: Cari
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Carilers.Where(x => x.Durum == true).ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniCari()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniCari(Cariler cari)
        {
            cari.Durum = true;
            c.Carilers.Add(cari);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariSil(int id)
        {
            var cari = c.Carilers.Find(id);
            cari.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult CariGetir(int id) {
            var cari = c.Carilers.Find(id);
            return View("CariGetir", cari);
        
        }
        public ActionResult CariGuncelle(Cariler cari)
        {
            if (!ModelState.IsValid)
            {
                return View("CariGetir");
            }

            var ca = c.Carilers.Find(cari.CariID);
            ca.CariAd =cari.CariAd;
            ca.CariSoyad =cari.CariSoyad;
            ca.CariŞehir = cari.CariŞehir;
            ca.CariMail =cari.CariMail;
            c.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult MusteriSatis(int id) {
            var degerler = c.SatisHarekets
                .Where(x => x.CariID == id).ToList();
            var cr = c.Carilers.Where(x => x.CariID == id).
                Select(y => y.CariAd + " " + y.CariSoyad).FirstOrDefault();
            ViewBag.cari = cr;
            return View(degerler);
        
        }
    }
}