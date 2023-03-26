using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Bolumler
    {
        public Bolumler()
        {
            Isler = new HashSet<Isler>();
            IsTalepleri = new HashSet<IsTalepleri>();
            Istekler = new HashSet<Istekler>();
            Personeller = new HashSet<Personeller>();
        }

        public int Id { get; set; }
        public string Bolum { get; set; }
        public decimal ilkdevirtutari { get; set; }
        public KesintitipAyrinti KesintitipAyrinti { get; set; }
        public int KesintitipAyrintiId { get; set; }

        public ICollection<Isler> Isler { get; set; }
        public ICollection<Istekler> Istekler { get; set; }
        public ICollection<Personeller> Personeller { get; set; }
        public ICollection<IsTalepleri> IsTalepleri { get; set; }
    }
}
