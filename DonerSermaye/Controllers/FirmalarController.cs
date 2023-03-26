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
    public class FirmalarController : Controller
    {
        private readonly donersermayeContext _context;

        public FirmalarController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Firmalar
        public async Task<IActionResult> Index(string FirmaFiltre)
        {
            var firma = await _context.Firmalar.ToListAsync();
            if (FirmaFiltre != null)
            {
                ViewBag.FirmaArama = FirmaFiltre;
                firma = await _context.Firmalar.Where(a => a.FirmaAdi.Contains(FirmaFiltre) || a.VergiNo.Contains(FirmaFiltre)).ToListAsync();
            }

            return View(firma);
        }

        // GET: Firmalar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmalar = await _context.Firmalar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (firmalar == null)
            {
                return NotFound();
            }

            return View(firmalar);
        }

        // GET: Firmalar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Firmalar/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirmaAdi,VergiDairesi,VergiNo,TcKimlikNo")] Firmalar firmalar)
        {
            if (ModelState.IsValid)
            {
                _context.Add(firmalar);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch
                {
                    ViewData["hata"] = "Firma eklenirken bir hata oluştu. Bu vergi numarasına ait bir firma daha önce eklenmiş!";
                    return View(firmalar);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(firmalar);
        }

        // GET: Firmalar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmalar = await _context.Firmalar.SingleOrDefaultAsync(m => m.Id == id);
            if (firmalar == null)
            {
                return NotFound();
            }
            return View(firmalar);
        }

        // POST: Firmalar/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirmaAdi,VergiDairesi,VergiNo,TcKimlikNo")] Firmalar firmalar)
        {
            if (id != firmalar.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(firmalar);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FirmalarExists(firmalar.Id))
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
            return View(firmalar);
        }

        // GET: Firmalar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var firmalar = await _context.Firmalar
                .SingleOrDefaultAsync(m => m.Id == id);
            if (firmalar == null)
            {
                return NotFound();
            }

            return View(firmalar);
        }

        // POST: Firmalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var firmalar = await _context.Firmalar.SingleOrDefaultAsync(m => m.Id == id);
            _context.Firmalar.Remove(firmalar);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FirmalarExists(int id)
        {
            return _context.Firmalar.Any(e => e.Id == id);
        }
    }
}
