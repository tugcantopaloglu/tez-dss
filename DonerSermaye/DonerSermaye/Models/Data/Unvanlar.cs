using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Unvanlar
    {
        public Unvanlar()
        {
            Personeller = new HashSet<Personeller>();
        }

        public int Id { get; set; }
        public string Unvan { get; set; }

        public ICollection<Personeller> Personeller { get; set; }
    }
}
