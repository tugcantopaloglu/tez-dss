using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace DonerSermaye.Controllers
{
    public class IstekGirController : Controller
    {
        private readonly donersermayeContext _context;

        public IstekGirController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> Index(int? FirmaId, int? BolumId, int? IsTurId, DateTime? BaslamaTarihi, DateTime? BitisTarihi, int? isid)
        {
            if(isid != null)
            {
                ViewBag.isid = isid;
            }
            else
            {
                ViewBag.isid = 0;
            }
            var donersermayeContext = _context.Istekler.Include(i => i.Bolum).Include(i => i.TurNoNavigation).Where(i=>i.Onay==1);
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
            if (IsTurId != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.TurNo == IsTurId);
                ViewData["Filtre"] = "filtre";
            }
            if (BaslamaTarihi != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.FaturaTarihi >= BaslamaTarihi);
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                donersermayeContext = donersermayeContext.Where(a => a.FaturaTarihi <= BitisTarihi);
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }

            donersermayeContext = donersermayeContext.OrderByDescending(a => a.IstekNo);

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "Tur", IsTurId);
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: IstekGir/Details/5
        public async Task<IActionResult> Details(int? id)
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

        // GET: IstekGir/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo");

            return View();
        }
        public IActionResult CreateB()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo");
            ViewData["Bolum"] = new SelectList(_context.Bolumler, "Id", "Bolum");
            ViewData["Tur"] = new SelectList(_context.IstekTurleri, "TurNo", "Tur");
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            return View();
        }
        // POST: IstekGir/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IstekNo,Konu,Ozet,YaklasikMaliyet,TurNo,OnayNo,BolumId,Onay,Fiyat,FaturaTarihi,FaturaNo,eFaturami,TahakkukNo")] Istekler istekler)
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
        // GET: IstekGir/Edit/5
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
            ViewData["Bolum"] = new SelectList(_context.Bolumler, "Id", "Bolum", istekler.BolumId);
            ViewData["Tur"] = new SelectList(_context.IstekTurleri, "TurNo", "Tur", istekler.TurNo);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", istekler.FirmaId);
            return View(istekler);
        }
        // POST: IstekGir/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IstekNo,Konu,YaklasikMaliyet,Ozet,TurNo,OnayNo,FirmaId,BolumId,Onay,Fiyat,FaturaTarihi,FaturaNo,eFaturami,TahakkukNo")] Istekler istekler, IFormFile fatura)
        {
            if (id != istekler.IstekNo)
            {
                return NotFound();
            }

            if (fatura != null)
            {
                var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot", "faturalar",
                  "harcama_" + istekler.IstekNo.ToString() + ".pdf");
                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await fatura.CopyToAsync(fileStream);
                }
            }

            if (ModelState.IsValid)
            {
                try
                {
                    istekler.OnayNo = id;
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateB([Bind("IstekNo,Konu,YaklasikMaliyet,Ozet,TurNo,OnayNo,FirmaId,BolumId,Onay,Fiyat,FaturaTarihi,FaturaNo,eFaturami,TahakkukNo")] Istekler istekler)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    istekler.Onay = 1;
                    _context.Add(istekler);
                    await _context.SaveChangesAsync();

                    istekler.OnayNo = istekler.IstekNo;
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
                return RedirectToAction(nameof(Index), new { isid = istekler.IstekNo });
            }
            return RedirectToAction("Index", "IstekOnay");
        }

        // GET: IstekGir/Delete/5
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

        // POST: IstekGir/Delete/5
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
