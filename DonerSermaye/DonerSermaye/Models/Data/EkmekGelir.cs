using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class EkmekGelir
    {
        public int Id { get; set; }
        public DateTime Gun { get; set; }
        public decimal Top1 { get; set; }
        public decimal Kdv1 { get; set; }
        public decimal Top8 { get; set; }
        public decimal Kdv8 { get; set; }
        public decimal Top18 { get; set; }
        public decimal Kdv18 { get; set; }
        public decimal KdvToplam { get; set; }
        public decimal KdvliToplam { get; set; }
        public decimal KdvsizToplam { get; set; }
    }
}
