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
    public class KisiselController : Controller
    {
        private readonly donersermayeContext _context;

        public KisiselController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Kisisel
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Personeller.Include(p => p.Bolum).Include(p => p.Unvan).Include(p => p.Yetki).Where(p=>p.Id== Convert.ToInt32(HttpContext.Session.GetString("PersonelId")));
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: Kisisel/Details/5
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

        // GET: Kisisel/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Id");
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id");
            return View();
        }

        // POST: Kisisel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BolumId,UnvanId,YetkiId,TcKimlikNo,Ad,Soyad,Iban,Sifre")] Personeller personeller)
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

        // GET: Kisisel/Edit/5
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
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Unvan", personeller.UnvanId);
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id", personeller.YetkiId);
            return View(personeller);
        }

        // POST: Kisisel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string Sifre, [Bind("Id,BolumId,UnvanId,EPosta,YetkiId,TcKimlikNo,Ad,Soyad,Iban")] Personeller personeller)
        {
            if (id != personeller.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Sifre != null)
                    {
                        personeller.Sifre = Sifre;
                    }
                    else
                    {
                        string _sifre = _context.Personeller.Where(a => a.Id == id).Select(a => a.Sifre).FirstOrDefault();
                        personeller.Sifre = _sifre;
                    }
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
                //return RedirectToAction(nameof(Index));
            }
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", personeller.BolumId);
            ViewData["UnvanId"] = new SelectList(_context.Unvanlar, "Id", "Unvan", personeller.UnvanId);
            ViewData["YetkiId"] = new SelectList(_context.Yetkiler, "Id", "Id", personeller.YetkiId);
            return View(personeller);
        }

        // GET: Kisisel/Delete/5
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

        // POST: Kisisel/Delete/5
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
