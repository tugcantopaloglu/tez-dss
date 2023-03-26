using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Firmalar
    {
        public Firmalar()
        {
            Isler = new HashSet<Isler>();
            Istekler = new HashSet<Istekler>();
            EkmekSiparis = new HashSet<EkmekSiparis>();
            IsTalepleri = new HashSet<IsTalepleri>();
            Arge = new HashSet<Arge>();
        }

        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public string VergiNo { get; set; }
        public string VergiDairesi { get; set; }
        public string TcKimlikNo { get; set; }
        public ICollection<Isler> Isler { get; set; }
        public ICollection<Istekler> Istekler { get; set; }
        public ICollection<EkmekSiparis> EkmekSiparis { get; set; }
        public ICollection<Arge> Arge { get; set; }
        public ICollection<IsTalepleri> IsTalepleri { get; set; }
    }
}
