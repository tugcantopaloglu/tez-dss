using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class EkmekTipleri
    {
        public EkmekTipleri()
        {
            EkmekSiparis = new HashSet<EkmekSiparis>();
        }

        public int Id { get; set; }
        public string EkmekTipi { get; set; }
        public double? BirimFiyat { get; set; }
        public double? Kdv { get; set; }

        public ICollection<EkmekSiparis> EkmekSiparis { get; set; }
    }
}
