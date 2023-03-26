using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class MyModel2
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public int FirmaId { get; set; }
        public int? PersonelId { get; set; }
        public string PersonelAdi { get; set; }
        public string Tur { get; set; }
        public int TurId { get; set; }
        public decimal Tutar { get; set; }
        public decimal Kisi { get; set; }
        public int IsId { get; set; }
        public int? FaturaTipi { get; set; }
        public bool faturayuklendimi { get; set; }
        public string BolumAdi { get; set; }
        public int BolumId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? FaturaTarihi { get; set; }
        public string FaturaNo { get; set; }
        public string Aciklama { get; set; }
        public decimal? KesintiToplami { get; set; }

        public int? HesapId { get; set; }
        public string HesapAdi { get; set; }

    }
}
