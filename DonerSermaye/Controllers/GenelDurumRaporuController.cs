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
    public class GenelDurumRaporuController : Controller
    {
        private readonly donersermayeContext _context;

        public GenelDurumRaporuController(donersermayeContext context)
        {
            _context = context;
        }

        // GET: IstekGir
        public async Task<IActionResult> Index()
        {
            var m = new List<FakulteGenelDurumViewModel>();

            // todo: yıl eklenecek. şimdilik hepsini veriyor.
            var zz = from a in _context.Bolumler select new FakulteGenelDurumViewModel { Id= a.Id, Bolum = a.Bolum };


            if (HttpContext.Session.GetString("yetki") == "1")
            {
                zz = zz.Where(a => a.Id == Convert.ToInt32(HttpContext.Session.GetString("bolumId")));
            }

            foreach (var z in zz)
            {
                var devir = _context.Bolumler.Where(a => a.Id == z.Id).Select(a => a.ilkdevirtutari).FirstOrDefault();

                var toplamgelir = _context.Isler.Where(a => a.BolumId == z.Id && a.FaturaTipi == 1 && a.BolumOnay == 1).Sum(a => a.Tutar);
                var dekanlikisler = _context.Isler.Where(a => a.BolumId == 1).Select(a => a.Id).ToList();
                var bolumisler = _context.Isler.Where(a => a.BolumId == z.Id && a.FaturaTipi == 1 && a.BolumOnay == 1).Select(a => a.Id).ToList();
                //var dekanlikpayisler = _context.Isler.Where(a => a.BolumId == 1 && a.FaturaTipi == 1 && a.BolumOnay == 1).Select(a => a.Id).ToList();
                var dekanlikpayi = _context.Bolumgelir.Where(a => a.BolumId == 1 && bolumisler.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);

                var malzemepayi = _context.Bolumgelir.Where(a => a.BolumId == z.Id).Sum(a => a.Tutar);

                if (z.Id == 1)
                    malzemepayi = _context.Bolumgelir.Where(a => a.BolumId == z.Id && dekanlikisler.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);

                if (z.Id == 1)
                    dekanlikpayi = _context.Bolumgelir.Where(a => a.BolumId == 1 && !bolumisler.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);

                var tahsiledilmeyen = _context.Isler.Where(a => a.BolumId == z.Id && a.FaturaTipi == 2 && a.BolumOnay == 1).Sum(a => a.Tutar);

                // todo bölüm ve dekanlık tahsil edilmeyenler bölümgelir tablosunda olmadığı için burada tekrar hesaplama gerekiyor.,

                var te = _context.Isler.Where(a => a.BolumId == z.Id && a.FaturaTipi == 2 && a.BolumOnay == 1).ToList();

                var labtutar = _context.Isler.Where(a => a.Lab == 1 && a.BolumId == z.Id && a.BolumOnay == 1 && a.FaturaTipi == 1).Sum(a => a.Tutar);

                decimal tf = 0;
                decimal tg = 0;
                foreach(var t1 in te)
                {
                    var sorgu = from a in _context.Isler
                                join b in _context.Iskesintiler on a.Id equals b.IsId
                                join c in _context.KesintiOran on b.KesintiOranId equals c.Id
                                join d in _context.Kesintitipleri on c.KesintiTipId equals d.Id
                                join e in _context.KesintitipAyrinti on d.kesintiTipAyrintiId equals e.Id
                                where a.Id == t1.Id && e.Id == 1
                                select new
                                {
                                    Aciklama = e.Aciklama,
                                    Oran = c.Oran,
                                    Tutar = (a.Tutar * c.Oran) / 100
                                }.Tutar;
                    tf += Convert.ToDecimal(sorgu.FirstOrDefault());

                    var sorgu2 = from a in _context.Isler
                                join b in _context.Iskesintiler on a.Id equals b.IsId
                                join c in _context.KesintiOran on b.KesintiOranId equals c.Id
                                join d in _context.Kesintitipleri on c.KesintiTipId equals d.Id
                                join e in _context.KesintitipAyrinti on d.kesintiTipAyrintiId equals e.Id
                                where a.Id == t1.Id && e.Id == 2
                                select new
                                {
                                    Aciklama = e.Aciklama,
                                    Oran = c.Oran,
                                    Tutar = (a.Tutar * c.Oran) / 100
                                }.Tutar;
                    tg += Convert.ToDecimal(sorgu2.FirstOrDefault());
                }
                /*
                var dekanlikislertahsiledilmeyen = _context.Isler.Where(a => a.BolumId == 1 && a.FaturaTipi == 2 && a.BolumOnay == 1).Select(a => a.Id).ToList();
                var malzemepayitahsiledilmeyen = _context.Bolumgelir.Where(a => a.BolumId == z.Id && dekanlikislertahsiledilmeyen.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);
                var bolumislertahsiledilmeyen = _context.Isler.Where(a => a.BolumId == z.Id && a.FaturaTipi == 2 && a.BolumOnay == 1).Select(a => a.Id).ToList();
                var dekanlikpayitahsiledilmeyen = _context.Bolumgelir.Where(a => a.BolumId == 1 && bolumislertahsiledilmeyen.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);
                */

                var malzemepayitahsiledilmeyen = tg;

                var dekanlikpayitahsiledilmeyen = tf;

                var toplamgider = _context.Istekler.Where(a => a.BolumId == z.Id && a.Onay == 1).Sum(a => a.Fiyat);
                var kalan = Convert.ToDecimal(malzemepayi) + Convert.ToDecimal(devir) - Convert.ToDecimal(toplamgider);

                if (z.Id == 1)
                    kalan = Convert.ToDecimal(malzemepayi) + Convert.ToDecimal(dekanlikpayi) + Convert.ToDecimal(devir) - Convert.ToDecimal(toplamgider);

                var net = Convert.ToDecimal(malzemepayitahsiledilmeyen) + Convert.ToDecimal(kalan) + Convert.ToDecimal(labtutar);

                if (toplamgelir == null)
                    toplamgelir = 0;
                if (malzemepayi == null)
                    malzemepayi = 0;
                if (dekanlikpayi == null)
                    dekanlikpayi = 0;
                if (tahsiledilmeyen == null)
                    tahsiledilmeyen = 0;
                if (labtutar == null)
                    labtutar = 0;
                if (toplamgider == null)
                    toplamgider = 0;

                FakulteGenelDurumViewModel fgm = new FakulteGenelDurumViewModel();
                fgm.Id = z.Id;
                fgm.Bolum = z.Bolum;
                fgm.DevirOncekiYil = devir;
                fgm.Gelir = toplamgelir;
                fgm.MalzemePayi = malzemepayi;
                fgm.DekanlikPayi = dekanlikpayi;
                fgm.TahsilEdilmeyenGelir = tahsiledilmeyen;
                fgm.LabGelir = labtutar;
                fgm.TahsilEdilmeyenGelirMalzemePayi = malzemepayitahsiledilmeyen;
                fgm.TahsilEdilmeyenGelirDekanlikPayi = dekanlikpayitahsiledilmeyen;
                fgm.Gider = toplamgider;
                fgm.Kalan = kalan;
                fgm.NetDurum = net;
                m.Add(fgm);
            }

            return View(m);
        }

    }
}
