using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class ArgePersonel
    {
        public int Id { get; set; }
        public int ArgeId { get; set; }
        public int PersonelId { get; set; }
        public Arge Arge { get; set; }
        public Personeller Personel { get; set; }
    }
}
