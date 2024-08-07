﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class KategoriController : Controller
    {
        //KATEGORİ LİSTELEME
        //CONTEXT den nesne türet
        //var türünde variable oluşturup tolist yap 
        // view de değerleri döndür
        // GET: Kategori
        Context c = new Context();
        public ActionResult Index()
        {
            var degerler = c.Kategoris.ToList();

            return View(degerler);
        }

        [HttpGet]
        public ActionResult KategoriEkle()
        {
            return View();
        }
        [HttpPost]
        public ActionResult KategoriEkle(Kategori k)
        {
            c.Kategoris.Add(k);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriSil(int id) {
            var kategori = c.Kategoris.Find(id);
            c.Kategoris.Remove(kategori);
            c.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult KategoriGetir(int id) {
            var kategori = c.Kategoris.Find(id);
            return View("KategoriGetir",kategori);
        }
        
        public ActionResult KategoriGuncelle(Kategori k)
        {
            var kategori = c.Kategoris.Find(k.KategoriID);
            kategori.KategoriAd = k.KategoriAd;
            c.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}