using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DonerSermaye.Models.Data;
using Microsoft.AspNetCore.Http;


namespace DonerSermaye.Controllers
{
    
    public class IsgetirController : Controller
    {
        private donersermayeContext veri= new donersermayeContext();

        [HttpPost]
        public JsonResult listele(int id)
        {
            var query = (from a in veri.Isler
                         join c in veri.IsAyrinti on a.Id equals c.ısId
                         join d in veri.Personeller on c.PersonelId equals d.Id
                         join b in veri.Bolumler on d.BolumId equals b.Id
                         join e in veri.Unvanlar on d.UnvanId equals e.Id
                         where a.HesapId==id
                         group new { c, b, d, e } by new { d.Ad, d.Soyad, e.Unvan, b.Bolum, e.Id } into g
                         select new {
                             
                             Ad = g.Key.Ad, Soyad = g.Key.Soyad, Unvan = g.Key.Unvan, UnvanID = g.Key.Id, Tutar = g.Sum( a=> a.c.Tutar)
                         }
                         
                         ).ToList().OrderBy(a => a.UnvanID);
          
            


            //var query = veri.Istipkesinti.Where(a => a.IsTipi == id);


            return Json(query);
            
        }

        [HttpPost]
        public JsonResult argelistele(int id)
        {
            var query = (from a in veri.Isler
                         join c in veri.IsAyrinti on a.Id equals c.ısId
                         join d in veri.Personeller on c.PersonelId equals d.Id
                         join b in veri.Bolumler on d.BolumId equals b.Id
                         join e in veri.Unvanlar on d.UnvanId equals e.Id
                         where a.HesapId == id                         
                         select new
                         {
                             Ad = d.Ad,
                             Soyad = d.Soyad,
                             Unvan = e.Unvan,
                             UnvanID = e.Id,
                             Tutar = c.Tutar,
                             ArgeNo = a.Aciklama
                         }

                         ).ToList().OrderBy(a => a.UnvanID);




            //var query = veri.Istipkesinti.Where(a => a.IsTipi == id);


            return Json(query);

        }




    }
}