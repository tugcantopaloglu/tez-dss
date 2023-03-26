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
    public class KisiselOzetController : Controller
    {
        private readonly donersermayeContext _context;

        public KisiselOzetController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: KisiselOzet
        public IActionResult Index()
        {
            var tamami = (from a in _context.IsAyrinti
                       join b in _context.Isler on a.ısId equals b.Id
                       join c in _context.Hesaplar on b.HesapId equals c.Id
                       where (a.PersonelId ==Convert.ToInt32(HttpContext.Session.GetString("PersonelId"))) && b.HesapId != null
                          select a.Tutar).Sum();

            var shesapa = (from a in _context.Hesaplar where a.Hesap.Contains("ARGE") select a.Id).Max();
            var shesap = (from a in _context.Hesaplar where !a.Hesap.Contains("ARGE") select a.Id).Max();

            var son = (from a in _context.IsAyrinti
                                   join b in _context.Isler on a.ısId equals b.Id
                                   where (a.PersonelId ==Convert.ToInt32(HttpContext.Session.GetString("PersonelId"))) && (b.HesapId == shesap || b.HesapId == shesapa)
                       select a.Tutar).Sum();

            var dagitilmamis = (from a in _context.IsAyrinti
                                join b in _context.Isler on a.ısId equals b.Id
                                where (a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId"))) && b.HesapId == null
                                select a.Tutar).Sum();

            decimal? p = 0;
            if (son != null)
                p = son;
            else
                p = 0;
            decimal? q = 0;
            if (tamami != null)
                q = tamami;
            else
                q = 0;
            decimal? r = 0;
            if (dagitilmamis != null)
                r = dagitilmamis;
            else
                r = 0;
           

            var query = from a in _context.IsAyrinti
                        join b in _context.Isler on a.ısId equals b.Id
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        where (b.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")))
                        orderby b.Durum, b.BolumOnay descending
                        select new MyModel
                        {
                            Id = a.Id,
                            FirmaAdi = c.FirmaAdi,
                            Tur = d.Tur,
                            Kisi = Convert.ToDecimal(a.Tutar),
                            Tutar = Convert.ToDecimal(b.Tutar),
                            Bonay = Convert.ToInt32(b.BolumOnay),
                            Gonay = Convert.ToInt32(b.Dagitim),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi
                         };
            var query2 = _context.Duyurular.Include(d => d.GonderenNavigation);

            var ViewModel = new ViewModel { list = query.ToList(), duyurular = query2.ToList() };
                        
            ViewBag.toplam = String.Format("{0:0.##}", q);
            ViewBag.son = String.Format("{0:0.##}", p);
            ViewBag.dagitilmamis = String.Format("{0:0.##}", r);
            return View(ViewModel);
        }

        // GET: KisiselOzet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAyrinti = await _context.IsAyrinti
                .Include(i => i.ıs)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (isAyrinti == null)
            {
                return NotFound();
            }

            return View(isAyrinti);
        }

        // GET: KisiselOzet/Create
        public IActionResult Create()
        {
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id");
            return View();
        }

        // POST: KisiselOzet/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ısId,TcKimlikNo,Tutar")] IsAyrinti isAyrinti)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isAyrinti);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id", isAyrinti.ısId);
            return View(isAyrinti);
        }

        // GET: KisiselOzet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isAyrinti = await _context.IsAyrinti.SingleOrDefaultAsync(m => m.Id == id);
            if (isAyrinti == null)
            {
                return NotFound();
            }
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id", isAyrinti.ısId);
            return View(isAyrinti);
        }

        // POST: KisiselOzet/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ısId,TcKimlikNo,Tutar")] IsAyrinti isAyrinti)
        {
            if (id != isAyrinti.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(isAyrinti);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IsAyrintiExists(isAyrinti.Id))
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
            ViewData["ısId"] = new SelectList(_context.Isler, "Id", "Id", isAyrinti.ısId);
            return View(isAyrinti);
        }

        // GET: KisiselOzet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var IsKesinti = _context.Iskesintiler.Where(a => a.IsId == id).ToList();
            foreach (var item in IsKesinti)
            {
                _context.Iskesintiler.Remove(item);
                await _context.SaveChangesAsync();
                
            }
            
            var IsAyrinti = _context.IsAyrinti.Where(a => a.ısId == id).ToList();
            foreach (var gelen in IsAyrinti)
            {
                _context.IsAyrinti.Remove(gelen);
                await _context.SaveChangesAsync();
            }

            var Isler = _context.Isler.Where(a => a.Id == id).ToList();
            foreach (var gelen in Isler)
            {
                _context.Isler.Remove(gelen);
                await _context.SaveChangesAsync();
            }

  

            return Redirect("/KisiselOzet");
        }

        // POST: KisiselOzet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isAyrinti = await _context.IsAyrinti.SingleOrDefaultAsync(m => m.Id == id);
            _context.IsAyrinti.Remove(isAyrinti);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IsAyrintiExists(int id)
        {
            return _context.IsAyrinti.Any(e => e.Id == id);
        }
    }
}
