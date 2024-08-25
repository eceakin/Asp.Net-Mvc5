using MvcOnlineTicariOtomasyon.Models.Siniflar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcOnlineTicariOtomasyon.Controllers
{
    public class CariPanelController : Controller
    {
        // GET: CariPanel
        Context c = new Context();
        [Authorize]
        
        public ActionResult Index()
        {
            var cariMail = (string)Session["CariMail"];
            var degerler = c.Carilers.FirstOrDefault(x=>x.CariMail == cariMail);
            ViewBag.m = cariMail;
            return View(degerler);
        }
        public ActionResult Siparislerim()
        {
            var cariMail = (string)Session["CariMail"];
            var id = c.Carilers.Where(x=>x.CariMail == cariMail.ToString())
                .Select(y=>y.CariID).FirstOrDefault();
            var degerler = c.SatisHarekets.Where(x => x.CariID == id).ToList();
            return View(degerler);
        }
    }
}