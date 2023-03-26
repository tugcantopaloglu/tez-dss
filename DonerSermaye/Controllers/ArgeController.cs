using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DonerSermaye.Controllers
{
    public class ArgeController : Controller
    {
        private readonly donersermayeContext _context;

        public ArgeController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Duyuru
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Arge.Include(a => a.ArgePersonel).ThenInclude(a => a.Personel).ThenInclude(a => a.Unvan).Where(a => a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")));

            return View(await donersermayeContext.ToListAsync());
        }

        // GET: Duyuru
        public async Task<IActionResult> Index2(int? FirmaId, int? BolumId, DateTime? BaslamaTarihi, DateTime? BitisTarihi, string ArgeNo)
        {
            var query2 = await _context.Arge.Include(a => a.Firma).Include(a => a.Personel).ThenInclude(a => a.Bolum).Include(a => a.ArgePersonel).ThenInclude(a => a.Personel).ThenInclude(a => a.Unvan).ToListAsync();
            var query = query2.Where(a => a.Id > 0);
            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (BolumId != null)
            {
                query = query.Where(a => a.Personel.BolumId == BolumId);
                ViewData["Filtre"] = "filtre";
            }
            if (ArgeNo != null)
            {
                query = query.Where(a => a.ArgeNo == ArgeNo);
                ViewData["ArgeNo"] = ArgeNo;
                ViewData["Filtre"] = "filtre";
            }

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);

            return View(query);
        }

        // GET: Duyuru
        public async Task<IActionResult> Index3(int? FirmaId, DateTime? BaslamaTarihi, DateTime? BitisTarihi, string ArgeNo)
        {
            var query2 = await _context.Arge.Include(a => a.Firma).Include(a => a.Personel).ThenInclude(a => a.Bolum).Include(a => a.ArgePersonel).ThenInclude(a => a.Personel).ThenInclude(a => a.Unvan).ToListAsync();
            var query = query2.Where(a => a.Id > 0);
            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            query = query.Where(a => a.Personel.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")));
            
            if (ArgeNo != null)
            {
                query = query.Where(a => a.ArgeNo == ArgeNo);
                ViewData["ArgeNo"] = ArgeNo;
                ViewData["Filtre"] = "filtre";
            }

            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);

            return View(query);
        }
        // GET: Duyuru/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arge = await _context.Arge
                .SingleOrDefaultAsync(m => m.Id == id);
            if (arge == null)
            {
                return NotFound();
            }

            return View(arge);
        }

        // GET: Duyuru/Create
        public IActionResult Create()
        {
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View();
        }

        // POST: Duyuru/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ArgeNo,Aciklama,ProjeTutari,Baslama,Bitis,FirmaId")] Arge arge, IFormFile argedosyasi)
        {
            if (ModelState.IsValid)
            {
                if (argedosyasi != null)
                {
                    var path = Path.Combine(
                      Directory.GetCurrentDirectory(), "wwwroot", "argeler",
                      arge.ArgeNo.ToString() + ".pdf");
                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await argedosyasi.CopyToAsync(fileStream);
                    }
                }

                _context.Add(arge);
                arge.PersonelId = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return RedirectToAction(nameof(Index));
        }

        // GET: Duyuru/Edit/5
        public async Task<IActionResult> AddPerson(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arge = await _context.Arge.SingleOrDefaultAsync(m => m.Id == id);
            if (arge == null)
            {
                return NotFound();
            }
            var p = _context.Personeller.Include(a => a.Unvan).Where(a => a.BolumId != 1).Select(s => new { Id = s.Id, Ad = string.Format("{0} {1} {2}", s.Unvan.Unvan, s.Ad, s.Soyad) }).ToList();

            ViewData["Personeller"] = new MultiSelectList(p, "Id", "Ad", _context.ArgePersonel.Where(a => a.ArgeId == id).Select(a=>a.PersonelId).ToList() );
            return View(arge);
        } // POST: Duyuru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddPerson(int id, List<int> PersonelListesi)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    List<ArgePersonel> argep = _context.ArgePersonel.Where(a => a.ArgeId == id).ToList();
                    _context.RemoveRange(argep);
                    foreach(var pl in PersonelListesi)
                    {
                        ArgePersonel ap = new ArgePersonel();
                        ap.ArgeId = id;
                        ap.PersonelId = pl;
                        _context.Add(ap);
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(Index));
        }
        // GET: Duyuru/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var arge = await _context.Arge.SingleOrDefaultAsync(m => m.Id == id);
            if (arge == null)
            {
                return NotFound();
            }
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View(arge);
        }

        // POST: Duyuru/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ArgeNo,Aciklama,ProjeTutari,Baslama,Bitis,FirmaId,PersonelId")] Arge arge, IFormFile argedosyasi)
        {
            if (id != arge.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (argedosyasi != null)
                {
                    var path = Path.Combine(
                      Directory.GetCurrentDirectory(), "wwwroot", "argeler",
                      arge.ArgeNo.ToString() + ".pdf");
                    using (Stream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await argedosyasi.CopyToAsync(fileStream);
                    }
                }

                try
                {
                    _context.Update(arge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return RedirectToAction(nameof(Index));
        }

        // GET: Duyuru/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var duyurular = await _context.Arge
                .SingleOrDefaultAsync(m => m.Id == id);
            if (duyurular == null)
            {
                return NotFound();
            }

            return View(duyurular);
        }

        // POST: Duyuru/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var arge = await _context.Arge.SingleOrDefaultAsync(m => m.Id == id);
            _context.Arge.Remove(arge);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
