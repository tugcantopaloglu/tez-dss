using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class FakulteGenelDurumViewModel
    {
        public int Id { get; set; }
        public string Bolum { get; set; }
        public decimal? DevirOncekiYil { get; set; }
        public decimal? Gelir { get; set; }
        public decimal? LabGelir { get; set; }
        public decimal? MalzemePayi { get; set; }
        public decimal? DekanlikPayi { get; set; }
        public decimal? TahsilEdilmeyenGelir { get; set; }
        public decimal? TahsilEdilmeyenGelirMalzemePayi { get; set; }
        public decimal? TahsilEdilmeyenGelirDekanlikPayi { get; set; }
        public decimal? Gider { get; set; }
        public decimal? Kalan { get; set; }
        public decimal? NetDurum { get; set; }
    }
}
