using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class IstekTurleri
    {
        public IstekTurleri()
        {
            Istekler = new HashSet<Istekler>();
        }

        public int TurNo { get; set; }
        public string Tur { get; set; }

        public ICollection<Istekler> Istekler { get; set; }
    }
}
