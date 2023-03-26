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
    public class KesintitipleriController : Controller
    {
        private readonly donersermayeContext _context;

        public KesintitipleriController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Kesintitipleri
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kesintitipleri.Include(x => x.kesintiTipAyrinti).ToListAsync());
        }


        // GET: Kesintitipleri/Create
        public IActionResult Create()
        {
            ViewData["kesintiTipAyrintiId"] = new SelectList(_context.KesintitipAyrinti, "Id", "Aciklama");
            return View();
        }

        // POST: Kesintitipleri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,kesintiTipAyrintiId,KesintiTipi")] Kesintitipleri kesintitipleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kesintitipleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kesintitipleri);
        }

        // GET: Kesintitipleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintitipleri = await _context.Kesintitipleri.SingleOrDefaultAsync(m => m.Id == id);
            ViewData["kesintiTipAyrintiId"] = new SelectList(_context.KesintitipAyrinti, "Id", "Aciklama", kesintitipleri.kesintiTipAyrinti);
            if (kesintitipleri == null)
            {
                return NotFound();
            }
            return View(kesintitipleri);
        }

        // POST: Kesintitipleri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,kesintiTipAyrintiId,KesintiTipi")] Kesintitipleri kesintitipleri)
        {
            if (id != kesintitipleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kesintitipleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KesintitipleriExists(kesintitipleri.Id))
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
            return View(kesintitipleri);
        }

        // GET: Kesintitipleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintitipleri = await _context.Kesintitipleri
                .SingleOrDefaultAsync(m => m.Id == id);
            if (kesintitipleri == null)
            {
                return NotFound();
            }

            return View(kesintitipleri);
        }

        // POST: Kesintitipleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kesintitipleri = await _context.Kesintitipleri.SingleOrDefaultAsync(m => m.Id == id);
            _context.Kesintitipleri.Remove(kesintitipleri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KesintitipleriExists(int id)
        {
            return _context.Kesintitipleri.Any(e => e.Id == id);
        }
    }
}
