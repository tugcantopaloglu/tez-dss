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

namespace DonerSermaye.Controllers
{
    public class IslerController : Controller
    {
        private readonly donersermayeContext _context;

        public IslerController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: Isler
        public IActionResult Index()
        {
            var sum = (from a in _context.IsAyrinti
                       join b in _context.Isler on a.ısId equals b.Id
                       join c in _context.Firmalar on b.FirmaId equals c.Id
                       join d in _context.IsTurleri on b.TurId equals d.Id
                       where (a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")))
                       select new { b.Id }).Count();

            var sumonay = (from a in _context.IsAyrinti
                           join b in _context.Isler on a.ısId equals b.Id
                           join c in _context.Firmalar on b.FirmaId equals c.Id
                           join d in _context.IsTurleri on b.TurId equals d.Id
                           where (a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")) && b.BolumOnay == 1)
                           select new { b.Id }).Count();

            var bekleyen = (from a in _context.IsAyrinti
                            join b in _context.Isler on a.ısId equals b.Id
                            join c in _context.Firmalar on b.FirmaId equals c.Id
                            join d in _context.IsTurleri on b.TurId equals d.Id
                            where (a.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")) && b.BolumOnay == 0)
                            select new { b.Id }).Count();

            var kisitoplam = (from a in _context.Isler
                              join b in _context.IsAyrinti on a.Id equals b.ısId
                              where b.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId"))
                              select b.Tutar).Sum();

            var toplam = (from a in _context.Isler
                          join b in _context.IsAyrinti on a.Id equals b.ısId
                          where b.Id == Convert.ToInt32(HttpContext.Session.GetString("PersonelId"))
                          select a.Tutar).Sum();

            var onayodeme = (from a in _context.Isler
                             join b in _context.IsAyrinti on a.Id equals b.ısId
                             where (b.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")) && a.Durum == 1)
                             select b.Tutar).Sum();

            var query = from b in _context.Isler
                        join a in _context.IsAyrinti
                            on b.Id equals a.ısId into g
                        from a in g.DefaultIfEmpty()
                        join c in _context.Firmalar on b.FirmaId equals c.Id
                        join d in _context.IsTurleri on b.TurId equals d.Id
                        where (b.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")))
                        orderby b.Durum, b.BolumOnay descending
                        select new MyModel
                        {
                            FirmaAdi = c.FirmaAdi,
                            Tur = d.Tur,
                            
                            Tutar = Convert.ToDecimal(b.Tutar),
                            Bonay = Convert.ToInt32(b.BolumOnay),
                            Gonay = Convert.ToInt32(b.Dagitim),
                            IsId = b.Id,
                            FaturaTipi = b.FaturaTipi,
                            FaturaTarihi = b.FaturaTarihi,
                            FaturaNo = b.FaturaNo
                        }; 

            var query2 = from b in _context.Isler
                                       join c in _context.Firmalar on b.FirmaId equals c.Id
                                       join d in _context.IsTurleri on b.TurId equals d.Id
                                       where (b.PersonelId == Convert.ToInt32(HttpContext.Session.GetString("PersonelId")) && 0 == (from z in _context.IsAyrinti where z.ısId == b.Id select z.Id).Count())
                                       orderby b.Durum, b.BolumOnay descending
                                       select new MyModel
                                       {
                                           Id = b.Id,
                                           FirmaAdi = c.FirmaAdi,
                                           Tur = d.Tur,
                                           Kisi = 0,
                                           Tutar = Convert.ToDecimal(b.Tutar),
                                           Bonay = Convert.ToInt32(b.BolumOnay),
                                           Gonay = Convert.ToInt32(b.BolumOnay),
                                           IsId = b.Id,
                                           FaturaTipi = b.FaturaTipi,
                                           FaturaTarihi = b.FaturaTarihi,
                                           FaturaNo = b.FaturaNo
                                       };
            var l = query.ToList();
            query2.ToList().ForEach(a => l.Add(a));

            var ViewModel = new ViewModel { list = l };

            ViewBag.sayi = sum;
            ViewBag.onaylanan = sumonay;
            ViewBag.bekleyen = bekleyen;
            ViewBag.toplam = String.Format("{0:0.##}", toplam);
            ViewBag.onayodeme = String.Format("{0:0.##}", onayodeme);
            ViewBag.kisi = String.Format("{0:0.##}", kisitoplam);
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

        // GET: Isler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yetki = HttpContext.Session.GetString("yetki").ToString();
            var isler = await _context.Isler.SingleOrDefaultAsync(m => m.Id == id);
            if (isler == null)
            {
                return NotFound();
            }
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

            ViewData["BolumId"] = new SelectList(_context.Bolumler, "Id", "Bolum", isler.BolumId);
            ViewData["FirmaId"] = new SelectList(_context.Firmalar, "Id", "VergiNo", isler.FirmaId);
            ViewData["TurId"] = new SelectList(_context.IsTurleri, "Id", "Tur", isler.TurId);

            return View(isler);
        }

        // POST: Isler/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TurId,PersonelId,Aciklama,FirmaId,Tutar,Kdv,KdvDahil,FaturaTarihi,FaturaNo,BolumOnay,Durum,BolumId")] Isler isler)
        {
            if (id != isler.Id)
            {
                return NotFound();
            }
            isler.BolumOnay = 0;
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
