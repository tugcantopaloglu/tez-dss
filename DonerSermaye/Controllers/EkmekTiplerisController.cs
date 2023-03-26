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
    public class EkmekTiplerisController : Controller
    {
        private readonly donersermayeContext _context;

        public EkmekTiplerisController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: EkmekTipleris
        public async Task<IActionResult> Index()
        {
            return View(await _context.EkmekTipleri.ToListAsync());
        }

        // GET: EkmekTipleris/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekmekTipleri = await _context.EkmekTipleri
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ekmekTipleri == null)
            {
                return NotFound();
            }

            return View(ekmekTipleri);
        }

        // GET: EkmekTipleris/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EkmekTipleris/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EkmekTipi,BirimFiyat,Kdv")] EkmekTipleri ekmekTipleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekmekTipleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ekmekTipleri);
        }

        // GET: EkmekTipleris/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekmekTipleri = await _context.EkmekTipleri.SingleOrDefaultAsync(m => m.Id == id);
            if (ekmekTipleri == null)
            {
                return NotFound();
            }
            return View(ekmekTipleri);
        }

        // POST: EkmekTipleris/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EkmekTipi,BirimFiyat,Kdv")] EkmekTipleri ekmekTipleri)
        {
            if (id != ekmekTipleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ekmekTipleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EkmekTipleriExists(ekmekTipleri.Id))
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
            return View(ekmekTipleri);
        }

        // GET: EkmekTipleris/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekmekTipleri = await _context.EkmekTipleri
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ekmekTipleri == null)
            {
                return NotFound();
            }

            return View(ekmekTipleri);
        }

        // POST: EkmekTipleris/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ekmekTipleri = await _context.EkmekTipleri.SingleOrDefaultAsync(m => m.Id == id);
            _context.EkmekTipleri.Remove(ekmekTipleri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EkmekTipleriExists(int id)
        {
            return _context.EkmekTipleri.Any(e => e.Id == id);
        }
    }
}
