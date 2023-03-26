using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public partial class IstekAyrinti
    {
        public int Id { get; set; }
        public int IstekId { get; set; }
        public string IstekAd { get; set; }
        public int? IstekAdet { get; set; }
        public decimal? YaklasikMaliyet { get; set; }

        public Istekler Istek { get; set; }
        
    }
}
