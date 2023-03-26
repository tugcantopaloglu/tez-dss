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
    public class BolumlerController : Controller
    {
        private readonly donersermayeContext _context;

        public BolumlerController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Bolumler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bolumler.Include(a => a.KesintitipAyrinti).ToListAsync());
        }

        // GET: Bolumler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolumler = await _context.Bolumler
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bolumler == null)
            {
                return NotFound();
            }

            return View(bolumler);
        }

        // GET: Bolumler/Create
        public IActionResult Create()
        {
            ViewData["kesintiTipAyrintiId"] = new SelectList(_context.KesintitipAyrinti, "Id", "Aciklama");
            return View();
        }

        // POST: Bolumler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Bolum,ilkdevirtutari,KesintitipAyrintiId")] Bolumler bolumler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bolumler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(bolumler);
        }

        // GET: Bolumler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolumler = await _context.Bolumler.SingleOrDefaultAsync(m => m.Id == id);

            ViewData["kesintiTipAyrintiId"] = new SelectList(_context.KesintitipAyrinti, "Id", "Aciklama", bolumler.KesintitipAyrinti);
            if (bolumler == null)
            {
                return NotFound();
            }
            return View(bolumler);
        }

        // POST: Bolumler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Bolum,ilkdevirtutari,KesintitipAyrintiId")] Bolumler bolumler)
        {
            if (id != bolumler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bolumler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BolumlerExists(bolumler.Id))
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
            return View(bolumler);
        }

        // GET: Bolumler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bolumler = await _context.Bolumler
                .SingleOrDefaultAsync(m => m.Id == id);
            if (bolumler == null)
            {
                return NotFound();
            }

            return View(bolumler);
        }

        // POST: Bolumler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bolumler = await _context.Bolumler.SingleOrDefaultAsync(m => m.Id == id);
            _context.Bolumler.Remove(bolumler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BolumlerExists(int id)
        {
            return _context.Bolumler.Any(e => e.Id == id);
        }
    }
}
