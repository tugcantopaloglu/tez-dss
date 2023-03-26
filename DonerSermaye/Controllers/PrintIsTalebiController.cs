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
    public class PrintIsTalebiController : Controller
    {
        private readonly donersermayeContext _context;

        public PrintIsTalebiController(donersermayeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int id)
        {
            var dsc = _context.IsTalepleri.Include(a => a.Firma).Where(a => a.Id == id).FirstOrDefault();

            var bl = _context.Bolumler.Where(a => a.Id == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))).FirstOrDefault();

            var pr = _context.Personeller.Include(a => a.Unvan).Where(a => a.Id == Convert.ToInt32(HttpContext.Session.GetString("PersonelId"))).FirstOrDefault();
            
            ViewBag.bolum = bl.Bolum;
            ViewBag.bolumbaskan = pr.Unvan.Unvan + " " + pr.Ad + " " + pr.Soyad;

            return View(dsc);
        }
        
    }
}
