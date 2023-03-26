using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.IO;

namespace DonerSermaye.Controllers
{
    public class KesintiRaporController : Controller
    {
        private readonly donersermayeContext _context;

        public KesintiRaporController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Isler
        public IActionResult Index(int? FirmaId, int? BolumId, int? KesintiTuru, DateTime? BaslamaTarihi, DateTime? BitisTarihi, int? IsNo)
        {
            if(BaslamaTarihi == null && BitisTarihi == null)
            {
                BaslamaTarihi = DateTime.Now.AddDays(-30);
                BitisTarihi = DateTime.Now;
            }
            var query = from b in _context.Isler
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        join e in _context.Bolumler on b.BolumId equals e.Id
                        join f in _context.Iskesintiler on b.Id equals f.IsId
                        join h in _context.KesintiOran on f.KesintiOranId equals h.Id
                        join i in _context.Kesintitipleri on h.KesintiTipId equals i.Id
                        where b.FaturaTipi == 1
                        orderby b.Durum, b.BolumOnay descending
                        select new MyModel
                        {
                            Id = 0,
                            FirmaAdi = c.FirmaAdi,
                            FirmaId = c.Id,
                            Tur = d.Tur,
                            Kisi = 0,
                            Tutar = Convert.ToDecimal(b.Tutar),
                            KesintiTutar = Convert.ToDecimal((b.Tutar * h.Oran) / 100),
                            KesintiOran = Convert.ToDecimal(h.Oran),
                            Bonay = Convert.ToInt32(b.BolumOnay),
                            Gonay = Convert.ToInt32(b.Dagitim),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo,
                            Bolum = e.Bolum,
                            BolumId = e.Id,
                            KesintiTuru = i.KesintiTipi,
                            KesintiTuruId = i.Id
                        };
            /* sadece lab olanları da eklemiştik. gerek kalmadı.
            var query2 = from b in _context.Isler
                         join c in _context.Firmalar on b.FirmaId equals c.Id
                         join d in _context.IsTurleri on b.TurId equals d.Id
                         join e in _context.Bolumler on b.BolumId equals e.Id
                         where (0 == (from z in _context.IsAyrinti where z.ısId == b.Id select z.Id).Count())
                                       orderby b.Durum, b.BolumOnay descending
                                       select new MyModel
                                       {
                                           Id = b.Id,
                                           FirmaAdi = c.FirmaAdi,
                                           FirmaId = c.Id,
                                           Tur = d.Tur,
                                           Kisi = 0,
                                           Tutar = Convert.ToDecimal(b.Tutar),
                                           Bonay = Convert.ToInt32(b.BolumOnay),
                                           Gonay = Convert.ToInt32(b.BolumOnay),
                                           IsId = b.Id,
                                           FaturaTipi = b.FaturaTipi,
                                           FaturaTarihi = b.FaturaTarihi,
                                           FaturaNo = b.FaturaNo,
                                           Bolum = e.Bolum,
                                           BolumId = e.Id
                                       };
            */
            if (IsNo != null)
            {
                query = query.Where(a => a.IsId == IsNo);
                //query2 = query2.Where(a => a.IsId == IsNo);
                ViewData["IsNo"] = IsNo;
                ViewData["Filtre"] = "filtre";
            }
            else
            {
                if (FirmaId != null)
                {
                    query = query.Where(a => a.FirmaId == FirmaId);
                    //query2 = query2.Where(a => a.FirmaId == FirmaId);
                    ViewData["Filtre"] = "filtre";
                }
                if (BolumId != null)
                {
                    query = query.Where(a => a.BolumId == BolumId);
                    //query2 = query2.Where(a => a.BolumId == BolumId);
                    ViewData["Filtre"] = "filtre";
                }
                if (KesintiTuru != null)
                {
                    query = query.Where(a => a.KesintiTuruId == KesintiTuru);
                    //query2 = query2.Where(a => a.FirmaId == FirmaId);
                    ViewData["Filtre"] = "filtre";
                }
                if (BaslamaTarihi != null)
                {
                    query = query.Where(a => a.FaturaTarihi >= BaslamaTarihi || a.FaturaTarihi == null);
                    //query2 = query2.Where(a => a.FaturaTarihi >= BaslamaTarihi);
                    ViewData["BaslamaTarihi"] = BaslamaTarihi;
                    ViewData["Filtre"] = "filtre";
                }
                if (BitisTarihi != null)
                {
                    query = query.Where(a => a.FaturaTarihi <= BitisTarihi || a.FaturaTarihi == null);
                    //query2 = query2.Where(a => a.FaturaTarihi <= BitisTarihi);
                    ViewData["BitisTarihi"] = BitisTarihi;
                    ViewData["Filtre"] = "filtre";
                }
            }

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "FirmaAdi", FirmaId);
            ViewData["KesintiTurleri"] = new SelectList(_context.Kesintitipleri, "Id", "KesintiTipi", KesintiTuru);

