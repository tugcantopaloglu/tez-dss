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
    public class IsTurleriController : Controller
    {
        private readonly donersermayeContext _context;

        public IsTurleriController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IsTurleri
        public async Task<IActionResult> Index()
        {
           

         return View(await _context.IsTurleri.ToListAsync());
        }

        // GET: IsTurleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isTurleri = await _context.IsTurleri
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isTurleri == null)
            {
                return NotFound();
            }

            return View(isTurleri);
        }

        // GET: IsTurleri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IsTurleri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tur")] IsTurleri isTurleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isTurleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(isTurleri);
        }

        // GET: IsTurleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isTurleri = await _context.IsTurleri.SingleOrDefaultAsync(m => m.Id == id);
            if (isTurleri == null)
            {
                return NotFound();
            }
            return View(isTurleri);
        }

        // POST: IsTurleri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tur")] IsTurleri isTurleri)
        {
            if (id != isTurleri.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isTurleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsTurleriExists(isTurleri.Id))
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
            return View(isTurleri);
        }

        // GET: IsTurleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isTurleri = await _context.IsTurleri
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isTurleri == null)
            {
                return NotFound();
            }

            return View(isTurleri);
        }

        // POST: IsTurleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isTurleri = await _context.IsTurleri.SingleOrDefaultAsync(m => m.Id == id);
            _context.IsTurleri.Remove(isTurleri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsTurleriExists(int id)
        {
            return _context.IsTurleri.Any(e => e.Id == id);
        }
    }
}
