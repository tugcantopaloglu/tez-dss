using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Bolumgelir
    {
        public int Id { get; set; }
        public int? BolumId { get; set; }
        public decimal? Tutar { get; set; }
        public int? IsId { get; set; }
        public string Dagitim { get; set; }
    }
}
