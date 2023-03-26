using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DonerSermaye.Models.Data
{
    public partial class Isler
    {
        public Isler()
        {
            IsAyrinti = new HashSet<IsAyrinti>();
            Iskesintiler = new HashSet<Iskesintiler>();
        }

        public int Id { get; set; }
        public int? BolumId { get; set; }
        public int? PersonelId { get; set; }
        public string Aciklama { get; set; }
        public int? FirmaId { get; set; }
        public int? TurId { get; set; }
        public decimal? Tutar { get; set; }
        public int? Kdv { get; set; }
        public int? BolumOnay { get; set; }
        public int? FaturaTipi { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? FaturaTarihi { get; set; }
        public string FaturaNo { get; set; }
        public bool faturayuklendimi { get; set; }
        public int? Durum { get; set; }
        public int? HesapId { get; set; }
        public int? Dagitim { get; set; }
        public int? Lab { get; set; }

        public Bolumler Bolum { get; set; }
        public FaturaTipi FaturaTipiNavigation { get; set; }
        public Firmalar Firma { get; set; }
        public Hesaplar Hesap { get; set; }
        public IsTurleri Tur { get; set; }
        public Personeller Personel { get; set; }
        public ICollection<IsAyrinti> IsAyrinti { get; set; }
        public ICollection<Iskesintiler> Iskesintiler { get; set; }
    }
}
