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
    public class OnayController : Controller
    {
        private readonly donersermayeContext _context;

        public OnayController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Onay
        public async Task<IActionResult> Index(int? yazdir)
        {
            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Tur).Where(i=>i.BolumId==Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && i.BolumOnay==0);
            ViewData["yazdir"] = yazdir;
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: Onay/Details/5
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

        // GET: Onay/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id");
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id");
            return View();
        }

        // POST: Onay/Create
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

        // GET: Onay/Edit/5
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

        // POST: Onay/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,Aciklama,PersonelId,Durum")] Isler isler)
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

        // GET: Onay/Delete/5
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

        // POST: Onay/Delete/5
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
        [HttpGet]
        public  IActionResult Reddet(int id)
        {
          


           var sorgu = _context.Isler.SingleOrDefault(m => m.Id == id);

            sorgu.BolumOnay = -1;
            _context.Isler.Update(sorgu);
             _context.SaveChanges();

            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Tur).Where(i => i.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && i.BolumOnay == null);
           
            return Redirect("/Onay");
        }
        [HttpGet]
        public IActionResult Onayla(int id)
        {
            var sorgu = _context.Isler.SingleOrDefault(m => m.Id == id);

            sorgu.BolumOnay = 1;
            _context.Isler.Update(sorgu);
            _context.SaveChanges();

            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Tur).Where(i => i.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && i.BolumOnay == null);

            return Redirect("/Onay?yazdir="+id);
        }
        [HttpGet]
        public JsonResult Incele(int? id)
        {
            var sorgu = from a in  _context.IsAyrinti
                        join b in _context.Personeller on a.PersonelId equals b.Id
                        where a.ısId==id
                        select new
                        {
                           Ad= b.Ad + " "+ b.Soyad,
                           Toplam=a.Tutar

                        };
                        
                       

            return Json(sorgu);
        }

        [HttpGet]
        public JsonResult Incele2(int? id)
        {
            var sorgu = from a in _context.Isler
                        join b in _context.Iskesintiler on a.Id equals b.IsId
                        join c in _context.KesintiOran on b.KesintiOranId equals c.Id
                        join d in _context.Kesintitipleri on c.KesintiTipId equals d.Id
                        join e in _context.KesintitipAyrinti on d.kesintiTipAyrintiId equals e.Id
                        where a.Id == id
                        select new
                        {
                            Aciklama = e.Aciklama,
                            Oran = c.Oran,
                            Tutar = (a.Tutar * c.Oran) / 100
                        };


            return Json(sorgu);
        }

    }
}
