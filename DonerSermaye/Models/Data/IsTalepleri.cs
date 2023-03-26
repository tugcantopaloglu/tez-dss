using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonerSermaye.Models.Data
{
    public partial class IsTalepleri
    {
        public int Id { get; set; }
        public int? BolumId { get; set; }
        public int? PersonelId { get; set; }
        public string Aciklama { get; set; }
        public string KararSayisi { get; set; }
        public string ToplantiSayisi { get; set; }
        public string RedSebebi { get; set; }
        public DateTime KararTarihi { get; set; }
        public int? FirmaId { get; set; }
        public int? TurId { get; set; }
        public decimal? Tutar { get; set; }
        public int? BolumOnay { get; set; }
        public Bolumler Bolum { get; set; }
        public Firmalar Firma { get; set; }
        public IsTurleri Tur { get; set; }
        public Personeller Personel { get; set; }
    }
}