            var l = query.ToList();
            //query2.ToList().ForEach(a => l.Add(a));

            var ViewModel = new ViewModel { list = l };

            return View(ViewModel);
        }

        [HttpPost]
        public JsonResult KesintiGetir(int id)
        {
            var query = from a in _context.KesintitipAyrinti
                        join f in _context.Kesintitipleri on a.Id equals f.kesintiTipAyrintiId
                        join c in _context.KesintiOran on f.Id equals c.KesintiTipId
                        where c.TurId == id
                        select new
                        {
                            f.KesintiTipi,
                            c.Oran
                        };

            //veri.Kesintiler.Where(i=>i.TurId==id).ToList();

            return Json(query);
        }



        [HttpPost]
        public JsonResult KesintiPersonelGetir(string id)
        {
            var query = _context.Personeller.Where(a => a.Id == Convert.ToInt32(id)).ToList();

            return Json(query);

        }

        [HttpPost]
        public String FirmaGetir(string id)
        {
            if (id != "undefined")
            {
                var query = _context.Firmalar.Where(a => a.Id == Convert.ToInt32(id)).FirstOrDefault();

                return query.FirmaAdi;
            }
            else
                return "";
        }

        [HttpPost]
        public IActionResult KesintiKaydet()
        {
            var s = "";
            foreach (string key in Request.Form.Keys)
            {

                if (!key.Contains("__RequestVerificationToken") && !key.Contains("X-Requested-With"))
                {
                    s += "key:" + key;
                    s += "value:" + Request.Form[key];
                }
            }
            ViewBag.giden = s;
            return View("gelen");
        }


        // GET: Isler/Details/5
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

