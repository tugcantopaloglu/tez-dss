using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Kesintitipleri
    {
        public int Id { get; set; }
        public string KesintiTipi { get; set; }
        public int? kesintiTipAyrintiId { get; set; }
        public KesintitipAyrinti kesintiTipAyrinti { get; set; }
       
    }
}
