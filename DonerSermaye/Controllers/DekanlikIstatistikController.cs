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
    public class DekanlikIstatistikController : Controller
    {
        private readonly donersermayeContext _context;

        public DekanlikIstatistikController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> Index(int? FirmaId, int? BolumId, int? PersonelId, DateTime? BaslamaTarihi, DateTime? BitisTarihi)
        {
            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Personeller on a.PersonelId equals f.Id
                        join g in _context.Unvanlar on f.UnvanId equals g.Id
                        where (b.BolumOnay == 1 && b.FaturaTipi == 1)
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

            if (PersonelId == -1)
            {
                query = from b in _context.Isler
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.IsAyrinti on b.Id equals f.ısId into gj
                        where (b.BolumOnay == 1 && b.FaturaTipi == 1 && gj.Count() == 0)
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            PersonelId = -1,
                            PersonelAdi = "",
                            Aciklama = b.Aciklama,
                            Tur = d.Tur,
                            Kisi = 0,
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo
                        };
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
            if (PersonelId != null)
            {
                query = query.Where(a => a.PersonelId == PersonelId);
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

            var result = query.ToList();
            foreach (var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                    join b in _context.Isler on a.IsId equals b.Id
                                    join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                    where a.IsId == d.IsId
                                    select (c.Oran * d.Tutar) / 100).Sum();
            }

            var zzt = (from a in result
                       group a by a.Tur into ddr
                       select new MyModel3
                       {
                           IsTuru = ddr.Key,
                           IsSayisi = ddr.Count(),
                           KisiTutar = ddr.Sum(a => a.Kisi),
                           ToplamTutar = ddr.Sum(a => a.Tutar)
                       });

            var p = _context.Personeller.Include(a => a.Unvan).Select(s => new { Id = s.Id, Ad = string.Format("{0} {1} {2}", s.Unvan.Unvan, s.Ad, s.Soyad) }).ToList();

            p.Insert(0, new { Id = -1, Ad = "Personelsiz Dağıtımlar" });

            ViewData["PersonelId"] = new SelectList(p, "Id", "Ad", PersonelId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            return View(zzt);
        }
        public async Task<IActionResult> Personel(int? FirmaId, int? BolumId, int? PersonelId, DateTime? BaslamaTarihi, DateTime? BitisTarihi)
        {
            int? personelid = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));

            
            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Personeller on a.PersonelId equals f.Id
                        join g in _context.Unvanlar on f.UnvanId equals g.Id
                        where (b.BolumOnay == 1 && b.FaturaTipi == 1)
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

            if(PersonelId==-1)
            {
                query = from b in _context.Isler
                join c in _context.Firmalar on b.FirmaId equals c.Id
                join e in _context.Bolumler on b.BolumId equals e.Id
                join d in _context.IsTurleri on b.TurId equals d.Id
                join f in _context.IsAyrinti on b.Id equals f.ısId into gj
                where (b.BolumOnay == 1 && b.FaturaTipi == 1 && gj.Count()==0)
                select new MyModel2
                {
                    Id = b.Id,
                    FirmaAdi = c.FirmaAdi,
                    FirmaId = c.Id,
                    BolumAdi = e.Bolum,
                    BolumId = e.Id,
                    PersonelId = -1,
                    PersonelAdi = "",
                    Aciklama = b.Aciklama,
                    Tur = d.Tur,
                    Kisi = 0,
                    Tutar = Convert.ToDecimal(b.Tutar),
                    IsId = b.Id,
                    FaturaTipi = b.FaturaTipi,
                    FaturaTarihi = b.FaturaTarihi,
                    FaturaNo = b.FaturaNo
                };
            }

            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (PersonelId != null)
            {
                query = query.Where(a => a.PersonelId == PersonelId);
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

            var result = query.ToList();
            foreach (var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                    join b in _context.Isler on a.IsId equals b.Id
                                    join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                    where a.IsId == d.IsId
                                    select (c.Oran * d.Tutar) / 100).Sum();
            }

            var zzt = (from a in result
                       group a by a.Tur into ddr
                       select new MyModel3
                       {
                           IsTuru = ddr.Key,
                           IsSayisi = ddr.Count(),
                           KisiTutar = ddr.Sum(a => a.Kisi),
                           ToplamTutar = ddr.Sum(a => a.Tutar)
                       });

            var p = _context.Personeller.Include(a => a.Unvan).Select(s => new { Id = s.Id, Ad = string.Format("{0} {1} {2}", s.Unvan.Unvan, s.Ad, s.Soyad) }).ToList();
            
            p.Add(new { Id = -1, Ad = "Personelsiz Dağıtımlar" });

            ViewData["PersonelId"] = new SelectList(p, "Id", "Ad", PersonelId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            return View(zzt);
        }


        public async Task<IActionResult> LabBolum(int? FirmaId, DateTime? BaslamaTarihi, DateTime? BitisTarihi)
        {
            int? personelid = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));


            var query = from b in _context.Isler
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Personeller on b.PersonelId equals f.Id
                        join g in _context.Unvanlar on f.UnvanId equals g.Id
                        where (b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && b.BolumOnay == 1 && b.FaturaTipi == 1) && b.Lab == 1
                        select new MyModel2
                        {
                            Id = b.Id,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            BolumAdi = e.Bolum,
                            BolumId = e.Id,
                            PersonelId = b.PersonelId,
                            PersonelAdi = string.Format("{0} {1} {2}", g.Unvan, f.Ad, f.Soyad),
                            Aciklama = b.Aciklama,
                            Tur = d.Tur,
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

            var result = query.ToList();
            foreach (var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                    join b in _context.Isler on a.IsId equals b.Id
                                    join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                    where a.IsId == d.IsId
                                    select (c.Oran * d.Tutar) / 100).Sum();
            }

            var zzt = (from a in result
                       group a by a.Tur into ddr
                       select new MyModel3
                       {
                           IsTuru = ddr.Key,
                           IsSayisi = ddr.Count(),
                           ToplamTutar = ddr.Sum(a => a.Tutar)
                       });

            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            return View(zzt);
        }
    }
}
