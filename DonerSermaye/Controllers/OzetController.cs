using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

namespace DonerSermaye.Controllers
{
    public class OzetController : Controller
    {
        private donersermayeContext _context = new donersermayeContext();
        public IActionResult Index()
        {
            var query = from a in _context.Isler
                        join b in _context.Iskesintiler on a.Id equals b.IsId
                        join c in _context.KesintiOran on b.KesintiOranId equals c.Id
                        join d in _context.Kesintitipleri on c.KesintiTip.Id equals d.Id
                        select new
                        {
                            
                            toplam=(c.Oran* Convert.ToDecimal(a.Tutar))/100

                        };

            /*
                        var bIs = _context.Isler.Where(a => a.BolumId ==1).Count();
                        var bTop = _context.Isler.Where(a => a.BolumId == 1).Select(b => b.Tutar).Sum();
                        var bOdenen = _context.Isler.Where(a => a.BolumId == 1 && a.Durum == 1).Select(b => b.Tutar).Sum();
                        var bolumpayi = from a in _context.Iskesintiler
                                        join b in _context.Isler on a.IsId equals b.Id
                                        join f in _context.Kesintiler on a.KesintiId equals f.Id
                                        where a.KesintiId == 15 && b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && b.Durum == 1
                                        select new { b.Tutar, f.Oran };


                        decimal s = 0;
                        foreach (var item in bolumpayi)
                        {
                            s += (Convert.ToDecimal(item.Tutar) * Convert.ToDecimal(item.Oran) / 100);
                        }
                        var sum = (from z in _context.Istekler where z.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) select z.Fiyat).Sum();
                        if (sum == null)
                        {
                            ViewBag.bPayi = String.Format("{0:0.##}", s);
                        }
                        else
                        {
                            ViewBag.bPayi = String.Format("{0:0.##}", s - sum);
                        }
                        ViewBag.bIs = bIs;
                        ViewBag.bTop = String.Format("{0:0.##}", bTop);
                        ViewBag.bOdenen = String.Format("{0:0.##}", bOdenen);

                        //////////////////////////////////////////////////////////////
                        var mbIs = _context.Isler.Where(a => a.BolumId == 2).Count();
                        var mbTop = _context.Isler.Where(a => a.BolumId == 2).Select(b => b.Tutar).Sum();
                        var mbOdenen = _context.Isler.Where(a => a.BolumId == 2 && a.Durum == 1).Select(b => b.Tutar).Sum();
                        var mbolumpayi = _context.Isler.Select(a => new { a.Tutar, a.Kdv, a.BolumId, a.Durum }).Where(a => a.BolumId == 2 && a.Durum == 1).ToList();
                        decimal h = 0;
                        foreach (var item in mbolumpayi)
                        {
                            h += (Convert.ToDecimal(item.Tutar) * Convert.ToDecimal(item.Kdv) / 100);
                        }
                        ViewBag.mbIs = mbIs;
                        ViewBag.mbTop = String.Format("{0:0.##}", mbTop);
                        ViewBag.mbOdenen = String.Format("{0:0.##}", mbOdenen);
                        ViewBag.mbPayi = String.Format("{0:0.##}", h);

                */



            return View(query);
        }
    }
}