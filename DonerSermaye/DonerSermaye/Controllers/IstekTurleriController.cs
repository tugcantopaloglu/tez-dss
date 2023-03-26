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
    public class IstekTurleriController : Controller
    {
        private readonly donersermayeContext _context;

        public IstekTurleriController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekTurleri
        public async Task<IActionResult> Index()
        {
            return View(await _context.IstekTurleri.ToListAsync());
        }

        // GET: IstekTurleri/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekTurleri = await _context.IstekTurleri
                .SingleOrDefaultAsync(m => m.TurNo == id);
            if (istekTurleri == null)
            {
                return NotFound();
            }

            return View(istekTurleri);
        }

        // GET: IstekTurleri/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: IstekTurleri/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TurNo,Tur")] IstekTurleri istekTurleri)
        {
            if (ModelState.IsValid)
            {
                _context.Add(istekTurleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(istekTurleri);
        }

        // GET: IstekTurleri/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekTurleri = await _context.IstekTurleri.SingleOrDefaultAsync(m => m.TurNo == id);
            if (istekTurleri == null)
            {
                return NotFound();
            }
            return View(istekTurleri);
        }

        // POST: IstekTurleri/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TurNo,Tur")] IstekTurleri istekTurleri)
        {
            if (id != istekTurleri.TurNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(istekTurleri);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IstekTurleriExists(istekTurleri.TurNo))
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
            return View(istekTurleri);
        }

        // GET: IstekTurleri/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekTurleri = await _context.IstekTurleri
                .SingleOrDefaultAsync(m => m.TurNo == id);
            if (istekTurleri == null)
            {
                return NotFound();
            }

            return View(istekTurleri);
        }

        // POST: IstekTurleri/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var istekTurleri = await _context.IstekTurleri.SingleOrDefaultAsync(m => m.TurNo == id);
            _context.IstekTurleri.Remove(istekTurleri);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IstekTurleriExists(int id)
        {
            return _context.IstekTurleri.Any(e => e.TurNo == id);
        }
    }
}
