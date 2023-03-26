using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class EkmekFirma
    {
        public int Id { get; set; }
        public string Firma { get; set; }
        public string KullaniciAdi { get; set; }
        public string Sifre { get; set; }
        public int? YetkiId { get; set; }
    }
}
