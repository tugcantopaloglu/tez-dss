using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class EkmekSiparisViewModel
    {
        public int Id { get; set; }
        public DateTime? TeslimTarihi { get; set; }
        public string EkmekTipi { get; set; }
        public int? Adet { get; set; }
        public double? ToplamFiyat { get; set; }
        public double? BirimFiyat { get; set; }
        public string FirmaAdi { get; set; }
        public int? FirmaId { get; set; }
    }
}
