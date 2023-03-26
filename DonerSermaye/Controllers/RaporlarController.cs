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
    public class RaporlarController : Controller
    {
        private readonly donersermayeContext _context;

        public RaporlarController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Raporlar
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Hesap).Include(i => i.Tur);
            return View(await donersermayeContext.ToListAsync());
        }
        

        private bool IslerExists(int id)
        {
            return _context.Isler.Any(e => e.Id == id);
        }
    }
}
