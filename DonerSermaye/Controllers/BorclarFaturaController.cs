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
    public class BorclarFaturaController : Controller
    {
        private readonly donersermayeContext _context;

        public BorclarFaturaController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: BorclarFatura
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.FaturaTipiNavigation).Include(i => i.Firma).Include(i => i.Hesap).Include(i => i.Tur).Where(a=>a.FaturaTipi==2);
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: BorclarFatura/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = await _context.Isler
                .Include(i => i.Bolum)
                .Include(i => i.FaturaTipiNavigation)
                .Include(i => i.Firma)
                .Include(i => i.Hesap)
                .Include(i => i.Tur)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }

            return View(isler);
        }

        // GET: BorclarFatura/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "Id");
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id");
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id");
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id");
            return View();
        }

        // POST: BorclarFatura/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTipi,FaturaTarihi,FaturaNo,Durum,HesapId,Dagitim,Lab")] Isler isler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "Id", isler.FaturaTipi);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id", isler.HesapId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        // GET: BorclarFatura/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = await _context.Isler.SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }
           
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "FaturaTipi1", isler.FaturaTipi);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id", isler.HesapId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
           
            return View(isler);
        }

        // POST: BorclarFatura/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTipi,FaturaTarihi,FaturaNo,Durum,Dagitim,Lab")] Isler isler)
        {
            if (id != isler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (isler.FaturaTipi==1)
                    {
                        isler.Dagitim = 1;
                    }
                    else if (isler.FaturaTipi==2)
                    {
                        isler.Dagitim = null;
                    }
                    _context.Update(isler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslerExists(isler.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "Id", isler.FaturaTipi);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id", isler.HesapId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);

            return View(isler);
        }

        // GET: BorclarFatura/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = await _context.Isler
                .Include(i => i.Bolum)
                .Include(i => i.FaturaTipiNavigation)
                .Include(i => i.Firma)
                .Include(i => i.Hesap)
                .Include(i => i.Tur)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }

            return View(isler);
        }

        // POST: BorclarFatura/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isler = await _context.Isler.SingleOrDefaultAsync(m => m.Id == id);
            _context.Isler.Remove(isler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslerExists(int id)
        {
            return _context.Isler.Any(e => e.Id == id);
        }
    }
}
