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
    public class IsTalepleriController : Controller
    {
        private readonly donersermayeContext _context;

        public IsTalepleriController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: BolumPersonel
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.IsTalepleri.Include(a => a.Firma).Where(a=>a.BolumId==Convert.ToInt32(HttpContext.Session.GetString("bolumId")) && a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")));
                        
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: BolumPersonel
        public async Task<IActionResult> IndexB()
        {
            var donersermayeContext = _context.IsTalepleri.Include(a => a.Firma).Where(a => a.BolumId == Convert.ToInt32(HttpContext.Session.GetString("bolumId")));

            return View(await donersermayeContext.ToListAsync());
        }

        // GET: BolumPersonel
        public async Task<IActionResult> IndexC()
        {
            var donersermayeContext = _context.IsTalepleri.Include(a => a.Firma);

            return View(await donersermayeContext.ToListAsync());
        }
        // GET: BolumPersonel
        public async Task<IActionResult> Onayla(int? id)
        {
            var donersermayeContext = _context.IsTalepleri.Where(a => a.Id == id);

            return View(await donersermayeContext.SingleAsync());
        }

        // GET: BolumPersonel
        public async Task<IActionResult> Reddet(int? id)
        {
            var donersermayeContext = _context.IsTalepleri.Where(a => a.Id == id);

            return View(await donersermayeContext.SingleAsync());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: BolumPersonel
        public async Task<IActionResult> Onayla(IsTalepleri IsTalepleri)
        {
            var donersermayeContext = _context.IsTalepleri.Where(a => a.Id == IsTalepleri.Id).FirstOrDefault();
            donersermayeContext.ToplantiSayisi = IsTalepleri.ToplantiSayisi;
            donersermayeContext.KararSayisi = IsTalepleri.KararSayisi;
            donersermayeContext.KararTarihi = IsTalepleri.KararTarihi;
            donersermayeContext.BolumOnay = 1;
            _context.SaveChanges();

            return RedirectToAction("IndexB");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // GET: BolumPersonel
        public async Task<IActionResult> Reddet(IsTalepleri IsTalepleri)
        {
            var donersermayeContext = _context.IsTalepleri.Where(a => a.Id == IsTalepleri.Id).FirstOrDefault();
            donersermayeContext.RedSebebi = IsTalepleri.RedSebebi;
            donersermayeContext.BolumOnay = -1;
            _context.SaveChanges();

            return RedirectToAction("IndexB");
        }

        // GET: BolumPersonel/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeller = await _context.Personeller
                .Include(p => p.Bolum)
                .Include(p => p.Unvan)
                .Include(p => p.Yetki)
                .SingleOrDefaultAsync(m => m.Id == id);

            if (personeller == null)
            {
                return NotFound();
            }

            return View(personeller);
        }

        // GET: BolumPersonel/Create
        public IActionResult Create()
        {
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            ViewData["IsTuru"] = new SelectList(_context.IsTurleri, "Id", "Tur");
            return View();
        }

        // POST: BolumPersonel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BolumId,FirmaId,TurId,Aciklama,Tutar")] IsTalepleri IsTalepleri)
        {
            if (ModelState.IsValid)
            {
                if (IsTalepleri.FirmaId == 0)
                    IsTalepleri.FirmaId = null;
                IsTalepleri.BolumId = Convert.ToInt32(HttpContext.Session.GetString("bolumId"));
                IsTalepleri.PersonelId = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));
                _context.Add(IsTalepleri);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction("Index");
        }

        // GET: BolumPersonel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dsc = await _context.IsTalepleri.SingleOrDefaultAsync(m => m.Id == id);
            if (dsc == null)
            {
                return NotFound();
            }

            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi");
            ViewData["IsTuru"] = new SelectList(_context.IsTurleri, "Id", "Tur");
            return View(dsc);
        }

        // POST: BolumPersonel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, IsTalepleri IsTalepleri)
        {
            if (id != IsTalepleri.Id)
            {
                return NotFound();
            }

            var dsc = _context.IsTalepleri.Where(a => a.Id == IsTalepleri.Id).FirstOrDefault();
            dsc.Aciklama = IsTalepleri.Aciklama;
            dsc.Tutar = IsTalepleri.Tutar;
            dsc.FirmaId = IsTalepleri.FirmaId;
            dsc.TurId = IsTalepleri.TurId;
            dsc.BolumOnay = 0;
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Sifre(int id, [Bind("Id,BolumId,UnvanId,YetkiId,TcKimlikNo,Ad,Soyad,Iban,Sifre")] Personeller personeller)
        {
            if (id != personeller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(personeller);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonellerExists(personeller.Id))
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
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", personeller.BolumId);
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Id", personeller.UnvanId);
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id", personeller.YetkiId);
            return View(personeller);
        }

        // GET: BolumPersonel/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeller = await _context.Personeller
                .Include(p => p.Bolum)
                .Include(p => p.Unvan)
                .Include(p => p.Yetki)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (personeller == null)
            {
                return NotFound();
            }

            return View(personeller);
        }

        // POST: BolumPersonel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var personeller = await _context.Personeller.SingleOrDefaultAsync(m => m.Id == id);
            _context.Personeller.Remove(personeller);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonellerExists(int id)
        {
            return _context.Personeller.Any(e => e.Id == id);
        }
    }
}
