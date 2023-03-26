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
    public class ddddddController : Controller
    {
        private readonly donersermayeContext _context;

        public ddddddController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: dddddd
        public async Task<IActionResult> Index()
        {
            return View(await _context.Raporlar.ToListAsync());
        }

        // GET: dddddd/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raporlar = await _context.Raporlar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (raporlar == null)
            {
                return NotFound();
            }

            return View(raporlar);
        }

        // GET: dddddd/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: dddddd/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Rapor")] Raporlar raporlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(raporlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(raporlar);
        }

        // GET: dddddd/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raporlar = await _context.Raporlar.SingleOrDefaultAsync(m => m.Id == id);
            if (raporlar == null)
            {
                return NotFound();
            }
            return View(raporlar);
        }

        // POST: dddddd/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Rapor")] Raporlar raporlar)
        {
            if (id != raporlar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(raporlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RaporlarExists(raporlar.Id))
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
            return View(raporlar);
        }

        // GET: dddddd/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var raporlar = await _context.Raporlar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (raporlar == null)
            {
                return NotFound();
            }

            return View(raporlar);
        }

        // POST: dddddd/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var raporlar = await _context.Raporlar.SingleOrDefaultAsync(m => m.Id == id);
            _context.Raporlar.Remove(raporlar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RaporlarExists(int id)
        {
            return _context.Raporlar.Any(e => e.Id == id);
        }
    }
}
