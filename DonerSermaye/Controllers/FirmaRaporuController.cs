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
    public class FirmaRaporuController : Controller
    {
        private readonly donersermayeContext _context;

        public FirmaRaporuController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> Index(int? FirmaId, int? BolumId, DateTime? BaslamaTarihi, DateTime? BitisTarihi,int? IsNo)
        {
            int? personelid = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));

            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
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
                            FaturaNo = b.FaturaNo
                        };
            

            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (BolumId != null)
            {
                query = query.Where(a => a.BolumId == BolumId);
                ViewData["Filtre"] = "filtre";
            }
            if (BaslamaTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi >= BaslamaTarihi);
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi <= BitisTarihi);
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (IsNo != null)
            {
                query = query.Where(a => a.IsId == IsNo);
                ViewData["IsNo"] = IsNo;
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

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            return View(result);
        }
        public async Task<IActionResult> Bolum(int? PersonelId, int? FirmaId, int? BolumId, DateTime? BaslamaTarihi, DateTime? BitisTarihi, int? IsNo)
        {

            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Personeller on a.PersonelId equals f.Id
                        join g in _context.Unvanlar on f.UnvanId equals g.Id
                        where (b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && b.BolumOnay == 1 && b.FaturaTipi == 1)
                        orderby b.FaturaTarihi, b.BolumOnay descending
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            PersonelId = a.PersonelId,
                            PersonelAdi = string.Format("{0} {1} {2}", g.Unvan, f.Ad, f.Soyad),                        
                            Aciklama = b.Aciklama,
                            Tur = d.Tur,
                            Kisi = Convert.ToDecimal(a.Tutar),
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo
                        };


            if (PersonelId != null)
            {
                query = query.Where(a => a.PersonelId == PersonelId);
                ViewData["Filtre"] = "filtre";
            }
            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (BolumId != null)
            {
                query = query.Where(a => a.BolumId == BolumId);
                ViewData["Filtre"] = "filtre";
            }
            if (BaslamaTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi >= BaslamaTarihi);
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi <= BitisTarihi);
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (IsNo != null)
            {
                query = query.Where(a => a.IsId == IsNo);
                ViewData["IsNo"] = IsNo;
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
            var p = _context.Personeller.Include(a => a.Unvan).Where(a => a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))).Select(s => new { Id = s.Id, Ad = string.Format("{0} {1} {2}", s.Unvan.Unvan, s.Ad, s.Soyad) }).ToList();
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["PersonelId"] = new SelectList(p, "Id", "Ad", PersonelId);
            return View(result);
        }
        public async Task<IActionResult> BolumTumisler(int? FirmaId, int? IsTuru, DateTime? BaslamaTarihi, DateTime? BitisTarihi, int? IsNo)
        {
            var query = from b in _context.Isler
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        where (b.BolumOnay == 1 && b.FaturaTipi == 1)
                        orderby e.Bolum ascending, b.FaturaNo ascending
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            Aciklama = b.Aciklama,
                            TurId = d.Id,
                            Tur = d.Tur,
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo,
                            faturayuklendimi = b.faturayuklendimi
                        };

            query = query.Where(a => a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")));

            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (IsTuru != null)
            {
                query = query.Where(a => a.TurId == IsTuru);
                ViewData["Filtre"] = "filtre";
            }
            if (BaslamaTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi >= BaslamaTarihi);
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi <= BitisTarihi);
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (IsNo != null)
            {
                query = query.Where(a => a.IsId == IsNo);
                ViewData["IsNo"] = IsNo;
                ViewData["Filtre"] = "filtre";
            }

            var result = query.ToList();
            foreach (var d in result)
            {
                d.KesintiToplami = (from a in _context.Bolumgelir
                                    where a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.IsId == d.IsId select a.Tutar).Sum();
            }
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["IsTurleri"] = new SelectList(_context.IsTurleri, "Id", "Tur", IsTuru);
            return View(result);
        }
    }
}
