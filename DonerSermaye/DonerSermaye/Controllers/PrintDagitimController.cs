using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;

namespace DonerSermaye.Controllers
{
    public class PrintDagitimController : Controller
    {
        private readonly donersermayeContext _context;

        public PrintDagitimController(donersermayeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int HesapId)
        {
            var isler = _context.Isler.Include(a => a.Bolum).Include(a => a.Tur).Include(a => a.Firma).Include(a => a.IsAyrinti).ThenInclude(a => a.Personel).ThenInclude(a => a.Unvan).Where(a => a.HesapId == HesapId).ToList();

            var hesap = _context.Hesaplar.Where(a => a.Id == HesapId).FirstOrDefault();

            ViewBag.Arge = "0";
            
            ViewBag.HesapAdi = hesap.Hesap;
            ViewBag.ToplantiTarihi = hesap.ToplantiTarihi;
            ViewBag.ToplantiSayisi = hesap.ToplantiSayisi;
            ViewBag.KararSayisi = hesap.KararSayisi;

            Ayarlar ayarlar = _context.Ayarlar.FirstOrDefault();

            ViewBag.k1 = ayarlar.Dekan;
            ViewBag.k2 = ayarlar.Uye1;
            ViewBag.k3 = ayarlar.Uye2;
            ViewBag.k4 = ayarlar.Uye3;
            ViewBag.k5 = ayarlar.Uye4;
            ViewBag.k6 = ayarlar.Uye5;
            ViewBag.k7 = ayarlar.Uye6;
            ViewBag.k8 = ayarlar.Raportor;

            List<IsAyrintiViewModel> ayr = new List<IsAyrintiViewModel>();
            IsAyrintiViewModel ayrd = new IsAyrintiViewModel();
            decimal dt = 0;
            decimal topdt = 0;
            foreach (var ii in isler)
            {
                if (ii.TurId == 3)
                    ViewBag.Arge = "1";
                foreach (var zz in ii.IsAyrinti)
                {
                    if (ayr.Where(a => a.PersonelId == zz.PersonelId).Count() == 0 || ii.TurId == 3)
                    {
                        ayrd = new IsAyrintiViewModel();
                        ayrd.PersonelId = zz.PersonelId;
                        ayrd.Unvan = zz.Personel.Unvan.Unvan;
                        ayrd.AdSoyad = zz.Personel.Ad + " " + zz.Personel.Soyad;
                        ayrd.Tutar = zz.Tutar;
                        ayrd.UnvanId = zz.Personel.UnvanId;
                        ayrd.Aciklama = ii.Aciklama;
                        ayr.Add(ayrd);
                    }
                    else
                    {
                        ayrd = ayr.Where(a => a.PersonelId == zz.PersonelId).FirstOrDefault();
                        ayrd.Tutar += zz.Tutar;
                    }
                    dt += Convert.ToDecimal(zz.Tutar);
                    
                }
                topdt += Convert.ToDecimal(ii.Tutar);
            }
            List<IsAyrintiViewModel> ayrList = ayr.OrderBy(a => a.UnvanId).ThenBy(b => b.AdSoyad).ToList();
            
            ViewData["nettoplam"] = ((Decimal)dt).ToString("0.00");
            ViewData["bruttoplam"] = ((Decimal)topdt).ToString("0.00");

            return View(ayrList);
        }
        
    }
}
