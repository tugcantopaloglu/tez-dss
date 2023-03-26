using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class IsTurleri
    {
        public IsTurleri()
        {
            Isler = new HashSet<Isler>();
            IsTalepleri = new HashSet<IsTalepleri>();
        }

        public int Id { get; set; }
        public string Tur { get; set; }

        public ICollection<Isler> Isler { get; set; }
        public ICollection<IsTalepleri> IsTalepleri { get; set; }
    }
}
