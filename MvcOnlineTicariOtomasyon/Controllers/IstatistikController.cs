﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcOnlineTicariOtomasyon.Models.Siniflar;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class IstatistikController : Controller
    {
        // GET: Istatistik
        Context c = new Context();
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
            var deger6 = (from x in c.Uruns select x.Marka).Distinct().Count().ToString();
            ViewBag.d6 = deger6;
            var deger7 = c.Uruns.Count(x => x.Stok <= 20).ToString();
            ViewBag.d7 = deger7;
            var deger8 = (from x in c.Uruns
                          orderby
                          x.SatisFiyat descending
                          select
                          x.UrunAd
                          ).FirstOrDefault();
            ViewBag.d8 = deger8;

            var deger9 = (from x in c.Uruns
                          orderby
                          x.SatisFiyat ascending
                          select
                          x.UrunAd
                           ).FirstOrDefault();
            ViewBag.d9 = deger9;

            var deger10 = c.Uruns.Count(x => x.UrunAd == "Buzdolabı").ToString();
            ViewBag.d10 = deger10;


            var deger11 = c.Uruns.Count(x => x.UrunAd == "Laptop").ToString();
            ViewBag.d11 = deger11;


            var deger12 = c.Uruns.GroupBy(x => x.Marka).OrderByDescending(y => y.Count()).
                Select(z => z.Key).FirstOrDefault();
            ViewBag.d12 = deger12;

            var deger13 = c.Uruns.Where(u => u.UrunID == (c.SatisHarekets.GroupBy(x => x.UrunID).
                OrderByDescending(y => y.Count()).Select(z => z.Key).
                FirstOrDefault())).Select(k => k.UrunAd).FirstOrDefault();
            ViewBag.d13 = deger13;


            var deger14 = c.SatisHarekets.Sum(x => x.ToplamTutar).ToString();
            ViewBag.d14 = deger14;

            DateTime bugun = DateTime.Today;
            var deger15 = c.SatisHarekets.Count(x => x.Tarih == bugun).ToString();
            ViewBag.d15 = deger15;

            var deger16 = c.SatisHarekets.Where(x => x.Tarih == bugun)
                .Sum(y => y.ToplamTutar).ToString();
            ViewBag.d16 = deger16;
            return View();
        }
        public ActionResult KolayTablolar()
        {
            var sorgu = from x in c.Carilers group x by x.CariŞehir
                        into g select new SinifGrup
                        {
                            Sehir = g.Key,
                            Sayi = g.Count()
                        };
            return View(sorgu.ToList());
        }
        public PartialViewResult Partial1()
        {
            var sorgu2 = from x in c.Personels
                         group x by x.DepartmanID into
                         g
                         select new SinifGrup2
                         {
                             Departman = g.Key,
                             Sayi = g.Count()
                         };
            return PartialView(sorgu2.ToList());
        }
    }
}