using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace DonerSermaye.Controllers
{
    public class OdemeController : Controller
    {
        private readonly donersermayeContext _context;

        public OdemeController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Odeme
        public async Task<IActionResult> Index()
        {
            var donersermayeContext = _context.Isler.Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Tur).Where(i=>i.FaturaTipi == null &&  i.BolumOnay==1);
            return View(await donersermayeContext.ToListAsync());
        }

        // GET: Odeme/Details/5
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

        public IActionResult Iade(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = _context.Isler.FirstOrDefault(m => m.Id == id);
            isler.BolumOnay = -1;
            isler.FaturaTipi = null;
            isler.FaturaTarihi = null;
            isler.FaturaNo = null;
            _context.SaveChanges();
            if (isler == null)
            {
                return NotFound();
            }

            return RedirectToAction(nameof(Index));
        }
        // GET: Odeme/Create
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id");
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id");
            return View();
        }

        // POST: Odeme/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,Aciklama,Durum")] Isler isler)
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

        // GET: Odeme/Edit/5
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
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", isler.BolumId);
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "FaturaTipi1", isler.FaturaTipi);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", isler.Firma);
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id", isler.HesapId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        // POST: Odeme/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,FaturaTipi,Durum,Aciklama,PersonelId,Lab")] Isler isler, IFormFile fatura)
        {
            if (id != isler.Id)
            {
                return NotFound();
            }

            if(fatura != null)
            {
                var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot", "faturalar",
                  isler.Id.ToString() + ".pdf");
                using (Stream fileStream = new FileStream(path, FileMode.Create))
                {
                    await fatura.CopyToAsync(fileStream);
                }
                isler.faturayuklendimi = true;
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // 29.03.2022 bölümgelir peşinden borçlar faturasına dönen işlerde silinmeli
                    var bolumgelirvarmi = _context.Bolumgelir.Where(a => a.IsId == isler.Id).Count();
                    if (isler.FaturaTipi == 2)
                    {
                        isler.Durum = 0;
                        if (bolumgelirvarmi > 0)
                        {
                            var bgg = _context.Bolumgelir.Where(a => a.IsId == isler.Id).ToList();
                            foreach (var bg1 in bgg)
                            {
                                _context.Bolumgelir.Remove(bg1);
                            }
                        }
                    }
                    _context.Update(isler);
                    // peşin fatura tipi ise bölüm ve dekanlık gelirleri de eklenecek. 29.03.2022 - bölümgelir kaydedildiyse bu işlem tekrar yapılmayacak!!
                    if (isler.FaturaTipi == 1 && bolumgelirvarmi == 0)
                    {
                        Bolumgelir bg = new Bolumgelir();
                        bg.BolumId = isler.BolumId;
                        bg.IsId = isler.Id;
                        Bolumler b = _context.Bolumler.Where(a => a.Id == isler.BolumId).FirstOrDefault();
                        
                            KesintiOran ko = _context.KesintiOran.Include(z => z.KesintiTip).Where(a => a.KesintiTip.kesintiTipAyrintiId == b.KesintitipAyrintiId && a.TurId == isler.TurId).FirstOrDefault();
                        if (isler.BolumId == 1)
                            ko = _context.KesintiOran.Include(z => z.KesintiTip).Where(a => a.KesintiTip.kesintiTipAyrintiId == 2 && a.TurId == isler.TurId).FirstOrDefault();

                        var ktutar = (isler.Tutar * ko.Oran) / 100;
                        bg.Tutar = ktutar;
                        if(isler.BolumId != 1)
                            _context.Bolumgelir.Add(bg);
                        Bolumler dekanlik = _context.Bolumler.Where(c => c.KesintitipAyrinti.Id == 1).FirstOrDefault();
                        KesintiOran koDekanlik = _context.KesintiOran.Include(z => z.KesintiTip).Where(a => a.KesintiTip.kesintiTipAyrintiId == dekanlik.KesintitipAyrintiId && a.TurId == isler.TurId).FirstOrDefault();
                        var ktutarDekanlik = (isler.Tutar * koDekanlik.Oran) / 100;
                        Bolumgelir bgDekanlik = new Bolumgelir();
                        bgDekanlik.BolumId = dekanlik.Id;
                        bgDekanlik.IsId = isler.Id;
                        bgDekanlik.Tutar = ktutarDekanlik;
                        if (isler.BolumId == 1)
                            bgDekanlik.Tutar += bg.Tutar;
                        _context.Bolumgelir.Add(bgDekanlik);
                    }
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
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "Id", isler.FaturaTipi);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id", isler.HesapId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        // GET: Odeme/Delete/5
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

        // POST: Odeme/Delete/5
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
