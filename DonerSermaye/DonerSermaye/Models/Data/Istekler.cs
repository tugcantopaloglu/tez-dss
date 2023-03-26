using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonerSermaye.Models.Data
{
    public partial class Istekler
    {
        public int IstekNo { get; set; }
        public string Konu { get; set; }
        public string Ozet { get; set; }
        public string TahakkukNo { get; set; }
        public int? TurNo { get; set; }
        public int? OnayNo { get; set; }
        public int? BolumId { get; set; }
        public int? FirmaId { get; set; }
        public int? Onay { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? FaturaTarihi { get; set; }
        public string FaturaNo { get; set; }
        public bool eFaturami { get; set; }
        public decimal? YaklasikMaliyet { get; set; }
        public decimal? Fiyat { get; set; }

        public Bolumler Bolum { get; set; }
        public IstekTurleri TurNoNavigation { get; set; }
        public Firmalar Firma { get; set; }
    }
}
