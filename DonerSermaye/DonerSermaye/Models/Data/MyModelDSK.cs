using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class MyModelDSK
    {
        public int Id { get; set; }
        public string Tur { get; set; }
        public int? TurId { get; set; }
        public string OgretimUyesi { get; set; }
        public decimal? Tutar { get; set; }
        public decimal? Kisi { get; set; }
        public string BolumAdi { get; set; }
        public int? BolumId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}")]
        public DateTime? FaturaTarihi { get; set; }
        public string Aciklama { get; set; }
        public decimal? KesintiToplami { get; set; }

        public int? HesapId { get; set; }
        public string HesapAdi { get; set; }

    }
}
