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
    public class BolumIstekController : Controller
    {
        private readonly donersermayeContext _context;

        public BolumIstekController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Istekler
        public async Task<IActionResult> Index(int? FirmaId, int? IsTurId, DateTime? BaslamaTarihi, DateTime? BitisTarihi, int? IstekNo)
        {
            var bIs = _context.Isler.Where(a => a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.FaturaTipi == 1).Count();
            var bTop = _context.Isler.Where(a => a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.FaturaTipi == 1).Select(b => b.Tutar).Sum();
            var bOdenen = _context.IsAyrinti.Include(a => a.ıs).Where(a => a.ıs.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.ıs.Dagitim == 1).Select(b => b.Tutar).Sum();
            var bolumpayi = from b in _context.Bolumgelir
                            where b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))
                            select new { b.Tutar };
            var bolumpayi2 = bolumpayi.ToList();
            decimal s = 0;
            decimal tmpvalue;
            decimal tmpvalue2;
            foreach (var item in bolumpayi2)
            {
                decimal? tt = decimal.TryParse(item.Tutar.ToString(), out tmpvalue) ?
                  tmpvalue : (decimal?)null;
                s += (Convert.ToDecimal(tt));
            }

            // todo: 2023 ve sonrasında geçmiş yıl peşin fatura toplamları listelenmeli. şimdilik sadece geçen yıldan devir tutarı alınıyor.

            //var gecmisyil = 2021;

            var gecmisyiltoplami = _context.Bolumler.Where(a => a.Id == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))).Select(a => a.ilkdevirtutari).FirstOrDefault();

            ViewBag.bIs = bIs;
            ViewBag.bTop = String.Format("{0:0.##}", bTop);
            ViewBag.bOdenen = String.Format("{0:0.##}", bOdenen);
            var sum = (from z in _context.Istekler where z.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) select z.Fiyat).Sum();
            ViewBag.bGecmisyil = String.Format("{0:0.##}", gecmisyiltoplami);
            ViewBag.bGider = String.Format("{0:0.##}", sum);
            ViewBag.bGelir = String.Format("{0:0.##}", s);

            if (sum == null)
            {
                ViewBag.bKalan = String.Format("{0:0.##}", s + gecmisyiltoplami);
            }
            else
            {
                ViewBag.bKalan = String.Format("{0:0.##}", s + gecmisyiltoplami - sum);
            }

            var query = _context.Istekler.Include(i => i.TurNoNavigation).Include(i=>i.Bolum).Include(a => a.Firma).Where(i=>i.BolumId== Convert.ToInt32(HttpContext.Session.GetString("bolumId")));


            if (FirmaId != null)
            {
                query = query.Where(a => a.FirmaId == FirmaId);
                ViewData["Filtre"] = "filtre";
            }
            if (BaslamaTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi >= BaslamaTarihi);
                ViewData["BaslamaTarihi"] = BaslamaTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (BitisTarihi != null)
            {
                query = query.Where(a => a.FaturaTarihi <= BitisTarihi);
                ViewData["BitisTarihi"] = BitisTarihi;
                ViewData["Filtre"] = "filtre";
            }
            if (IstekNo != null)
            {
                query = query.Where(a => a.IstekNo == IstekNo);
                ViewData["IstekNo"] = IstekNo;
                ViewData["Filtre"] = "filtre";
            }
            if (IsTurId != null)
            {
                query = query.Where(a => a.TurNo == IsTurId);
                ViewData["Filtre"] = "filtre";
            }

            var result = query.ToList();
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "Tur", IsTurId);

            query = query.OrderByDescending(a => a.IstekNo);

            return View(await query.ToListAsync());
        }

        // GET: Istekler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekler = await _context.Istekler
                .Include(i => i.TurNoNavigation)
                .SingleOrDefaultAsync(m => m.IstekNo == id);
            if (istekler == null)
            {
                return NotFound();
            }

            return View(istekler);
        }

        // GET: Istekler/Create
        public IActionResult Create()
        {
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo");
            ViewData["Tur"] = new SelectList(_context.IstekTurleri, "TurNo", "Tur");
            ViewData["Bolum"] = new SelectList(_context.IstekTurleri, "Id", "bolum");
            return View();
        }

        // POST: Istekler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IstekNo,Konu,Ozet,TurNo,OnayNo,Onay,Aciklama,YaklasikMaliyet,IstekAd,IstekAdet")] Istekler istekler, string action)
        {
                Istekler istekx = null;
                if (action == "Ekle")
                {
                    istekler.BolumId = Convert.ToInt32(HttpContext.Session.GetString("bolumId"));
                    istekx = istekler;
                    ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
                    return View(istekler);
                }
                else if (action == "Onaya Gönder")
                {
                   if (ModelState.IsValid)
                   {
                    _context.Add(istekx);
                     await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Istekler");
                   }
                }   

            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
            return View(istekler);
        }
        // GET: Istekler/Edit/5
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
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
            ViewData["Tur"] = new SelectList(_context.IstekTurleri, "TurNo", "Tur", istekler.TurNo);
            return View(istekler);
        }

        // POST: Istekler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IstekNo,Konu,Ozet,TurNo,OnayNo,BolumId,YaklasikMaliyet")] Istekler istekler)
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
            ViewData["TurNo"] = new SelectList(_context.IstekTurleri, "TurNo", "TurNo", istekler.TurNo);
            return View(istekler);
        }

        // GET: Istekler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var istekler = await _context.Istekler
                .Include(i => i.TurNoNavigation)
                .SingleOrDefaultAsync(m => m.IstekNo == id);
            if (istekler == null)
            {
                return NotFound();
            }

            return View(istekler);
        }

        // POST: Istekler/Delete/5
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
