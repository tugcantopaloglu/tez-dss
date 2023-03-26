using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Duyurular
    {
        public int DuyuruNo { get; set; }
        public string Baslik { get; set; }
        public string Duyuru { get; set; }
        public DateTime? Tarih { get; set; }
        public int? Gonderen { get; set; }

        public Personeller GonderenNavigation { get; set; }
    }
}
