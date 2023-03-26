using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class IsAyrintiViewModel
    {
        public int? PersonelId { get; set; }
        public string Unvan { get; set; }
        public int? UnvanId { get; set; }
        public string AdSoyad { get; set; }
        public decimal? Tutar { get; set; }
        public string Aciklama { get; set; }
    }
}
