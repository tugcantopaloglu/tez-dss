using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Iskesintiler
    {
        public int Id { get; set; }
        public int? IsId { get; set; }
        public int KesintiOranId { get; set; }

        public Isler Is { get; set; }
    }
}
