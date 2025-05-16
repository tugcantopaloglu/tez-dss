using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonerSermaye.Controllers
{
    
    public class GirisController : Controller
    {
        private readonly donersermayeContext _context;

        public GirisController(donersermayeContext context)
        {
            _context = context;
        }

        public IActionResult Index(string att)
        {
            if (att != null)
                ViewBag.alert = "Girilen bilgilere uygun bir kullanıcı bulunamadı. Lütfen tekrar deneyiniz. Sorun devam ederse bölüm başkanınız ile iletişime geçiniz.";
            return View();
        }
        [HttpPost]
        public IActionResult Kontrol()
        {
            var query = _context.Personeller.Include(a => a.Unvan).Where(a => a.TcKimlikNo == "DUMMY_TC_NO" && a.Sifre == "DUMMY_PASSWORD").FirstOrDefault();
            if (query==null)
            {
                return Redirect("/Giris?att=fail");
            }
            else
            {
                HttpContext.Session.SetString("PersonelId", "DUMMY_ID");
                HttpContext.Session.SetString("Id", "DUMMY_ID");
                HttpContext.Session.SetString("kisi", "DUMMY_UNVAN DUMMY_AD DUMMY_SOYAD");
                HttpContext.Session.SetString("yetki", "DUMMY_YETKI");
                HttpContext.Session.SetString("bolumId", "DUMMY_BOLUM_ID");
                ViewBag.Tc = "DUMMY_TC";
                ViewBag.Ad = "DUMMY_AD";
                ViewBag.Yetki = "DUMMY_YETKI";
                ViewBag.BolumId = "DUMMY_BOLUM_ID";
                if (ViewBag.Yetki=="1")
                {
                    return Redirect("/BolumOzet");
                }
                else if (ViewBag.Yetki == "2")
                {
                    return Redirect("/KisiselOzet");
                }
                else if (ViewBag.Yetki == "3")
                {
                    return Redirect("/Ozet");
                }
                else if (ViewBag.Yetki == "4")
                {
                    return Redirect("/EkmekSiparis/Create");
                }
                else if (ViewBag.Yetki == "5")
                {
                    return Redirect("/Ozet");
                }
                else if (ViewBag.Yetki == "6")
                {
                    return Redirect("/BolumIstek");
                }
                return View();
            }
                
        }
    }
}