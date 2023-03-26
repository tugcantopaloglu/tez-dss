using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class IsAyrinti
    {
        public int Id { get; set; }
        public int ısId { get; set; }
        public int? PersonelId { get; set; }
        public decimal? Tutar { get; set; }
        public int? BolumId { get; set; }
        public Personeller Personel { get; set; }
        public Isler ıs { get; set; }
    }
}
