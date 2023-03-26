using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using System.Globalization;

namespace DonerSermaye.Controllers
{
    public class FirmaGoruntulemeController : Controller
    {
        private readonly donersermayeContext _context;

        public FirmaGoruntulemeController(donersermayeContext context)
        {
            _context = context;
        }
      
        public async Task<IActionResult> Index()
        {
            return View();
        }

        public IActionResult Detay1(string vergino)
        {
            var query = (from a in _context.Isler
                         join b in _context.Firmalar on a.FirmaId equals b.Id
                         where (b.VergiNo == vergino)
                         select a).Include(t1 => t1.Firma).Include(t2 => t2.Bolum).ToList();
            return View(query);
        }
    }
}
