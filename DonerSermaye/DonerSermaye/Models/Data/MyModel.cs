using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class MyModel
    {
        public int Id { get; set; }
        public string FirmaAdi { get; set; }
        public int FirmaId { get; set; }
        public string Tur { get; set; }
        public decimal Tutar { get; set; }
        public decimal Kisi { get; set; }
        public int Bonay { get; set; }

        public int IsId { get; set; }
        public int Gonay { get; set; }
        public int? FaturaTipi { get; set; }
        public decimal IsSayisi { get; set; }
        public DateTime? FaturaTarihi { get; set; }
        public string FaturaNo { get; set; }
        public string Bolum { get; set; }
        public int BolumId { get; set; }
        public string KesintiTuru { get; set; }
        public int KesintiTuruId { get; set; }
        public decimal KesintiTutar { get; set; }
        public decimal KesintiOran { get; set; }
    }
}
