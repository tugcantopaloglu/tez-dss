using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class EkmekSiparis
    {
        public int Id { get; set; }
        public DateTime? SiparisTarihi { get; set; }
        public int? EkmekTipleriId { get; set; }
        public int? Adet { get; set; }
        public double? ToplamFiyat { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public int? FirmaId { get; set; }

        public EkmekTipleri EkmekTipleri { get; set; }
        public Firmalar Firma { get; set; }
    }
}
