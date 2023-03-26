using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Hesaplar
    {
        public Hesaplar()
        {
            Isler = new HashSet<Isler>();
        }

        public int Id { get; set; }
        public string Hesap { get; set; }
        public DateTime ToplantiTarihi { get; set; }
        public int ToplantiSayisi { get; set; }
        public int KararSayisi { get; set; }

        public ICollection<Isler> Isler { get; set; }
    }
}
