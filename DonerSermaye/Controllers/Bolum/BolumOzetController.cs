using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;

namespace DonerSermaye.Controllers.Bolum
{
    public class BolumOzetController : Controller
    {
        private readonly donersermayeContext _context;

        public BolumOzetController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: BolumOzet
        public async Task<IActionResult> Index()
        {
            var bIs = _context.Isler.Where(a => a.BolumId ==Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.FaturaTipi == 1).Count();
            var bTop = _context.Isler.Where(a => a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.FaturaTipi == 1).Select(b => b.Tutar).Sum();
            var bOdenen = _context.IsAyrinti.Include(a=>a.ıs).Where(a => a.ıs.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.ıs.Dagitim == 1).Select(b => b.Tutar).Sum();
            var bolumpayi = from b in _context.Bolumgelir
                            where b.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))
                            select new { b.Tutar };
            var bolumpayi2 = bolumpayi.ToList();
            decimal s=0;
            decimal tmpvalue;
            decimal tmpvalue2;
            foreach (var item in bolumpayi2)
            {
                decimal? tt = decimal.TryParse(item.Tutar.ToString(), out tmpvalue) ?
                  tmpvalue : (decimal?)null;
                s +=(Convert.ToDecimal(tt));
            }

            // todo: 2023 ve sonrasında geçmiş yıl peşin fatura toplamları listelenmeli. şimdilik sadece geçen yıldan devir tutarı alınıyor.

            //var gecmisyil = 2021;

            var gecmisyiltoplami = _context.Bolumler.Where(a => a.Id == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))).Select(a => a.ilkdevirtutari).FirstOrDefault();

            ViewBag.bIs =bIs;
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

            var labtutar = _context.Isler.Where(a => a.Lab == 1 && a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.BolumOnay == 1 && a.FaturaTipi == 1).Sum(a => a.Tutar);
            if (labtutar != null)
            {
                ViewBag.LabTutar = ((Decimal)labtutar).ToString("0.00");
            }
            else
            {
                ViewBag.LabTutar = 0;
            }

            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Tur).Where(i => i.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId"))).Take(20).OrderByDescending(a => a.Id);
            return View(await donersermayeContext.ToListAsync());
        }

        





        // GET: BolumOzet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = await _context.Isler
                .Include(i => i.Bolum)
                .Include(i => i.Firma)
                .Include(i => i.Tur)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }

            return View(isler);
        }

        // GET: BolumOzet/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id");
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id");
            return View();
        }

        // POST: BolumOzet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,Durum")] Isler isler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isler);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        // GET: BolumOzet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = await _context.Isler.SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        // POST: BolumOzet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,Durum")] Isler isler)
        {
            if (id != isler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isler);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IslerExists(isler.Id))
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
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        // GET: BolumOzet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = await _context.Isler
                .Include(i => i.Bolum)
                .Include(i => i.Firma)
                .Include(i => i.Tur)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }

            return View(isler);
        }

        // POST: BolumOzet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isler = await _context.Isler.SingleOrDefaultAsync(m => m.Id == id);
            _context.Isler.Remove(isler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslerExists(int id)
        {
            return _context.Isler.Any(e => e.Id == id);
        }
    }
}
