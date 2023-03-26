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
    public class KesintitipAyrintiController : Controller
    {
        private readonly donersermayeContext _context;

        public KesintitipAyrintiController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Kesintitipleri
        public async Task<IActionResult> Index()
        {
            return View(await _context.KesintitipAyrinti.ToListAsync());
        }


        // GET: Kesintitipleri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Kesintitipleri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Aciklama")] KesintitipAyrinti kesintitipAyrinti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(kesintitipAyrinti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kesintitipAyrinti);
        }

        // GET: Kesintitipleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintitipleri = await _context.KesintitipAyrinti.SingleOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Aciklama")] KesintitipAyrinti kesintitipAyrinti)
        {
            if (id != kesintitipAyrinti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kesintitipAyrinti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KesintitipleriExists(kesintitipAyrinti.Id))
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
            return View(kesintitipAyrinti);
        }

        // GET: Kesintitipleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kesintitipleri = await _context.KesintitipAyrinti
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
            var kesintitipleri = await _context.KesintitipAyrinti.SingleOrDefaultAsync(m => m.Id == id);
            _context.KesintitipAyrinti.Remove(kesintitipleri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KesintitipleriExists(int id)
        {
            return _context.KesintitipAyrinti.Any(e => e.Id == id);
        }
    }
}
