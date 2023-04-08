using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using DonerSermaye.Models.Service;

namespace DonerSermaye.Controllers
{
    public class IstekOnayController : Controller
    {
        private readonly donersermayeContext _context;

        public IstekOnayController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekOnay
        public async Task<IActionResult> Index(int? FirmaId, int? BolumId)
        {
            var donersermayeContext = _context.Istekler.Include(i => i.Bolum).Include(i => i.TurNoNavigation).Where(a => a.BolumId == a.BolumId);

            if (FirmaId != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (BolumId != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.BolumId == BolumId);
                ViewData["Filtre"] = "filtre";
            }

            donersermayeContext = donersermayeContext.OrderByDescending(a => a.IstekNo);

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);

            return View(await donersermayeContext.ToListAsync());
        }

        public async Task<IActionResult> Incele(int? BolumId)
        {
            decimal bekleyenisYaklasikMaaliyetHesaplama = 0;
            IstekOnayInceleViewModel model = new IstekOnayInceleViewModel();

            if (BolumId.HasValue)
            {
                GenelDurumService genelDurum = new GenelDurumService(_context);
                var bolum = genelDurum.Bolum(BolumId.GetValueOrDefault(0));
                model.Kalan = bolum.Kalan.GetValueOrDefault(0);
                model.Bolum = bolum.Bolum;
            }
            var istekler = _context.Istekler.Where(i =>i.BolumId == BolumId && i.FaturaNo == null && i.Onay == 1).ToList();
            foreach (var istek in istekler)
            {
                bekleyenisYaklasikMaaliyetHesaplama += istek.YaklasikMaliyet.GetValueOrDefault(0);
            }

            model.BekleyenYaklasikMaliyet = bekleyenisYaklasikMaaliyetHesaplama;

            return View(model);
        }

        // GET: IstekOnay/Onayla/5
        public async  Task<IActionResult> Onayla(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekler =  _context.Istekler.Where(i => i.IstekNo == id).FirstOrDefault();
            istekler.Onay =1;
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: IstekOnay/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo");
            return View();
        }

        // POST: IstekOnay/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IstekNo,Konu,Ozet,TurNo,OnayNo,BolumId")] Istekler istekler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(istekler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", istekler.BolumId);
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
            return View(istekler);
        }

        // GET: IstekOnay/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekler = await _context.Istekler.SingleOrDefaultAsync(m => m.IstekNo == id);
            if (istekler == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", istekler.BolumId);
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
            return View(istekler);
        }

        // POST: IstekOnay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IstekNo,Konu,Ozet,TurNo,OnayNo,BolumId")] Istekler istekler)
        {
            if (id != istekler.IstekNo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(istekler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsteklerExists(istekler.IstekNo))
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
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", istekler.BolumId);
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
            return View(istekler);
        }

        // GET: IstekOnay/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekler = await _context.Istekler
                .Include(i => i.Bolum)
                .Include(i => i.TurNoNavigation)
                .SingleOrDefaultAsync(m => m.IstekNo == id);
            if (istekler == null)
            {
                return NotFound();
            }

            return View(istekler);
        }

        // POST: IstekOnay/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var istekler = await _context.Istekler.SingleOrDefaultAsync(m => m.IstekNo == id);
            _context.Istekler.Remove(istekler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsteklerExists(int id)
        {
            return _context.Istekler.Any(e => e.IstekNo == id);
        }
    }
}
