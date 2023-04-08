using DonerSermaye.Models.Data;
using System;
using System.Linq;

namespace DonerSermaye.Models.Service
{
    public class GenelDurumService
    {
        private readonly donersermayeContext _context;

        public GenelDurumService(donersermayeContext context)
        {
            _context = context;
        }

        public FakulteGenelDurumViewModel Bolum(int bolumId)
        {
            var bolum = _context.Bolumler.FirstOrDefault(a => a.Id == bolumId);
            var devir = _context.Bolumler.Where(a => a.Id == bolumId).Select(a => a.ilkdevirtutari).FirstOrDefault();

            var toplamgelir = _context.Isler.Where(a => a.BolumId == bolumId && a.FaturaTipi == 1 && a.BolumOnay == 1).Sum(a => a.Tutar);
            var dekanlikisler = _context.Isler.Where(a => a.BolumId == 1).Select(a => a.Id).ToList();
            var bolumisler = _context.Isler.Where(a => a.BolumId == bolumId && a.FaturaTipi == 1 && a.BolumOnay == 1).Select(a => a.Id).ToList();
            //var dekanlikpayisler = _context.Isler.Where(a => a.BolumId == 1 && a.FaturaTipi == 1 && a.BolumOnay == 1).Select(a => a.Id).ToList();
            var dekanlikpayi = _context.Bolumgelir.Where(a => a.BolumId == 1 && bolumisler.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);

            var malzemepayi = _context.Bolumgelir.Where(a => a.BolumId == bolumId).Sum(a => a.Tutar);

            if (bolumId == 1)
                malzemepayi = _context.Bolumgelir.Where(a => a.BolumId == bolumId && dekanlikisler.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);

            if (bolumId == 1)
                dekanlikpayi = _context.Bolumgelir.Where(a => a.BolumId == 1 && !bolumisler.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);

            var tahsiledilmeyen = _context.Isler.Where(a => a.BolumId == bolumId && a.FaturaTipi == 2 && a.BolumOnay == 1).Sum(a => a.Tutar);

            // todo bölüm ve dekanlýk tahsil edilmeyenler bölümgelir tablosunda olmadýðý için burada tekrar hesaplama gerekiyor.,

            var te = _context.Isler.Where(a => a.BolumId == bolumId && a.FaturaTipi == 2 && a.BolumOnay == 1).ToList();

            var labtutar = _context.Isler.Where(a => a.Lab == 1 && a.BolumId == bolumId && a.BolumOnay == 1 && a.FaturaTipi == 1).Sum(a => a.Tutar);

            decimal tf = 0;
            decimal tg = 0;
            foreach (var t1 in te)
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
            var malzemepayitahsiledilmeyen = _context.Bolumgelir.Where(a => a.BolumId == bolumId && dekanlikislertahsiledilmeyen.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);
            var bolumislertahsiledilmeyen = _context.Isler.Where(a => a.BolumId == bolumId && a.FaturaTipi == 2 && a.BolumOnay == 1).Select(a => a.Id).ToList();
            var dekanlikpayitahsiledilmeyen = _context.Bolumgelir.Where(a => a.BolumId == 1 && bolumislertahsiledilmeyen.Contains(Convert.ToInt32(a.IsId))).Sum(a => a.Tutar);
            */

            var malzemepayitahsiledilmeyen = tg;

            var dekanlikpayitahsiledilmeyen = tf;

            var toplamgider = _context.Istekler.Where(a => a.BolumId == bolumId && a.Onay == 1).Sum(a => a.Fiyat);
            var kalan = Convert.ToDecimal(malzemepayi) + Convert.ToDecimal(devir) - Convert.ToDecimal(toplamgider);

            if (bolumId == 1)
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
            fgm.Id = bolumId;
            fgm.Bolum = bolum.Bolum;
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

            return fgm;
        }
    }
}