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
    public class UnvanlarController : Controller
    {
        private readonly donersermayeContext _context;

        public UnvanlarController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Unvanlar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Unvanlar.ToListAsync());
        }

        // GET: Unvanlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unvanlar = await _context.Unvanlar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unvanlar == null)
            {
                return NotFound();
            }

            return View(unvanlar);
        }

        // GET: Unvanlar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Unvanlar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Unvan")] Unvanlar unvanlar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unvanlar);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(unvanlar);
        }

        // GET: Unvanlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unvanlar = await _context.Unvanlar.SingleOrDefaultAsync(m => m.Id == id);
            if (unvanlar == null)
            {
                return NotFound();
            }
            return View(unvanlar);
        }

        // POST: Unvanlar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Unvan")] Unvanlar unvanlar)
        {
            if (id != unvanlar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unvanlar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnvanlarExists(unvanlar.Id))
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
            return View(unvanlar);
        }

        // GET: Unvanlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unvanlar = await _context.Unvanlar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unvanlar == null)
            {
                return NotFound();
            }

            return View(unvanlar);
        }

        // POST: Unvanlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unvanlar = await _context.Unvanlar.SingleOrDefaultAsync(m => m.Id == id);
            _context.Unvanlar.Remove(unvanlar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UnvanlarExists(int id)
        {
            return _context.Unvanlar.Any(e => e.Id == id);
        }
    }
}
