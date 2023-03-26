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
    public class BolumPersonelController : Controller
    {
        private readonly donersermayeContext _context;

        public BolumPersonelController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: BolumPersonel
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Personeller.Include(p => p.Bolum).Include(p => p.Unvan).Include(p => p.Yetki).Where(a=>a.BolumId==Convert.ToInt32(HttpContext.Session.GetString("bolumId")));
            return View(await donersermayeContext.ToListAsync());
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
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Unvan");
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Yetki");
            return View();
        }

        // POST: BolumPersonel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BolumId,UnvanId,YetkiId,TcKimlikNo,Ad,Soyad,EPosta,Iban,Sifre")] Personeller personeller)
        {
            if (ModelState.IsValid)
            {
                _context.Add(personeller);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", personeller.BolumId);
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Id", personeller.UnvanId);
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id", personeller.YetkiId);
            return View(personeller);
        }

        // GET: BolumPersonel/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeller = await _context.Personeller.SingleOrDefaultAsync(m => m.Id == id);
            if (personeller == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", personeller.BolumId);
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Id", personeller.UnvanId);
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id", personeller.YetkiId);
            ViewData["Bolum"] = new SelectList(_context.Bolumler, "Id", "Bolum", personeller.BolumId);
            ViewData["Unvan"] = new SelectList(_context.Unvanlar, "Id", "Unvan", personeller.UnvanId);
            ViewData["Yetki"] = new SelectList(_context.Yetkiler, "Id", "Yetki", personeller.YetkiId);
            return View(personeller);
        }

        public async Task<IActionResult> Sifre(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var personeller = await _context.Personeller.SingleOrDefaultAsync(m => m.Id == id);
            if (personeller == null)
            {
                return NotFound();
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", personeller.BolumId);
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Id", personeller.UnvanId);
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id", personeller.YetkiId);
            ViewData["Bolum"] = new SelectList(_context.Bolumler, "Id", "Bolum", personeller.BolumId);
            ViewData["Unvan"] = new SelectList(_context.Unvanlar, "Id", "Unvan", personeller.UnvanId);
            ViewData["Yetki"] = new SelectList(_context.Yetkiler, "Id", "Yetki", personeller.YetkiId);
            return View(personeller);
        }

        // POST: BolumPersonel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,UnvanId,YetkiId,TcKimlikNo,Ad,Soyad,EPosta,Iban,Sifre")] Personeller personeller)
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
