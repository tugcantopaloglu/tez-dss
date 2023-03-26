using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace DonerSermaye.Models.Data
{
    public partial class donersermayeContext : DbContext
    {
        public virtual DbSet<Bolumgelir> Bolumgelir { get; set; }
        public virtual DbSet<Arge> Arge { get; set; }
        public virtual DbSet<ArgePersonel> ArgePersonel { get; set; }
        public virtual DbSet<Bolumler> Bolumler { get; set; }
        public virtual DbSet<Duyurular> Duyurular { get; set; }
        public virtual DbSet<EkmekFirma> EkmekFirma { get; set; }
        public virtual DbSet<EkmekSiparis> EkmekSiparis { get; set; }
        public virtual DbSet<EkmekTipleri> EkmekTipleri { get; set; }
        public virtual DbSet<FaturaTipi> FaturaTipi { get; set; }
        public virtual DbSet<Firmalar> Firmalar { get; set; }
        public virtual DbSet<Hesaplar> Hesaplar { get; set; }
        public virtual DbSet<IsAyrinti> IsAyrinti { get; set; }
        public virtual DbSet<Iskesintiler> Iskesintiler { get; set; }
        public virtual DbSet<Isler> Isler { get; set; }
        public virtual DbSet<IsTalepleri> IsTalepleri { get; set; }
        public virtual DbSet<Istekler> Istekler { get; set; }
        public virtual DbSet<IstekTurleri> IstekTurleri { get; set; }
        public virtual DbSet<IsTurleri> IsTurleri { get; set; }
        public virtual DbSet<KesintiOran> KesintiOran { get; set; }
        public virtual DbSet<Kesintitipleri> Kesintitipleri { get; set; }
        public virtual DbSet<KesintitipAyrinti> KesintitipAyrinti { get; set; }
        public virtual DbSet<Personeller> Personeller { get; set; }
        public virtual DbSet<Raporlar> Raporlar { get; set; }
        public virtual DbSet<Unvanlar> Unvanlar { get; set; }
        public virtual DbSet<Yetkiler> Yetkiler { get; set; }
        public virtual DbSet<Ayarlar> Ayarlar { get; set; }
        public virtual DbSet<EkmekGelir> EkmekGelir { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
               optionsBuilder.UseSqlServer("Server=2HP-Desktop;Database=test;Integrated Security=True;", builder =>
                //optionsBuilder.UseSqlServer("Server=DESKTOP-56L7D24\SQLEXPRESS01;User Id=muhuser;password=577mh577;Database=donersermayenew;", builder =>
                {
                    builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                });
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ayarlar>(entity =>
            {
                entity.ToTable("ayarlar");

                entity.Property(e => e.BSBALimiti).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Bolumgelir>(entity =>
            {
                entity.ToTable("bolumgelir");

                entity.Property(e => e.Dagitim).HasColumnType("nchar(20)");

                entity.Property(e => e.Tutar).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Bolumler>(entity =>
            {
                entity.ToTable("bolumler");

                entity.Property(e => e.Bolum)
                    .HasColumnName("bolum")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Duyurular>(entity =>
            {
                entity.HasKey(e => e.DuyuruNo);

                entity.ToTable("duyurular");

                entity.Property(e => e.DuyuruNo).HasColumnName("Duyuru_No");

                entity.Property(e => e.Baslik)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Duyuru).HasColumnType("text");

                entity.Property(e => e.Tarih).HasColumnType("datetime");

                entity.HasOne(d => d.GonderenNavigation)
                    .WithMany(p => p.Duyurular)
                    .HasForeignKey(d => d.Gonderen)
                    .HasConstraintName("FK_duyurular_personeller");
            });

            modelBuilder.Entity<EkmekFirma>(entity =>
            {
                entity.ToTable("ekmekFirma");

                entity.Property(e => e.Firma)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.KullaniciAdi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sifre)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.YetkiId).HasColumnName("yetkiId");
            });

            modelBuilder.Entity<EkmekSiparis>(entity =>
            {
                entity.ToTable("ekmekSiparis");

                entity.Property(e => e.TeslimTarihi).HasColumnType("date");

                entity.HasOne(d => d.EkmekTipleri)
                    .WithMany(p => p.EkmekSiparis)
                    .HasForeignKey(d => d.EkmekTipleriId)
                    .HasConstraintName("FK_ekmekSiparis_ekmektipleri");

                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.EkmekSiparis)
                    .HasForeignKey(d => d.FirmaId)
                    .HasConstraintName("FK_ekmekSiparis_personeller");
            });

            modelBuilder.Entity<EkmekTipleri>(entity =>
            {
                entity.ToTable("ekmekTipleri");

                entity.Property(e => e.EkmekTipi)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<FaturaTipi>(entity =>
            {
                entity.ToTable("faturaTipi");

                entity.Property(e => e.FaturaTipi1)
                    .HasColumnName("FaturaTipi")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Firmalar>(entity =>
            {
                entity.ToTable("firmalar");

                entity.Property(e => e.FirmaAdi)
                    .HasColumnName("firmaAdi")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TcKimlikNo)
                    .HasColumnName("tcKimlikNo")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.VergiNo)
                    .HasColumnName("vergiNo")
                    .HasMaxLength(25)
                    .IsUnicode(false);

                entity.Property(e => e.VergiDairesi)
                    .HasColumnName("vergiDairesi")
                    .HasMaxLength(25);
            });

            modelBuilder.Entity<Firmalar>().HasIndex(u => u.VergiNo).IsUnique();

            modelBuilder.Entity<Hesaplar>(entity =>
            {
                entity.ToTable("hesaplar");

                entity.Property(e => e.Hesap)
                    .HasColumnName("hesap")
                    .HasColumnType("nchar(30)");
            });

            modelBuilder.Entity<IsAyrinti>(entity =>
            {
                entity.ToTable("isAyrinti");

                entity.Property(e => e.BolumId).HasColumnName("bolumId");

                entity.Property(e => e.PersonelId).HasColumnName("personelId");

                entity.Property(e => e.Tutar).HasColumnType("money");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.IsAyrinti)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("FK_isAyrinti_personeller");

                entity.HasOne(d => d.ıs)
                    .WithMany(p => p.IsAyrinti)
                    .HasForeignKey(d => d.ısId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_isAyrinti_isler1");
            });

            modelBuilder.Entity<Iskesintiler>(entity =>
            {
                entity.ToTable("iskesintiler");

                entity.Property(e => e.IsId).HasColumnName("isId");

                entity.HasOne(d => d.Is)
                    .WithMany(p => p.Iskesintiler)
                    .HasForeignKey(d => d.IsId)
                    .HasConstraintName("FK_iskesintiler_isler");
            });


            modelBuilder.Entity<Arge>(entity =>
            {
                entity.ToTable("arge");

                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.Arge)
                    .HasForeignKey(d => d.FirmaId)
                    .HasConstraintName("FK_arge_firma");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.Arge)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("FK_arge_personel");
            });

            modelBuilder.Entity<ArgePersonel>(entity =>
            {
                entity.ToTable("argepersonel");


                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.ArgePersonel)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("FK_personel_argepersonel");

                entity.HasOne(d => d.Arge)
                    .WithMany(p => p.ArgePersonel)
                    .HasForeignKey(d => d.ArgeId)
                    .HasConstraintName("FK_arge_argepersonel");
            });

            modelBuilder.Entity<Isler>(entity =>
            {
                entity.ToTable("isler");

                entity.Property(e => e.BolumId).HasColumnName("bolumId");

                entity.Property(e => e.BolumOnay)
                    .HasColumnName("bolumOnay")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Dagitim)
                    .HasColumnName("dagitim")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Durum)
                    .HasColumnName("durum")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FaturaNo)
                    .HasColumnName("faturaNo")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FaturaTarihi)
                    .HasColumnName("faturaTarihi")
                    .HasColumnType("date");

                entity.Property(e => e.FaturaTipi).HasColumnName("faturaTipi");

                entity.Property(e => e.FirmaId).HasColumnName("firmaId");

                entity.Property(e => e.HesapId).HasColumnName("hesapId");

                entity.Property(e => e.Kdv).HasColumnName("kdv");

                entity.Property(e => e.Lab)
                    .HasColumnName("lab")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TurId).HasColumnName("turId");

                entity.Property(e => e.Tutar)
                    .HasColumnName("tutar")
                    .HasColumnType("money");

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Isler)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK_isler_bolumler");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.Isler)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("FK_isler_personeller");

                entity.HasOne(d => d.FaturaTipiNavigation)
                    .WithMany(p => p.Isler)
                    .HasForeignKey(d => d.FaturaTipi)
                    .HasConstraintName("FK_isler_faturaTipi");

                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.Isler)
                    .HasForeignKey(d => d.FirmaId)
                    .HasConstraintName("FK_isler_firmalar");

                entity.HasOne(d => d.Hesap)
                    .WithMany(p => p.Isler)
                    .HasForeignKey(d => d.HesapId)
                    .HasConstraintName("FK_isler_hesaplar");

                entity.HasOne(d => d.Tur)
                    .WithMany(p => p.Isler)
                    .HasForeignKey(d => d.TurId)
                    .HasConstraintName("FK_isler_isTurleri");
            });


            modelBuilder.Entity<IsTalepleri>(entity =>
            {
                entity.ToTable("istalepleri");

                entity.Property(e => e.BolumId).HasColumnName("bolumId");

                entity.Property(e => e.BolumOnay)
                    .HasColumnName("bolumOnay")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.FirmaId).HasColumnName("firmaId");

                entity.Property(e => e.TurId).HasColumnName("turId");

                entity.Property(e => e.Tutar)
                    .HasColumnName("tutar")
                    .HasColumnType("money");

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.IsTalepleri)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK_istalepleri_bolumler");

                entity.HasOne(d => d.Personel)
                    .WithMany(p => p.IsTalepleri)
                    .HasForeignKey(d => d.PersonelId)
                    .HasConstraintName("FK_istalepleri_personeller");

                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.IsTalepleri)
                    .HasForeignKey(d => d.FirmaId)
                    .HasConstraintName("FK_istalepleri_firmalar");

                entity.HasOne(d => d.Tur)
                    .WithMany(p => p.IsTalepleri)
                    .HasForeignKey(d => d.TurId)
                    .HasConstraintName("FK_istalepleri_isTurleri");
            });


            modelBuilder.Entity<Istekler>(entity =>
            {
                entity.HasKey(e => e.IstekNo);

                entity.ToTable("istekler");

                entity.Property(e => e.FaturaNo)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.FaturaTarihi).HasColumnType("smalldatetime");

                entity.Property(e => e.Fiyat).HasColumnType("money");

                entity.Property(e => e.Konu)
                    .HasMaxLength(150)
                    .IsUnicode(false);

                entity.Property(e => e.Ozet)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Istekler)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK_istekler_bolumler");

                entity.HasOne(d => d.TurNoNavigation)
                    .WithMany(p => p.Istekler)
                    .HasForeignKey(d => d.TurNo)
                    .HasConstraintName("FK_istekler_istekTurleri");

                entity.HasOne(d => d.Firma)
                    .WithMany(p => p.Istekler)
                    .HasForeignKey(d => d.FirmaId)
                    .HasConstraintName("FK_istekler_firmalar");
            });

            modelBuilder.Entity<IstekTurleri>(entity =>
            {
                entity.HasKey(e => e.TurNo);

                entity.ToTable("istekTurleri");

                entity.Property(e => e.Tur)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<IsTurleri>(entity =>
            {
                entity.ToTable("isTurleri");

                entity.Property(e => e.Tur)
                    .HasColumnName("tur")
                    .HasColumnType("nchar(40)");
            });

            modelBuilder.Entity<KesintiOran>(entity =>
            {
                entity.ToTable("kesintiOran");

                entity.Property(e => e.Oran).HasColumnName("oran");

            });

            modelBuilder.Entity<KesintiOran>()
                .HasIndex(p => new { p.KesintiTipId, p.TurId }).IsUnique();

            modelBuilder.Entity<Ayarlar>()
                .HasIndex(p => new { p.BSBALimiti }).IsUnique();

            modelBuilder.Entity<Kesintitipleri>(entity =>
            {
                entity.ToTable("kesintitipleri");

                entity.Property(e => e.KesintiTipi)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.kesintiTipAyrinti)
                    .WithMany(p => p.Kesintitipleri)
                    .HasForeignKey(d => d.kesintiTipAyrintiId)
                    .HasConstraintName("FK_kesintitipleri_kesintitipayrinti");
            });

            modelBuilder.Entity<Personeller>(entity =>
            {
                entity.ToTable("personeller");

                entity.Property(e => e.Ad)
                    .HasColumnName("ad")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BolumId).HasColumnName("bolumId");

                entity.Property(e => e.Iban)
                    .HasColumnName("iban")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Sifre)
                    .HasColumnName("sifre")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Soyad)
                    .HasColumnName("soyad")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.TcKimlikNo)
                    .HasColumnName("tcKimlikNo")
                    .HasMaxLength(11)
                    .IsUnicode(false);

                entity.Property(e => e.UnvanId).HasColumnName("unvanId");

                entity.Property(e => e.YetkiId).HasColumnName("yetkiId");

                entity.HasOne(d => d.Bolum)
                    .WithMany(p => p.Personeller)
                    .HasForeignKey(d => d.BolumId)
                    .HasConstraintName("FK_personeller_bolumler");

                entity.HasOne(d => d.Unvan)
                    .WithMany(p => p.Personeller)
                    .HasForeignKey(d => d.UnvanId)
                    .HasConstraintName("FK_personeller_unvanlar");

                entity.HasOne(d => d.Yetki)
                    .WithMany(p => p.Personeller)
                    .HasForeignKey(d => d.YetkiId)
                    .HasConstraintName("FK_personeller_yetkiler");
            });

            modelBuilder.Entity<Raporlar>(entity =>
            {
                entity.ToTable("raporlar");

                entity.Property(e => e.Rapor)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Unvanlar>(entity =>
            {
                entity.ToTable("unvanlar");

                entity.Property(e => e.Unvan)
                    .HasColumnName("unvan")
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Yetkiler>(entity =>
            {
                entity.ToTable("yetkiler");

                entity.Property(e => e.Yetki)
                    .HasColumnName("yetki")
                    .HasMaxLength(25)
                    .IsUnicode(false);
            });
        }
    }
}
