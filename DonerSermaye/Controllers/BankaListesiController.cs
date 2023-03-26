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
    public class BankaListesiController : Controller
    {
        private readonly donersermayeContext _context;

        public BankaListesiController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: BankaListesi
        public async Task<IActionResult> Index()
        {
            
           

         



           var donersermayeContext = _context.IsAyrinti.Include(i => i.Personel).Include(i => i.ıs);
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: BankaListesi/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAyrinti = await _context.IsAyrinti
                .Include(i => i.Personel)
                .Include(i => i.ıs)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isAyrinti == null)
            {
                return NotFound();
            }

            return View(isAyrinti);
        }

        // GET: BankaListesi/Create
        public IActionResult Create()
        {
            ViewData["PersonelId"] = new SelectList(_context.Personeller, "Id", "Id");
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id");
            return View();
        }

        // POST: BankaListesi/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ısId,PersonelId,Tutar")] IsAyrinti isAyrinti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isAyrinti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonelId"] = new SelectList(_context.Personeller, "Id", "Id", isAyrinti.PersonelId);
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id", isAyrinti.ısId);
            return View(isAyrinti);
        }

        // GET: BankaListesi/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAyrinti = await _context.IsAyrinti.SingleOrDefaultAsync(m => m.Id == id);
            if (isAyrinti == null)
            {
                return NotFound();
            }
            ViewData["PersonelId"] = new SelectList(_context.Personeller, "Id", "Id", isAyrinti.PersonelId);
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id", isAyrinti.ısId);
            return View(isAyrinti);
        }

        // POST: BankaListesi/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ısId,PersonelId,Tutar")] IsAyrinti isAyrinti)
        {
            if (id != isAyrinti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isAyrinti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsAyrintiExists(isAyrinti.Id))
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
            ViewData["PersonelId"] = new SelectList(_context.Personeller, "Id", "Id", isAyrinti.PersonelId);
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id", isAyrinti.ısId);
            return View(isAyrinti);
        }

        // GET: BankaListesi/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAyrinti = await _context.IsAyrinti
                .Include(i => i.Personel)
                .Include(i => i.ıs)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isAyrinti == null)
            {
                return NotFound();
            }

            return View(isAyrinti);
        }

        // POST: BankaListesi/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isAyrinti = await _context.IsAyrinti.SingleOrDefaultAsync(m => m.Id == id);
            _context.IsAyrinti.Remove(isAyrinti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsAyrintiExists(int id)
        {
            return _context.IsAyrinti.Any(e => e.Id == id);
        }
    }
}
