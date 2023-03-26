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
    public class HesaplarController : Controller
    {
        private readonly donersermayeContext _context;

        public HesaplarController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Hesaplar
        public async Task<IActionResult> Index()
        {

            return View(await _context.Hesaplar.Where(a => !a.Hesap.Contains("ARGE") ).ToListAsync());
        }

        // GET: Hesaplar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hesaplar = await _context.Hesaplar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hesaplar == null)
            {
                return NotFound();
            }

            return View(hesaplar);
        }

        // GET: Hesaplar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Hesaplar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Hesap")] Hesaplar hesaplar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hesaplar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hesaplar);
        }

        // GET: Hesaplar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hesaplar = await _context.Hesaplar.SingleOrDefaultAsync(m => m.Id == id);
            if (hesaplar == null)
            {
                return NotFound();
            }
            return View(hesaplar);
        }

        // POST: Hesaplar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Hesap")] Hesaplar hesaplar)
        {
            if (id != hesaplar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hesaplar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HesaplarExists(hesaplar.Id))
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
            return View(hesaplar);
        }

        // GET: Hesaplar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hesaplar = await _context.Hesaplar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (hesaplar == null)
            {
                return NotFound();
            }

            return View(hesaplar);
        }

        // POST: Hesaplar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hesaplar = await _context.Hesaplar.SingleOrDefaultAsync(m => m.Id == id);
            _context.Hesaplar.Remove(hesaplar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HesaplarExists(int id)
        {
            return _context.Hesaplar.Any(e => e.Id == id);
        }
    }
}
