using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class KesintitipAyrinti
    {
        public int Id { get; set; }
        public string Aciklama { get; set; }

        public ICollection<Kesintitipleri> Kesintitipleri { get; set; }
    }
}
