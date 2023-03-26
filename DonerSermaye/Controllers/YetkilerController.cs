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
    public class YetkilerController : Controller
    {
        private readonly donersermayeContext _context;

        public YetkilerController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Yetkiler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Yetkiler.ToListAsync());
        }

        // GET: Yetkiler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yetkiler = await _context.Yetkiler
                .SingleOrDefaultAsync(m => m.Id == id);
            if (yetkiler == null)
            {
                return NotFound();
            }

            return View(yetkiler);
        }

        // GET: Yetkiler/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Yetkiler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Yetki")] Yetkiler yetkiler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yetkiler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(yetkiler);
        }

        // GET: Yetkiler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yetkiler = await _context.Yetkiler.SingleOrDefaultAsync(m => m.Id == id);
            if (yetkiler == null)
            {
                return NotFound();
            }
            return View(yetkiler);
        }

        // POST: Yetkiler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Yetki")] Yetkiler yetkiler)
        {
            if (id != yetkiler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yetkiler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YetkilerExists(yetkiler.Id))
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
            return View(yetkiler);
        }

        // GET: Yetkiler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yetkiler = await _context.Yetkiler
                .SingleOrDefaultAsync(m => m.Id == id);
            if (yetkiler == null)
            {
                return NotFound();
            }

            return View(yetkiler);
        }

        // POST: Yetkiler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yetkiler = await _context.Yetkiler.SingleOrDefaultAsync(m => m.Id == id);
            _context.Yetkiler.Remove(yetkiler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YetkilerExists(int id)
        {
            return _context.Yetkiler.Any(e => e.Id == id);
        }
    }
}
