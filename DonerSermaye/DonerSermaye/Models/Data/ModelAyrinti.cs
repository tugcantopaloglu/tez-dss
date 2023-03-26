using System;

namespace DonerSermaye.Models.Data
{
    internal class ModelAyrinti
    {
        public int id { get; set; }
        public int? Firma { get; set; }
        public int? TurId { get; set; }
        public decimal? Tutar { get; set; }
        public int? Kdv { get; set; }
        public string FaturaNo { get; set; }
        public DateTime? FaturaTarihi { get; set; }
        public int PersonelId { get; set; }
        public decimal? kisitutar { get; set; }
        public int? KesintiId { get; set; }
    }
}