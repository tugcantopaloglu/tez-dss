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
    public class DekanlikRaporlarController : Controller
    {
        private readonly donersermayeContext _context;

        public DekanlikRaporlarController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> IsDetayKisi(int? PersonelId, int? FirmaId, int? BolumId, DateTime? BaslamaTarihi, DateTime? BitisTarihi,int? IsNo)
        {

            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join f in _context.Personeller on a.PersonelId equals f.Id
                        join g in _context.Unvanlar on f.UnvanId equals g.Id
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
                            Tur = d.Tur,
                            Kisi = Convert.ToDecimal(a.Tutar),
                            Tutar = Convert.ToDecimal(b.Tutar),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo,
                            PersonelAdi = g.Unvan + ' ' + f.Ad + ' ' + f.Soyad,
                            PersonelId = f.Id
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
            foreach(var d in result)
            {
                d.KesintiToplami = (from a in _context.Iskesintiler
                                   join b in _context.Isler on a.IsId equals b.Id
                                   join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                   where a.IsId == d.IsId
                                   select (c.Oran * d.Tutar)/100).Sum();
            }
            var p = _context.Personeller.Include(a => a.Unvan).Select(s => new { Id = s.Id, Ad = string.Format("{0} {1} {2}", s.Unvan.Unvan, s.Ad, s.Soyad) }).OrderBy(a => a.Ad).ToList();
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["PersonelId"] = new SelectList(p, "Id", "Ad", PersonelId);
            return View(result);
        }
        // GET: IstekGir
        public async Task<IActionResult> IsDetay(int? FirmaId, int? BolumId, int? IsTuru, DateTime? BaslamaTarihi, DateTime? BitisTarihi, int? IsNo)
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
                            Tur = d.Tur,
                            TurId = d.Id,
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
                d.KesintiToplami = (from a in _context.Iskesintiler
                                    join b in _context.Isler on a.IsId equals b.Id
                                    join c in _context.KesintiOran on a.KesintiOranId equals c.Id
                                    where a.IsId == d.IsId
                                    select (c.Oran * d.Tutar) / 100).Sum();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["IsTurleri"] = new SelectList(_context.IsTurleri, "Id", "Tur", IsTuru);
            return View(result);
        }

        public async Task<IActionResult> dskatki(int? BolumId, int? IsTuru, DateTime? BaslamaTarihi, DateTime? BitisTarihi)
        {
            var query = from h in _context.Personeller
                        join k in _context.Unvanlar on h.UnvanId equals k.Id
                        select new MyModelDSK
                        {
                            OgretimUyesi = k.Unvan + ' ' + h.Ad + ' ' + h.Soyad,
                            Kisi = (from z in _context.IsAyrinti
                                    join b in _context.Isler on z.ısId equals b.Id
                                    where b.Id == z.ısId && z.PersonelId == h.Id && b.FaturaTipi == 1 && b.BolumOnay == 1
                                    && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                    select z.Tutar).Sum(),
                            Tutar = (from z in _context.IsAyrinti
                                     join b in _context.Isler on z.ısId equals b.Id
                                     where b.Id == z.ısId && b.FaturaTipi == 1 && b.BolumOnay == 1
                                     && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                     select z.Tutar).Sum()
                        };

            if (BolumId != null)
            {
                query = from h in _context.Personeller
                        join k in _context.Unvanlar on h.UnvanId equals k.Id
                        select new MyModelDSK
                        {
                            OgretimUyesi = k.Unvan + ' ' + h.Ad + ' ' + h.Soyad,
                            Kisi = (from z in _context.IsAyrinti
                                    join b in _context.Isler on z.ısId equals b.Id
                                    where b.Id == z.ısId && z.PersonelId == h.Id && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.BolumId == BolumId
                                    && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                    select z.Tutar).Sum(),
                            Tutar = (from z in _context.IsAyrinti
                                     join b in _context.Isler on z.ısId equals b.Id
                                     where b.Id == z.ısId && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.BolumId == BolumId
                                     && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                     select z.Tutar).Sum()
                        };
                ViewData["Filtre"] = "filtre";
            }


            if (BaslamaTarihi != null)
            {
                query = from h in _context.Personeller
                        join k in _context.Unvanlar on h.UnvanId equals k.Id
                        select new MyModelDSK
                        {
                            OgretimUyesi = k.Unvan + ' ' + h.Ad + ' ' + h.Soyad,
                            Kisi = (from z in _context.IsAyrinti
                                    join b in _context.Isler on z.ısId equals b.Id
                                    where b.Id == z.ısId && z.PersonelId == h.Id && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.FaturaTarihi >= BaslamaTarihi
                                    && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                    select z.Tutar).Sum(),
                            Tutar = (from z in _context.IsAyrinti
                                     join b in _context.Isler on z.ısId equals b.Id
                                     where b.Id == z.ısId && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.FaturaTarihi >= BaslamaTarihi
                                     && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                     select z.Tutar).Sum()
                        };
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                query = from h in _context.Personeller
                        join k in _context.Unvanlar on h.UnvanId equals k.Id
                        select new MyModelDSK
                        {
                            OgretimUyesi = k.Unvan + ' ' + h.Ad + ' ' + h.Soyad,
                            Kisi = (from z in _context.IsAyrinti
                                    join b in _context.Isler on z.ısId equals b.Id
                                    where b.Id == z.ısId && z.PersonelId == h.Id && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.FaturaTarihi <= BitisTarihi
                                    && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                    select z.Tutar).Sum(),
                            Tutar = (from z in _context.IsAyrinti
                                     join b in _context.Isler on z.ısId equals b.Id
                                     where b.Id == z.ısId && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.FaturaTarihi <= BitisTarihi
                                     && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                     select z.Tutar).Sum()
                        };
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }

            if (BitisTarihi != null && BitisTarihi != null)
            {
                query = from h in _context.Personeller
                        join k in _context.Unvanlar on h.UnvanId equals k.Id
                        select new MyModelDSK
                        {
                            OgretimUyesi = k.Unvan + ' ' + h.Ad + ' ' + h.Soyad,
                            Kisi = (from z in _context.IsAyrinti
                                    join b in _context.Isler on z.ısId equals b.Id
                                    where b.Id == z.ısId && z.PersonelId == h.Id && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.FaturaTarihi >= BaslamaTarihi && b.FaturaTarihi <= BitisTarihi
                                    && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                    select z.Tutar).Sum(),
                            Tutar = (from z in _context.IsAyrinti
                                     join b in _context.Isler on z.ısId equals b.Id
                                     where b.Id == z.ısId && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.FaturaTarihi >= BaslamaTarihi && b.FaturaTarihi <= BitisTarihi
                                     && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                     select z.Tutar).Sum()
                        };
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }

            /*
            if (BolumId != null)
            {
                query = query.Where(a => a.BolumId == BolumId);
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
            */
            var result = query.Where(a => a.Kisi != 0).ToList().OrderByDescending(a => a.Kisi);

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["IsTurleri"] = new SelectList(_context.IsTurleri, "Id", "Tur", IsTuru);
            return View(result);
        }


        public async Task<IActionResult> dskatkiB()
        {
            var query = from h in _context.Personeller
                        join k in _context.Unvanlar on h.UnvanId equals k.Id
                        select new MyModelDSK
                        {
                            OgretimUyesi = k.Unvan + ' ' + h.Ad + ' ' + h.Soyad,
                            Kisi = (from z in _context.IsAyrinti
                                    join b in _context.Isler on z.ısId equals b.Id
                                    where b.Id == z.ısId && z.PersonelId == h.Id && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))
                                    && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                    select z.Tutar).Sum(),
                            Tutar = (from z in _context.IsAyrinti
                                     join b in _context.Isler on z.ısId equals b.Id
                                     where b.Id == z.ısId && b.FaturaTipi == 1 && b.BolumOnay == 1 && b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))
                                     && b.FaturaTarihi.Value.Year == DateTime.Now.Year
                                     select z.Tutar).Sum()
                        };

            /*
            if (BolumId != null)
            {
                query = query.Where(a => a.BolumId == BolumId);
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
            */
            var result = query.Where(a => a.Kisi != 0).ToList().OrderByDescending(a => a.Kisi);

            return View(result);
        }

    }
}
