using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DonerSermaye.Controllers
{
    public class BilgiController : Controller
    {
        private donersermayeContext _context = new donersermayeContext();

        [HttpPost]
        public JsonResult BolumGetir()
        {
            var query = _context.Bolumler.ToList();
            
            return Json(query);
        }

        [HttpPost]
        public JsonResult BilgiGetir(int id)
        {
            /*eski hali
             * var bolumpayi = from a in _context.Isler
                            join b in _context.Iskesintiler on a.Id equals b.IsId
                            join e in _context.KesintiOran on b.KesintiOranId equals e.Id
                            join d in _context.Kesintitipleri on e.KesintiTip.Id equals d.Id
                            join f in _context.Bolumler on a.BolumId equals f.Id
                            where d.kesintiTipAyrintiId == f.KesintitipAyrintiId && a.BolumId == id && a.BolumOnay == 1 && a.FaturaTipi == 1
                            select new { a.Tutar, e.Oran };*/
            JsonResult j;
            if (id == 998)
            {

                CultureInfo ci = new CultureInfo("tr-TR");
                var month = DateTime.Now.AddMonths(-1).ToString("MMMM", ci);
                ViewData["m"] = month + '-' + DateTime.Now.Year.ToString();
                var donersermayeContext = _context.Isler.Include(i => i.IsAyrinti).ThenInclude(a => a.Personel).
                    Include(i => i.Bolum).Include(i => i.Firma).
                    Include(i => i.Tur).Where(i => i.IsAyrinti.Count() > 0 && i.Durum == 0 && i.TurId != 3 && i.Lab != 1 && i.HesapId == null && i.FaturaTipi == 1);

                decimal dt = 0;
                foreach (var ii in donersermayeContext)
                {
                    foreach (var zz in ii.IsAyrinti)
                    {
                        dt += Convert.ToDecimal(zz.Tutar);
                    }
                }

                j = new JsonResult("{\"ff\": " + id + ", \"top\":" + ((Decimal)dt).ToString("0.00") + "}");

            }
            else if (id == 999)
            {

                CultureInfo ci = new CultureInfo("tr-TR");
                var month = DateTime.Now.AddMonths(-1).ToString("MMMM", ci);
                // arge dağıtımı id 3 olarak veritabanında belirtildi. iş türlerinde arge'nin id'si ne ise bu sorguda da o gelmeli. 19-10-2021 - Yusuf
                var donersermayeContext = _context.Isler.Include(i => i.IsAyrinti).ThenInclude(a => a.Personel).Include(i => i.Bolum).Include(i => i.Firma).Include(i => i.Tur).Where(i => i.Durum == 0 && i.TurId == 3 && i.Lab != 1 && i.HesapId == null && i.FaturaTipi == 1);

                decimal dt = 0;
                foreach (var ii in donersermayeContext)
                {
                    foreach (var zz in ii.IsAyrinti)
                    {
                        dt += Convert.ToDecimal(zz.Tutar);
                    }
                }

                j = new JsonResult("{\"ff\": " + id + ", \"top\":" + ((Decimal)dt).ToString("0.00") + "}");
            }
            else
            { 
                var bolumpayi = from a in _context.Bolumgelir where a.BolumId == id select new {a.Tutar};
                var bolumpayi2 = bolumpayi.ToList();
                decimal s = 0;
            
                decimal tmpvalue;
                decimal tmpvalue2;
            
                foreach (var item in bolumpayi2)
                {
                    /*
                    decimal? tt = decimal.TryParse(item.Tutar.ToString(), out tmpvalue) ?
                      tmpvalue : (decimal?)null;
                    decimal? oo = decimal.TryParse(item.Oran.ToString(), out tmpvalue2) ?
                      tmpvalue2 : (decimal?)null;
                    */
                    decimal? tt = decimal.TryParse(item.Tutar.ToString(), out tmpvalue) ? s:(decimal?)null;
                    s += (decimal)tmpvalue;
                }

            
                // todo: 2023 ve sonrasında geçmiş yıl peşin fatura toplamları listelenmeli. şimdilik sadece geçen yıldan devir tutarı alınıyor.

                //var gecmisyil = 2021;

                var gecmisyiltoplami = _context.Bolumler.Where(a => a.Id == id).Select(a => a.ilkdevirtutari).FirstOrDefault();


                var sum = (from z in _context.Istekler where z.BolumId == id select z.Fiyat).Sum();
                if (sum == null)
                    sum = 0;
                var bb = s + gecmisyiltoplami - sum;
                j = new JsonResult("{\"ff\": "+id+", \"top\":"+bb+"}");
            }
            return j;
        }

        [HttpPost]
        public JsonResult Bilpay()
        {
            var query = from a in _context.Isler
                        join b in _context.Iskesintiler on a.Id equals b.IsId
                        join e in _context.KesintiOran on b.KesintiOranId equals e.Id
                        join d in _context.Kesintitipleri on e.KesintiTip.Id equals d.Id
                        where a.BolumId == 1 && a.Dagitim == 1
                        select new
                        {
                            bolum=a.BolumId,
                            miktar = e.Oran * Convert.ToDecimal(a.Tutar) / 100

                        } into x
                        group x by new { x.bolum } into g
                        select new
                        {
                            top = g.Sum(i => i.miktar)
                        };

            return Json(query);
        }
    }
}