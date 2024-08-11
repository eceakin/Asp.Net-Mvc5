﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class SatisController : Controller
    {
        // GET: Satis
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.SatisHarekets.ToList();
            return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniSatis()
        {
            List<SelectListItem> deger1 =
                (from x in c.Uruns.ToList()
                 select  new SelectListItem
                 {
                     Text = x.UrunAd,
                     Value = x.UrunID.ToString()
                 }
                 ).ToList();

            List<SelectListItem> deger2=
               (from x in c.Carilers.ToList()
                select new SelectListItem
                {
                    Text = x.CariAd + " " + x.CariSoyad,
                    Value = x.CariID.ToString()
                }
                ).ToList();

            List<SelectListItem> deger3 =
               (from x in c.Personels.ToList()
                select new SelectListItem
                {
                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                    Value = x.PersonelID.ToString()
                }
                ).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            return View();
        }
        [HttpPost]
        public ActionResult YeniSatis(SatisHareket satis)
        {
           // satis.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString()); 
            c.SatisHarekets.Add(satis);
            c.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult SatisGetir(int id)
        {

            List<SelectListItem> deger1 =
                (from x in c.Uruns.ToList()
                 select new SelectListItem
                 {
                     Text = x.UrunAd,
                     Value = x.UrunID.ToString()
                 }
                 ).ToList();

            List<SelectListItem> deger2 =
               (from x in c.Carilers.ToList()
                select new SelectListItem
                {
                    Text = x.CariAd + " " + x.CariSoyad,
                    Value = x.CariID.ToString()
                }
                ).ToList();

            List<SelectListItem> deger3 =
               (from x in c.Personels.ToList()
                select new SelectListItem
                {
                    Text = x.PersonelAd + " " + x.PersonelSoyad,
                    Value = x.PersonelID.ToString()
                }
                ).ToList();
            ViewBag.dgr1 = deger1;
            ViewBag.dgr2 = deger2;
            ViewBag.dgr3 = deger3;
            var deger = c.SatisHarekets.Find(id);
            return  View("SatisGetir" , deger);
        }
        public ActionResult SatisGuncelle(SatisHareket satis)
        {
            var deger = c.SatisHarekets.Find(satis.SatisID);
            deger.CariID = satis.CariID;
            deger.Adet = satis.Adet;
            deger.Fiyat = satis.Fiyat;
            deger.PersonelID = satis.PersonelID;
            deger.Tarih = satis.Tarih;
            deger.ToplamTutar = satis.ToplamTutar;
            deger.UrunID = satis.UrunID;
            c.SaveChanges();
            return RedirectToAction("Index");


        }
        public ActionResult SatisDetay(int id)
        {
            var degerler = c.SatisHarekets.
                Where(x=>x.SatisID == id).ToList();
            return View(degerler);
        }

    }
}