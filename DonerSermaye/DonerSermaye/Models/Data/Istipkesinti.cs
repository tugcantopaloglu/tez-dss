using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Istipkesinti
    {
        public int Id { get; set; }
        public int? IsTipi { get; set; }

        public IsTurleri IsTipiNavigation { get; set; }
    }
}
