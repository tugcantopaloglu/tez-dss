using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class BSBAViewModel
    {
        public int? FirmaId { get; set; }
        public string FirmaAdi { get; set; }
        public string VergiNo { get; set; }
        public decimal? Tutar { get; set; }
        public int FaturaSayisi { get; set; }
        public int eFaturaSayisi { get; set; }
    }
}
