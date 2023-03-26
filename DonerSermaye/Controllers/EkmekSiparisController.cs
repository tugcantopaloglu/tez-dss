using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;

namespace DonerSermaye.Controllers
{
    public class EkmekSiparisController : Controller
    {
        private readonly donersermayeContext _context;

        public EkmekSiparisController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: EkmekSiparis
        public IActionResult Index(int? FirmaId, DateTime? BaslamaTarihi, DateTime? BitisTarihi)
        {


            var query = _context.EkmekSiparis;
            var toplam = query.Sum(d => d.ToplamFiyat);
            ViewBag.toplam = toplam;
            var donersermayeContext = from b in _context.EkmekSiparis
                                      join d in _context.EkmekTipleri on b.EkmekTipleriId equals d.Id
                                      join e in _context.Firmalar on b.FirmaId equals e.Id select new EkmekSiparisViewModel {
                                      Id = b.Id,
                                      EkmekTipi = d.EkmekTipi,
                                      FirmaAdi = e.FirmaAdi,
                                      FirmaId = b.FirmaId,
                                      Adet = b.Adet,
                                      TeslimTarihi = b.TeslimTarihi,
                                      ToplamFiyat = b.ToplamFiyat
                                      };

            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            if (FirmaId != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (BaslamaTarihi != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.TeslimTarihi >= BaslamaTarihi);
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.TeslimTarihi <= BitisTarihi);
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }
            var tls = donersermayeContext.ToList();
            return View(tls);
        }

        // GET: EkmekSiparis/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekmekSiparis = await _context.EkmekSiparis
                .Include(e => e.EkmekTipleri)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ekmekSiparis == null)
            {
                return NotFound();
            }

            return View(ekmekSiparis);
        }

        // GET: EkmekSiparis/Create
        public IActionResult Create()
        {
            ViewData["EkmekTipi"] = new SelectList(_context.EkmekTipleri, "Id", "EkmekTipi");
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View();
        }

        // POST: EkmekSiparis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiparisTarihi,EkmekTipi,FirmaId,Adet,ToplamFiyat,TeslimTarihi")] EkmekSiparis ekmekSiparis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ekmekSiparis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EkmekTipi"] = new SelectList(_context.EkmekTipleri, "Id", "EkmekTipi", ekmekSiparis.EkmekTipleriId);
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View(ekmekSiparis);
        }

        // GET: EkmekSiparis/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekmekSiparis = await _context.EkmekSiparis.SingleOrDefaultAsync(m => m.Id == id);
            if (ekmekSiparis == null)
            {
                return NotFound();
            }
            ViewData["EkmekTipi"] = new SelectList(_context.EkmekTipleri, "Id", "EkmekTipi", ekmekSiparis.EkmekTipleriId);
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View(ekmekSiparis);
        }

        // POST: EkmekSiparis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiparisTarihi,EkmekTipi,FirmaId,Adet,ToplamFiyat,TeslimTarihi")] EkmekSiparis ekmekSiparis)
        {
            if (id != ekmekSiparis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ekmekSiparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EkmekSiparisExists(ekmekSiparis.Id))
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
            ViewData["EkmekTipi"] = new SelectList(_context.EkmekTipleri, "Id", "EkmekTipi", ekmekSiparis.EkmekTipleriId);
            ViewData["Firmalar"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View(ekmekSiparis);
        }

        // GET: EkmekSiparis/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ekmekSiparis = await _context.EkmekSiparis
                .Include(e => e.EkmekTipleri)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ekmekSiparis == null)
            {
                return NotFound();
            }

            return View(ekmekSiparis);
        }

        // POST: EkmekSiparis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ekmekSiparis = await _context.EkmekSiparis.SingleOrDefaultAsync(m => m.Id == id);
            _context.EkmekSiparis.Remove(ekmekSiparis);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EkmekSiparisExists(int id)
        {
            return _context.EkmekSiparis.Any(e => e.Id == id);
        }

      public JsonResult BirimFiyatGetir(int id)
        {
         
            var query = _context.EkmekTipleri.Where(a => a.Id == id);


            return Json(query);

        }
        [HttpPost]
        public int Kaydet(EkmekSiparis[] e_veri)
        {
            foreach (var item in e_veri)
            {
                EkmekSiparis siparis = new EkmekSiparis(); 
                siparis.EkmekTipleriId = item.EkmekTipleriId;                
                siparis.TeslimTarihi = item.TeslimTarihi;
                siparis.Adet = item.Adet;
                siparis.ToplamFiyat = item.ToplamFiyat;
                siparis.SiparisTarihi = DateTime.Now;
                siparis.FirmaId = item.FirmaId;
                _context.EkmekSiparis.Add(siparis);
                _context.SaveChanges();    
            }
            return 1;
        }
    }
}
