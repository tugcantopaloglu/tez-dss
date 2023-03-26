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
    public class PrintController : Controller
    {
        private readonly donersermayeContext _context;

        public PrintController(donersermayeContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int IsId, int PrintTuru)
        {
            var isler = _context.Isler.Include(a => a.Bolum).Include(a => a.Tur).Include(a => a.Firma).Include(a => a.IsAyrinti).ThenInclude(a => a.Personel).ThenInclude(a => a.Unvan).Where(a => a.Id == IsId).OrderBy(a => a.Personel.Unvan.Id).FirstOrDefault();
            
            return View(isler);
        }
        
    }
}
