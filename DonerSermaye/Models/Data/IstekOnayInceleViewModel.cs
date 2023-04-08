using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonerSermaye.Models.Data
{
    public class IstekOnayInceleViewModel
    {
        public int Id { get; set; }
        public string Bolum { get; set; }       
        public decimal Kalan { get; set; }
        public string FaturaNo { get; set; }
        public int? OnayNo { get; set; }
        public decimal BekleyenYaklasikMaliyet { get; set; }
    }
}