        // GET: Isler/Create
        public IActionResult Create()
        {
            ViewBag.ses=HttpContext.Session.GetString("test");

            var yetki = HttpContext.Session.GetString("yetki").ToString();
            var bolumId = Convert.ToInt32(HttpContext.Session.GetString("bolumId").ToString());
            if (yetki == "2" || yetki == "1")
            {
                ViewData["BolumId"] = new SelectList(_context.Bolumler.Where(a => a.Id == bolumId), "Id", "Bolum");
                ViewData["DigerBolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum");
            }
            else
            {
                ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum");
                ViewData["DigerBolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum");
            }
            
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "VergiNo");
            ViewData["PersonelId"] = new SelectList(_context.Personeller, "Id", "Ad");
            ViewData["Personelad"] = new SelectList(_context.Personeller, "Id", "Soyad");
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Tur");
            return View();
        }
       
        // POST: Isler/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TurId,PersonelId,Aciklama,FirmaId,Tutar,Kdv,KdvDahil,FaturaTarihi,FaturaNo,BolumOnay,Durum,BolumId")] Isler isler)
        {
            if (ModelState.IsValid)
            {
                _context.Add(isler);
                await _context.SaveChangesAsync();
            }
           
            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Id", isler.BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "Id", isler.FirmaId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.TurId);
            ViewData["IsId"] = new SelectList(_context.IsTurleri, "Id", "Id", isler.Id);
            ViewBag.isId = isler.Id;
            return  View("Isgetir");
        }

        // GET: Isler/Create
        public IActionResult CreateB()
        {
            var bolumId = Convert.ToInt32(HttpContext.Session.GetString("bolumId").ToString());
            ViewData["BolumId"] = new SelectList(_context.Bolumler.Where(a => a.Id == bolumId), "Id", "Bolum");
            ViewData["DigerBolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum");

            ViewData["PersonelId"] = new SelectList(_context.Personeller, "Id", "Ad");
            ViewData["Personelad"] = new SelectList(_context.Personeller, "Id", "Soyad");
            var labtutar = _context.Isler.Where(a => a.Lab == 1 && a.BolumId == bolumId && a.BolumOnay == 1 && a.FaturaTipi == 1).Sum(a => a.Tutar);
            if(labtutar != null)
            {
                ViewData["LabTutar"] = ((Decimal)labtutar).ToString("0.00");
            }
            else
            {
                ViewData["LabTutar"] = 0;
            }
            return View();
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
        public async Task<IActionResult> Edit(int id, [Bind("Id,BolumId,FirmaId,TurId,Tutar,Kdv,BolumOnay,FaturaTarihi,FaturaNo,FaturaTipi,Durum,Aciklama,PersonelId,Lab,HesapId,Dagitim")] Isler isler, IFormFile fatura)
        {
            if (id != isler.Id)
            {
                return NotFound();
            }
            if (fatura != null)
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
                        if (isler.Dagitim == 1)
                        {
                            isler.FaturaTipi = 1;
                        }
                        else
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

        // GET: Isler/Delete/5
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

        // POST: Isler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var isler = await _context.Isler.SingleOrDefaultAsync(m => m.Id == id);
            var iskesintiler = await _context.Iskesintiler.Where(m => m.IsId == id).ToListAsync();
            var isayrinti = await _context.IsAyrinti.Where(m => m.ısId == id).ToListAsync();
            _context.IsAyrinti.RemoveRange(isayrinti);
            _context.Iskesintiler.RemoveRange(iskesintiler);
            _context.Isler.Remove(isler);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IslerExists(int id)
        {
            return _context.Isler.Any(e => e.Id == id);
        }
        public IActionResult Isgetir()
        {
              return View();
        }

       public IActionResult Kaydet()
        {
            Isler isler = new Isler();
            try
            { 
                int id = Convert.ToInt32(Request.Form["Id"]);
                if (id > 0)
                {
                    isler = _context.Isler.Where(a => a.Id == id).FirstOrDefault();
                    //varolan kesinti ve dağıtımları sil
                    List<Iskesintiler> iskesintiler = _context.Iskesintiler.Where(a => a.IsId == isler.Id).ToList();
                    _context.Iskesintiler.RemoveRange(iskesintiler);

                    List<IsAyrinti> IsAyrinti = _context.IsAyrinti.Where(a => a.ısId == isler.Id).ToList();
                    _context.IsAyrinti.RemoveRange(IsAyrinti);
                }
                else
                    _context.Add(isler);
            }
            catch
            {

            }

            isler.Lab = Convert.ToInt32(Request.Form["lab"]);
            isler.BolumId = Convert.ToInt32(Request.Form["bolumId"]);
            isler.TurId = Convert.ToInt32(Request.Form["TurId"]);
            isler.Aciklama = Request.Form["Aciklama"].ToString();
            isler.FirmaId = Convert.ToInt32(Request.Form["FirmaId"]);            
            isler.Tutar =decimal.Parse(Request.Form["datutar"], CultureInfo.InvariantCulture);
            isler.Kdv = Convert.ToInt32(Request.Form["kdvoran"]);
            isler.PersonelId = Convert.ToInt32(HttpContext.Session.GetString("PersonelId"));
            isler.BolumOnay = 0;
            _context.SaveChanges();

            

            var gelen = _context.KesintiOran.Where(a => a.TurId == isler.TurId).ToList();
            foreach (var item in gelen)
            {
                Iskesintiler kesinti = new Iskesintiler();
                kesinti.KesintiOranId = item.Id;
                kesinti.IsId = isler.Id;
                _context.Add(kesinti);
                _context.SaveChanges();
            }

            var s = 0;
            foreach (string tc  in Request.Form["personelId"])
            {
                string anahtar = "personelId" + tc;
                s+= Convert.ToInt32(tc);
                IsAyrinti ayrinti = new IsAyrinti();
                ayrinti.ısId = isler.Id;
                ayrinti.BolumId = isler.BolumId;
                ayrinti.PersonelId =Convert.ToInt32(tc);
                ayrinti.Tutar = Convert.ToDecimal(Request.Form[anahtar], System.Globalization.CultureInfo.InvariantCulture);
                _context.Add(ayrinti);
                _context.SaveChanges();
            }
      
            return View();  
        }

    }
}
