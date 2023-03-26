using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class FaturaTipi
    {
        public FaturaTipi()
        {
            Isler = new HashSet<Isler>();
        }

        public int Id { get; set; }
        public string FaturaTipi1 { get; set; }

        public ICollection<Isler> Isler { get; set; }
    }
}
