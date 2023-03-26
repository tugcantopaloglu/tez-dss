using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using System.Globalization;

namespace DonerSermaye.Controllers
{
    public class BSBAController : Controller
    {
        private readonly donersermayeContext _context;

        public BSBAController(donersermayeContext context)
        {
            _context = context;
        }      
        
        public class Yillar
        {
            public int Id { get; set; }
            public int Ad { get; set; }
        }

        public IActionResult Index(int? ay, int? yil)
        {
            if(yil == null)
            {
                yil = DateTime.Now.Year;
            }
            if (ay == null)
            {
                ay = DateTime.Now.Month;
            }
            var ayar = _context.Ayarlar.Where(b => b.Id == 1).FirstOrDefault();

            var aylar = new List<Aylar>();

            aylar.Add(new Aylar { Id = 1, Ad = "Ocak" });
            aylar.Add(new Aylar { Id = 2, Ad = "Şubat" });
            aylar.Add(new Aylar { Id = 3, Ad = "Mart" });
            aylar.Add(new Aylar { Id = 4, Ad = "Nisan" });
            aylar.Add(new Aylar { Id = 5, Ad = "Mayıs" });
            aylar.Add(new Aylar { Id = 6, Ad = "Haziran" });
            aylar.Add(new Aylar { Id = 7, Ad = "Temmuz" });
            aylar.Add(new Aylar { Id = 8, Ad = "Ağustos" });
            aylar.Add(new Aylar { Id = 9, Ad = "Eylül" });
            aylar.Add(new Aylar { Id = 10, Ad = "Ekim" });
            aylar.Add(new Aylar { Id = 11, Ad = "Kasım" });
            aylar.Add(new Aylar { Id = 12, Ad = "Aralık" });



            var yillar = new List<Yillar>();

            for(int i=2020; i<DateTime.Now.Year+1; i++)
            {
                yillar.Add(new Yillar { Id = i, Ad = i });
            }

            ViewData["Yillar"] = new SelectList(yillar, "Id", "Ad", yil);
            ViewData["Aylar"] = new SelectList(aylar, "Id", "Ad", ay);

            CultureInfo ci = new CultureInfo("tr-TR");
            var bslist = _context.Isler.Include(a => a.Firma).Where(i=> i.FaturaTipi==1 && i.FaturaTarihi.Value.Month == ay && i.FaturaTarihi.Value.Year == yil).GroupBy(a => new { a.FirmaId, a.Firma.FirmaAdi, a.Firma.VergiNo }).Select(a => new BSBAViewModel { FirmaId = a.Key.FirmaId, Tutar = a.Sum(b => b.Tutar), FaturaSayisi = a.Count(), FirmaAdi = a.Key.FirmaAdi, VergiNo = a.Key.VergiNo }).Where(a => a.Tutar >= ayar.BSBALimiti);

            BSBAListViewModel bsss = new BSBAListViewModel();

            bsss.BSList = bslist.ToList();

            var balist = _context.Istekler.Include(a => a.Firma).Where(i => i.FaturaTarihi.Value.Month == ay && i.FaturaTarihi.Value.Year == yil).GroupBy(a => new { a.FirmaId, a.Firma.FirmaAdi, a.Firma.VergiNo }).Select(a => new BSBAViewModel { FirmaId = a.Key.FirmaId, Tutar = a.Sum(b => b.Fiyat), FaturaSayisi = a.Count(), eFaturaSayisi = _context.Istekler.Where(i => i.FaturaTarihi.Value.Month == ay && i.FaturaTarihi.Value.Year == yil && i.eFaturami == true && i.Firma.Id == a.Key.FirmaId).Count(), FirmaAdi = a.Key.FirmaAdi, VergiNo = a.Key.VergiNo }).Where(a => a.Tutar >= ayar.BSBALimiti);

            //balist.ForEachAsync(a => a.eFaturaSayisi = _context.Istekler.Where(i => i.FaturaTarihi.Value.Month == ay && i.eFaturami == true && i.Firma.Id == a.FirmaId).Count());

            bsss.BAList = balist.ToList();

            return View(bsss);

        }       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var isler = (from a in _context.Isler
                         join b in _context.IsAyrinti on a.Id equals b.ısId
                         join c in _context.Iskesintiler on a.Id equals c.IsId
                         where (a.Id == id)
                         select new ModelAyrinti
                         {
                            id= a.Id,
                             Firma=a.FirmaId,
                             TurId=a.TurId,
                             Tutar=a.Tutar,
                             Kdv=a.Kdv,
                             FaturaNo=a.FaturaNo,
                             FaturaTarihi=a.FaturaTarihi,
                             PersonelId =Convert.ToInt32(b.PersonelId),
                             kisitutar =b.Tutar
                         }).ToList();
            if (isler == null)
            {
                return NotFound();
            }

