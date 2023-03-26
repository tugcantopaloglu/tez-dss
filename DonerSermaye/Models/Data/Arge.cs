using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Arge
    {
        public Arge()
        {
            ArgePersonel = new HashSet<ArgePersonel>();
        }
        public int Id { get; set; }
        public string Aciklama { get; set; }
        public string ArgeNo { get; set; }
        public double ProjeTutari { get; set; }
        public int FirmaId { get; set; }
        public Firmalar Firma { get; set; }
        public int PersonelId { get; set; }
        public Personeller Personel { get; set; }
        public DateTime Baslama { get; set; }
        public DateTime Bitis { get; set; }
        public ICollection<ArgePersonel> ArgePersonel { get; set; }
    }
}
