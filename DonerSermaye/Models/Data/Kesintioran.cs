using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class KesintiOran
    {
        public int Id { get; set; }
        public decimal? Oran { get; set; }
        public int? KesintiTipId { get; set; }
        public int? TurId { get; set; }
        public Kesintitipleri KesintiTip { get; set; }
        public IsTurleri Tur { get; set; }

    }
}
