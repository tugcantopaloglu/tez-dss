using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace DonerSermaye.Controllers
{
    public class RaporController : Controller
    {
        private readonly donersermayeContext _context;

        public RaporController(donersermayeContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult KisiListesi()
        {
            var query = (from a in _context.IsAyrinti
                         join b in _context.Personeller on a.PersonelId equals b.Id
                         join c in _context.Isler on a.ısId equals c.Id
                         where (c.Dagitim==1 && b.Id!=19)
                         group new
                         {
                             b.Ad,
                             b.Soyad,
                             b.TcKimlikNo,
                             b.Iban,
                             a.Tutar,
                         } by b into grup
                         select new
                         {
                             ad = grup.Key.Ad,
                             soyad = grup.Key.Soyad,
                             iban = grup.Key.Iban,
                             tc = grup.Key.TcKimlikNo,
                             tutar = grup.Sum(s => s.Tutar)
                         }
                        ).ToList();

            return Json(query);

        }
        public JsonResult RaporGetir()
        {
            var query = _context.Raporlar.ToList();
            return Json(query);

        }
        public JsonResult KisiIsGetir()
        {
            var query = (from a in _context.IsAyrinti
                         join b in _context.Isler on a.ısId equals b.Id
                         join c in _context.Personeller on a.PersonelId equals c.Id
                         join d in _context.Bolumler on b.BolumId equals d.Id
                         join e in _context.IsTurleri on b.TurId equals e.Id
                         join f in _context.Firmalar on b.FirmaId equals f.Id
                         where(b.HesapId==null && c.Id != 19)
                         orderby c.Ad
                         select new
                         {
                           firma=f.FirmaAdi,
                           tur=e.Tur,
                           bolum=d.Bolum,
                           ad=c.Ad,
                           soyad=c.Soyad,
                           ktutar=a.Tutar
                         });
            return Json(query);

        }
        public JsonResult IsGetir()
        {
            var query = (
                         from a in _context.Isler
                         join b in _context.Bolumler on a.BolumId equals b.Id
                         join c in _context.IsTurleri on a.TurId equals c.Id
                         join d in _context.Firmalar on a.FirmaId equals d.Id
                         where(a.HesapId==null)
                         select new
                         {
                             firma = d.FirmaAdi,
                             tur = c.Tur,
                             bolum =b.Bolum,
                             tutar = a.Tutar
                         });
            return Json(query);

        }
        public JsonResult OnayIsGetir()
        {
            var query = (
                         from a in _context.Isler
                         join b in _context.Bolumler on a.BolumId equals b.Id
                         join c in _context.IsTurleri on a.TurId equals c.Id
                         join d in _context.Firmalar on a.FirmaId equals d.Id
                         where (a.Durum==1 && a.HesapId==null)
                         select new
                         {
                             firma = d.FirmaAdi,
                             tur = c.Tur,
                             bolum = b.Bolum,
                             tutar = a.Tutar,
                             faturano=a.FaturaNo,
                             tarih=a.FaturaTarihi
                         });
            return Json(query);

        }

        decimal s = 0;
        public JsonResult BolumPayiGetir()
        {
           /* var query = (from a in _context.Iskesintiler
                         join b in _context.Isler on a.IsId equals b.Id
                         join f in _context.Kesintiler on a.KesintiId equals f.Id
                         where a.KesintiId == 15
                         select new {
                             b.Tutar,
                             f.Oran,
                             b.BolumId,
                             pay =  b.Tutar / 100 * Convert.ToDecimal(f.Oran)

                         }).ToList();

           
            foreach (var item in query)
            {
                s += (Convert.ToDecimal(item.Tutar) * Convert.ToDecimal(item.Oran) / 100);
            }
           
            ViewBag.bolumpayi= String.Format("{0:0.##}", s);

          
    */  var query=1;
            return Json(query);

        }
    }
}