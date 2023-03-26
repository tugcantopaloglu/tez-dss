using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Yetkiler
    {
        public Yetkiler()
        {
            Personeller = new HashSet<Personeller>();
        }

        public int Id { get; set; }
        public string Yetki { get; set; }

        public ICollection<Personeller> Personeller { get; set; }
    }
}
