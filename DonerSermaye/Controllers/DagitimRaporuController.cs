using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;

namespace DonerSermaye.Controllers
{
    public class DagitimRaporuController : Controller
    {
        private readonly donersermayeContext _context;

        public DagitimRaporuController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> Index(int? HesapId)
        {
            int? personelid = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));

            var toplamdagitim = (from a in _context.IsAyrinti
                                 join b in _context.Isler on a.ısId equals b.Id
                                 join f in _context.Hesaplar on b.HesapId equals f.Id
                                 where (b.HesapId != null && a.PersonelId == personelid)
                                 select a.Tutar).Sum();

            var secilendagitim = (from a in _context.IsAyrinti
                                  join b in _context.Isler on a.ısId equals b.Id
                                  join f in _context.Hesaplar on b.HesapId equals f.Id
                                  where (b.HesapId == HesapId && a.PersonelId == personelid)
                                  select a.Tutar).Sum();
            if (HesapId == null)
                secilendagitim = toplamdagitim;
            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Hesaplar on b.HesapId equals f.Id
                        where (a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")) && b.BolumOnay == 1 && b.FaturaTipi == 1)
                        orderby b.FaturaTarihi, b.BolumOnay descending
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            Aciklama = b.Aciklama,
                            Tur = d.Tur,
                            Kisi = Convert.ToDecimal(a.Tutar),
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo,
                            HesapId = b.HesapId,
                            HesapAdi = f.Hesap
                        };
            

            if (HesapId != null)
            {
                query = query.Where(a => a.HesapId == HesapId);
                ViewData["Filtre"] = "filtre";
            }
            
            var result = query.ToList();
            foreach(var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                   join b in _context.Isler on a.IsId equals b.Id
                                   join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                   where a.IsId == d.IsId
                                   select (c.Oran * d.Tutar)/100).Sum();
            }
            if (toplamdagitim != null)
                ViewData["toplamdagitim"] = ((decimal)toplamdagitim).ToString("#.##");
            if (secilendagitim != null)
                ViewData["secilendagitim"] = ((decimal)secilendagitim).ToString("#.##");
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Hesap", HesapId);
            return View(result);
        }
        // GET: IstekGir
        public async Task<IActionResult> Bolum(int? HesapId)
        {
            int? personelid = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));

            var toplamdagitim = (from a in _context.IsAyrinti
                                 join b in _context.Isler on a.ısId equals b.Id
                                 join f in _context.Hesaplar on b.HesapId equals f.Id
                                 where (b.HesapId != null && a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")))
                                 select a.Tutar).Sum();

            var secilendagitim = (from a in _context.IsAyrinti
                                  join b in _context.Isler on a.ısId equals b.Id
                                  join f in _context.Hesaplar on b.HesapId equals f.Id
                                  where (b.HesapId == HesapId && a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")))
                                  select a.Tutar).Sum();

            if (HesapId == null)
                secilendagitim = toplamdagitim;

            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Hesaplar on b.HesapId equals f.Id
                        join h in _context.Personeller on a.PersonelId equals h.Id
                        join g in _context.Unvanlar on h.UnvanId equals g.Id
                        where (b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && b.BolumOnay == 1 && b.FaturaTipi == 1)
                        orderby b.FaturaTarihi, b.BolumOnay descending
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            Aciklama = b.Aciklama,
                            PersonelId = a.PersonelId,
                            PersonelAdi = string.Format("{0} {1} {2}", g.Unvan, h.Ad, h.Soyad),
                            Tur = d.Tur,
                            Kisi = Convert.ToDecimal(a.Tutar),
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo,
                            HesapId = b.HesapId,
                            HesapAdi = f.Hesap
                        };

            if (HesapId != null)
            {
                query = query.Where(a => a.HesapId == HesapId);
                ViewData["Filtre"] = "filtre";
            }

            var result = query.ToList();
            foreach (var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                    join b in _context.Isler on a.IsId equals b.Id
                                    join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                    where a.IsId == d.IsId
                                    select (c.Oran * d.Tutar) / 100).Sum();
            }

            if (toplamdagitim != null)
                ViewData["toplamdagitim"] = ((decimal)toplamdagitim).ToString("#.##");
            if (secilendagitim != null)
                ViewData["secilendagitim"] = ((decimal)secilendagitim).ToString("#.##");

            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Hesap", HesapId);
            return View(result);
        }
        public async Task<IActionResult> Dekanlik(int? HesapId)
        {
            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Hesaplar on b.HesapId equals f.Id
                        join h in _context.Personeller on a.PersonelId equals h.Id
                        join g in _context.Unvanlar on h.UnvanId equals g.Id
                        where (b.BolumOnay == 1 && b.FaturaTipi == 1)
                        orderby b.FaturaTarihi, b.BolumOnay descending
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            Aciklama = b.Aciklama,
                            PersonelId = a.PersonelId,
                            PersonelAdi = string.Format("{0} {1} {2}", g.Unvan, h.Ad, h.Soyad),
                            Tur = d.Tur,
                            Kisi = Convert.ToDecimal(a.Tutar),
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo,
                            HesapId = b.HesapId,
                            HesapAdi = f.Hesap
                        };

            if (HesapId != null)
            {
                query = query.Where(a => a.HesapId == HesapId);
                ViewData["Filtre"] = "filtre";
            }

            var result = query.ToList();
            foreach (var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                    join b in _context.Isler on a.IsId equals b.Id
                                    join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                    where a.IsId == d.IsId
                                    select (c.Oran * d.Tutar) / 100).Sum();
            }

            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Hesap", HesapId);
            return View(result);
        }
    }
}