            return  View(isler);
        }

       
        public IActionResult Create()
        {
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id");
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id");
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id");
            return View();
        }

        // POST: Odenen/Create
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

        // GET: Odenen/Edit/5
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

        
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,FaturaTipi,Durum,Aciklama,PersonelId")] Isler isler)
        {
            if (id != isler.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (isler.FaturaTipi == 2)
                        isler.Durum = 0;
                    _context.Update(isler);
                    // peşin fatura tipi ise bölüm ve dekanlık gelirleri de eklenecek.
                    if (isler.FaturaTipi == 1)
                    {
                        Bolumgelir bg = new Bolumgelir();
                        bg.BolumId = isler.BolumId;
                        bg.IsId = isler.Id;
                        Bolumler b = _context.Bolumler.Where(a => a.Id == isler.BolumId).FirstOrDefault();
                        KesintiOran ko = _context.KesintiOran.Include(z => z.KesintiTip).Where(a => a.KesintiTip.kesintiTipAyrintiId == b.KesintitipAyrintiId && a.TurId == isler.TurId).FirstOrDefault();
                        var ktutar = (isler.Tutar * ko.Oran) / 100;
                        bg.Tutar = ktutar;
                        _context.Bolumgelir.Add(bg);
                        Bolumler dekanlik = _context.Bolumler.Where(c => c.KesintitipAyrinti.Id == 1).FirstOrDefault();
                        KesintiOran koDekanlik = _context.KesintiOran.Include(z => z.KesintiTip).Where(a => a.KesintiTip.kesintiTipAyrintiId == dekanlik.KesintitipAyrintiId && a.TurId == isler.TurId).FirstOrDefault();
                        var ktutarDekanlik = (isler.Tutar * koDekanlik.Oran) / 100;
                        Bolumgelir bgDekanlik = new Bolumgelir();
                        bgDekanlik.BolumId = dekanlik.Id;
                        bgDekanlik.IsId = isler.Id;
                        bgDekanlik.Tutar = ktutarDekanlik;
                        _context.Bolumgelir.Add(bgDekanlik);
                    }
                    else
                    {
                        Bolumler b = _context.Bolumler.Where(a => a.Id == isler.BolumId).FirstOrDefault();
                        List<Bolumgelir> bg = _context.Bolumgelir.Where(a => a.IsId == isler.Id).ToList();
                        foreach (var bt in bg)
                        {
                            _context.Bolumgelir.Remove(bt);
                        }
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
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", isler.BolumId);
            ViewData["FaturaTipi"] = new SelectList(_context.FaturaTipi, "Id", "FaturaTipi1", isler.FaturaTipi);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", isler.Firma);
            ViewData["HesapId"] = new SelectList(_context.Hesaplar, "Id", "Id", isler.HesapId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            return View(isler);
        }

        
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

        public async  Task<IActionResult> HesapEkle(string hesap)
        {
            var hesaplar = new Hesaplar();
            hesaplar.Hesap = hesap;
            _context.Hesaplar.Add(hesaplar);
            _context.SaveChanges();
            var guncelle =  _context.Isler.Where(i => i.Durum == 0 && i.Lab != 1 && i.HesapId == null && i.FaturaTipi == 1 && i.TurId != 3);
            foreach (var item in guncelle)
            {
                item.HesapId = hesaplar.Id;
                item.Dagitim = 1;
                item.Durum = 1;
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

  
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
