using System;
using System.Collections.Generic;

namespace DonerSermaye.Models.Data
{
    public partial class Personeller
    {
        public Personeller()
        {
            Duyurular = new HashSet<Duyurular>();
            IsAyrinti = new HashSet<IsAyrinti>();
            Isler = new HashSet<Isler>();
            ArgePersonel = new HashSet<ArgePersonel>();
            Arge = new HashSet<Arge>();
            IsTalepleri = new HashSet<IsTalepleri>();
        }

        public int Id { get; set; }
        public int? BolumId { get; set; }
        public int? UnvanId { get; set; }
        public int? YetkiId { get; set; }
        public string TcKimlikNo { get; set; } = "DUMMY_TC_NO";
        public string Ad { get; set; } = "DUMMY_AD";
        public string Soyad { get; set; } = "DUMMY_SOYAD";
        public string Iban { get; set; } = "DUMMY_IBAN";
        public string EPosta { get; set; } = "DUMMY_EMAIL";
        public string Sifre { get; set; } = "DUMMY_PASSWORD";

        public Bolumler Bolum { get; set; }
        public Unvanlar Unvan { get; set; }
        public Yetkiler Yetki { get; set; }
        public ICollection<Duyurular> Duyurular { get; set; }
        public ICollection<IsAyrinti> IsAyrinti { get; set; }
        public ICollection<Isler> Isler { get; set; }
        public ICollection<ArgePersonel> ArgePersonel { get; set; }
        public ICollection<Arge> Arge { get; set; }
        public ICollection<IsTalepleri> IsTalepleri { get; set; }
    }
}
