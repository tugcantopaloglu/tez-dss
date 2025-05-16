using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class EkmekFirma
    {
        public int Id { get; set; }
        public string Firma { get; set; } = "DUMMY_FIRMA";
        public string KullaniciAdi { get; set; } = "DUMMY_KULLANICI_ADI";
        public string Sifre { get; set; } = "DUMMY_SIFRE";
        public int? YetkiId { get; set; }
    }
}
