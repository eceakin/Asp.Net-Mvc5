using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class UrunController : Controller
    {
        // GET: Urun
        Context c = new Context();
        public ActionResult Index(string p)
        {
            var urunler = from x in c.Uruns select x;
            if (!string.IsNullOrEmpty(p))
            {
                urunler = urunler.Where(y => y.UrunAd.Contains(p));
            }
            return View(urunler.ToList());
        }
        [HttpGet]
        public ActionResult YeniUrun()
        {
            //dropdown
            List<SelectListItem> deger1 =
                (from x in c.Kategoris.ToList() select
                 new SelectListItem
                 {
                     Text =x.KategoriAd,
                     Value = x.KategoriID.ToString()
                 }).ToList();

            ViewBag.Deger1 = deger1;
            return View();
        }
        [HttpPost]
        public ActionResult YeniUrun(Urun urun)
        {
            c.Uruns.Add(urun);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunSil(int id)
        {
            var deger = c.Uruns.Find(id);
            deger.Durum = false;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            List<SelectListItem> deger1 =
               (from x in c.Kategoris.ToList()
                select
                new SelectListItem
                {
                    Text = x.KategoriAd,
                    Value = x.KategoriID.ToString()
                }).ToList();

            ViewBag.Deger1 = deger1;
            var urunDeger = c.Uruns.Find(id);
            return View("UrunGetir",urunDeger);
        }
        public ActionResult UrunGuncelle(Urun urun)
        {
            var u = c.Uruns.Find(urun.UrunID);
            u.AlisFiyat = urun.AlisFiyat;
            u.Durum = urun.Durum;
            u.KategoriId = urun.KategoriId;
            u.Marka = urun.Marka;
            u.SatisFiyat=urun.SatisFiyat;
            u.Stok = urun.Stok;
            u.UrunAd = urun.UrunAd;
            u.UrunGorsel = urun.UrunGorsel;
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunListesi()
        {
            var degerler = c.Uruns.ToList();
            return View(degerler);
        }

    }
}