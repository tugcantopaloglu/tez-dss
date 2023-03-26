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
            var query = _context.Personeller.Include(a => a.Unvan).Where(a => a.TcKimlikNo == Request.Form["tc"] && a.Sifre == Request.Form["sifre"]).FirstOrDefault();
            if (query==null)
            {
                return Redirect("/Giris?att=fail");
            }
            else
            {
                HttpContext.Session.SetString("PersonelId",(query.Id).ToString());
                HttpContext.Session.SetString("Id", (query.Id).ToString());
                HttpContext.Session.SetString("kisi",query.Unvan.Unvan + " " + query.Ad+" "+query.Soyad);
                HttpContext.Session.SetString("yetki", (query.YetkiId).ToString());
                HttpContext.Session.SetString("bolumId", (query.BolumId).ToString());
                ViewBag.Tc = HttpContext.Session.GetString("tc");
                ViewBag.Ad = HttpContext.Session.GetString("kisi");
                ViewBag.Yetki = HttpContext.Session.GetString("yetki");
                ViewBag.BolumId = HttpContext.Session.GetString("bolumId");
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
                return View();
            }
                
        }
    }
}