using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace DonerSermaye.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ayarlar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BSBALimiti = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Dekan = table.Column<string>(nullable: true),
                    Raportor = table.Column<string>(nullable: true),
                    Uye1 = table.Column<string>(nullable: true),
                    Uye2 = table.Column<string>(nullable: true),
                    Uye3 = table.Column<string>(nullable: true),
                    Uye4 = table.Column<string>(nullable: true),
                    Uye5 = table.Column<string>(nullable: true),
                    Uye6 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ayarlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "bolumgelir",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BolumId = table.Column<int>(nullable: true),
                    Dagitim = table.Column<string>(type: "nchar(20)", nullable: true),
                    IsId = table.Column<int>(nullable: true),
                    Tutar = table.Column<decimal>(type: "decimal(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bolumgelir", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ekmekFirma",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Firma = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    KullaniciAdi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Sifre = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    yetkiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ekmekFirma", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EkmekGelir",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Gun = table.Column<DateTime>(nullable: false),
                    Kdv1 = table.Column<decimal>(nullable: false),
                    Kdv18 = table.Column<decimal>(nullable: false),
                    Kdv8 = table.Column<decimal>(nullable: false),
                    KdvToplam = table.Column<decimal>(nullable: false),
                    KdvliToplam = table.Column<decimal>(nullable: false),
                    KdvsizToplam = table.Column<decimal>(nullable: false),
                    Top1 = table.Column<decimal>(nullable: false),
                    Top18 = table.Column<decimal>(nullable: false),
                    Top8 = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EkmekGelir", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ekmekTipleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BirimFiyat = table.Column<double>(nullable: true),
                    EkmekTipi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Kdv = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ekmekTipleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "faturaTipi",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FaturaTipi = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_faturaTipi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "firmalar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    firmaAdi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    tcKimlikNo = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    vergiDairesi = table.Column<string>(maxLength: 25, nullable: true),
                    vergiNo = table.Column<string>(unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_firmalar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "hesaplar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    hesap = table.Column<string>(type: "nchar(30)", nullable: true),
                    KararSayisi = table.Column<int>(nullable: false),
                    ToplantiSayisi = table.Column<int>(nullable: false),
                    ToplantiTarihi = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hesaplar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "istekTurleri",
                columns: table => new
                {
                    TurNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Tur = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_istekTurleri", x => x.TurNo);
                });

            migrationBuilder.CreateTable(
                name: "isTurleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    tur = table.Column<string>(type: "nchar(40)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isTurleri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KesintitipAyrinti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KesintitipAyrinti", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "raporlar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Rapor = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_raporlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "unvanlar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    unvan = table.Column<string>(unicode: false, maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unvanlar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "yetkiler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    yetki = table.Column<string>(unicode: false, maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_yetkiler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ekmekSiparis",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Adet = table.Column<int>(nullable: true),
                    EkmekTipleriId = table.Column<int>(nullable: true),
                    FirmaId = table.Column<int>(nullable: true),
                    SiparisTarihi = table.Column<DateTime>(nullable: true),
                    TeslimTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    ToplamFiyat = table.Column<double>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ekmekSiparis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ekmekSiparis_ekmektipleri",
                        column: x => x.EkmekTipleriId,
                        principalTable: "ekmekTipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ekmekSiparis_personeller",
                        column: x => x.FirmaId,
                        principalTable: "firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "bolumler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bolum = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    KesintitipAyrintiId = table.Column<int>(nullable: false),
                    ilkdevirtutari = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bolumler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_bolumler_KesintitipAyrinti_KesintitipAyrintiId",
                        column: x => x.KesintitipAyrintiId,
                        principalTable: "KesintitipAyrinti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "kesintitipleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KesintiTipi = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    kesintiTipAyrintiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kesintitipleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kesintitipleri_kesintitipayrinti",
                        column: x => x.kesintiTipAyrintiId,
                        principalTable: "KesintitipAyrinti",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "istekler",
                columns: table => new
                {
                    IstekNo = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    BolumId = table.Column<int>(nullable: true),
                    FaturaNo = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    FaturaTarihi = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    FirmaId = table.Column<int>(nullable: true),
                    Fiyat = table.Column<decimal>(type: "money", nullable: true),
                    IstekAd = table.Column<string>(nullable: true),
                    IstekAdet = table.Column<int>(nullable: true),
                    Konu = table.Column<string>(unicode: false, maxLength: 150, nullable: true),
                    Onay = table.Column<int>(nullable: true),
                    OnayNo = table.Column<int>(nullable: true),
                    Ozet = table.Column<string>(unicode: false, maxLength: 250, nullable: true),
                    TahakkukNo = table.Column<string>(nullable: true),
                    TurNo = table.Column<int>(nullable: true),
                    YaklasikMaliyet = table.Column<decimal>(nullable: true),
                    eFaturami = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_istekler", x => x.IstekNo);
                    table.ForeignKey(
                        name: "FK_istekler_bolumler",
                        column: x => x.BolumId,
                        principalTable: "bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istekler_firmalar",
                        column: x => x.FirmaId,
                        principalTable: "firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istekler_istekTurleri",
                        column: x => x.TurNo,
                        principalTable: "istekTurleri",
                        principalColumn: "TurNo",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "personeller",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ad = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    bolumId = table.Column<int>(nullable: true),
                    EPosta = table.Column<string>(nullable: true),
                    iban = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    sifre = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    soyad = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    tcKimlikNo = table.Column<string>(unicode: false, maxLength: 11, nullable: true),
                    unvanId = table.Column<int>(nullable: true),
                    yetkiId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_personeller", x => x.Id);
                    table.ForeignKey(
                        name: "FK_personeller_bolumler",
                        column: x => x.bolumId,
                        principalTable: "bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personeller_unvanlar",
                        column: x => x.unvanId,
                        principalTable: "unvanlar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_personeller_yetkiler",
                        column: x => x.yetkiId,
                        principalTable: "yetkiler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "kesintiOran",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    KesintiTipId = table.Column<int>(nullable: true),
                    oran = table.Column<decimal>(nullable: true),
                    TurId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_kesintiOran", x => x.Id);
                    table.ForeignKey(
                        name: "FK_kesintiOran_kesintitipleri_KesintiTipId",
                        column: x => x.KesintiTipId,
                        principalTable: "kesintitipleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_kesintiOran_isTurleri_TurId",
                        column: x => x.TurId,
                        principalTable: "isTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IstekAyrinti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IstekAd = table.Column<string>(nullable: true),
                    IstekAdet = table.Column<int>(nullable: true),
                    IstekId = table.Column<int>(nullable: false),
                    YaklasikMaliyet = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IstekAyrinti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IstekAyrinti_istekler_IstekId",
                        column: x => x.IstekId,
                        principalTable: "istekler",
                        principalColumn: "IstekNo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "arge",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    ArgeNo = table.Column<string>(nullable: true),
                    Baslama = table.Column<DateTime>(nullable: false),
                    Bitis = table.Column<DateTime>(nullable: false),
                    FirmaId = table.Column<int>(nullable: false),
                    PersonelId = table.Column<int>(nullable: false),
                    ProjeTutari = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_arge", x => x.Id);
                    table.ForeignKey(
                        name: "FK_arge_firma",
                        column: x => x.FirmaId,
                        principalTable: "firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_arge_personel",
                        column: x => x.PersonelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "duyurular",
                columns: table => new
                {
                    Duyuru_No = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Baslik = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    Duyuru = table.Column<string>(type: "text", nullable: true),
                    Gonderen = table.Column<int>(nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_duyurular", x => x.Duyuru_No);
                    table.ForeignKey(
                        name: "FK_duyurular_personeller",
                        column: x => x.Gonderen,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "isler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    bolumId = table.Column<int>(nullable: true),
                    bolumOnay = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    dagitim = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    durum = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    faturaNo = table.Column<string>(unicode: false, maxLength: 50, nullable: true),
                    faturaTarihi = table.Column<DateTime>(type: "date", nullable: true),
                    faturaTipi = table.Column<int>(nullable: true),
                    firmaId = table.Column<int>(nullable: true),
                    hesapId = table.Column<int>(nullable: true),
                    kdv = table.Column<int>(nullable: true),
                    lab = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    PersonelId = table.Column<int>(nullable: true),
                    turId = table.Column<int>(nullable: true),
                    tutar = table.Column<decimal>(type: "money", nullable: true),
                    faturayuklendimi = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_isler_bolumler",
                        column: x => x.bolumId,
                        principalTable: "bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_isler_faturaTipi",
                        column: x => x.faturaTipi,
                        principalTable: "faturaTipi",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_isler_firmalar",
                        column: x => x.firmaId,
                        principalTable: "firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_isler_hesaplar",
                        column: x => x.hesapId,
                        principalTable: "hesaplar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_isler_personeller",
                        column: x => x.PersonelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_isler_isTurleri",
                        column: x => x.turId,
                        principalTable: "isTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "istalepleri",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Aciklama = table.Column<string>(nullable: true),
                    bolumId = table.Column<int>(nullable: true),
                    bolumOnay = table.Column<int>(nullable: true, defaultValueSql: "((0))"),
                    firmaId = table.Column<int>(nullable: true),
                    KararSayisi = table.Column<string>(nullable: true),
                    KararTarihi = table.Column<DateTime>(nullable: false),
                    PersonelId = table.Column<int>(nullable: true),
                    RedSebebi = table.Column<string>(nullable: true),
                    ToplantiSayisi = table.Column<string>(nullable: true),
                    turId = table.Column<int>(nullable: true),
                    tutar = table.Column<decimal>(type: "money", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_istalepleri", x => x.Id);
                    table.ForeignKey(
                        name: "FK_istalepleri_bolumler",
                        column: x => x.bolumId,
                        principalTable: "bolumler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istalepleri_firmalar",
                        column: x => x.firmaId,
                        principalTable: "firmalar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istalepleri_personeller",
                        column: x => x.PersonelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_istalepleri_isTurleri",
                        column: x => x.turId,
                        principalTable: "isTurleri",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "argepersonel",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ArgeId = table.Column<int>(nullable: false),
                    PersonelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_argepersonel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_arge_argepersonel",
                        column: x => x.ArgeId,
                        principalTable: "arge",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_personel_argepersonel",
                        column: x => x.PersonelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "isAyrinti",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    bolumId = table.Column<int>(nullable: true),
                    personelId = table.Column<int>(nullable: true),
                    Tutar = table.Column<decimal>(type: "money", nullable: true),
                    ısId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_isAyrinti", x => x.Id);
                    table.ForeignKey(
                        name: "FK_isAyrinti_personeller",
                        column: x => x.personelId,
                        principalTable: "personeller",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_isAyrinti_isler1",
                        column: x => x.ısId,
                        principalTable: "isler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "iskesintiler",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    isId = table.Column<int>(nullable: true),
                    KesintiOranId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_iskesintiler", x => x.Id);
                    table.ForeignKey(
                        name: "FK_iskesintiler_isler",
                        column: x => x.isId,
                        principalTable: "isler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_arge_FirmaId",
                table: "arge",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_arge_PersonelId",
                table: "arge",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_argepersonel_ArgeId",
                table: "argepersonel",
                column: "ArgeId");

            migrationBuilder.CreateIndex(
                name: "IX_argepersonel_PersonelId",
                table: "argepersonel",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_ayarlar_BSBALimiti",
                table: "ayarlar",
                column: "BSBALimiti",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_bolumler_KesintitipAyrintiId",
                table: "bolumler",
                column: "KesintitipAyrintiId");

            migrationBuilder.CreateIndex(
                name: "IX_duyurular_Gonderen",
                table: "duyurular",
                column: "Gonderen");

            migrationBuilder.CreateIndex(
                name: "IX_ekmekSiparis_EkmekTipleriId",
                table: "ekmekSiparis",
                column: "EkmekTipleriId");

            migrationBuilder.CreateIndex(
                name: "IX_ekmekSiparis_FirmaId",
                table: "ekmekSiparis",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_firmalar_vergiNo",
                table: "firmalar",
                column: "vergiNo",
                unique: true,
                filter: "[vergiNo] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_isAyrinti_personelId",
                table: "isAyrinti",
                column: "personelId");

            migrationBuilder.CreateIndex(
                name: "IX_isAyrinti_ısId",
                table: "isAyrinti",
                column: "ısId");

            migrationBuilder.CreateIndex(
                name: "IX_iskesintiler_isId",
                table: "iskesintiler",
                column: "isId");

            migrationBuilder.CreateIndex(
                name: "IX_isler_bolumId",
                table: "isler",
                column: "bolumId");

            migrationBuilder.CreateIndex(
                name: "IX_isler_faturaTipi",
                table: "isler",
                column: "faturaTipi");

            migrationBuilder.CreateIndex(
                name: "IX_isler_firmaId",
                table: "isler",
                column: "firmaId");

            migrationBuilder.CreateIndex(
                name: "IX_isler_hesapId",
                table: "isler",
                column: "hesapId");

            migrationBuilder.CreateIndex(
                name: "IX_isler_PersonelId",
                table: "isler",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_isler_turId",
                table: "isler",
                column: "turId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_bolumId",
                table: "istalepleri",
                column: "bolumId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_firmaId",
                table: "istalepleri",
                column: "firmaId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_PersonelId",
                table: "istalepleri",
                column: "PersonelId");

            migrationBuilder.CreateIndex(
                name: "IX_istalepleri_turId",
                table: "istalepleri",
                column: "turId");

            migrationBuilder.CreateIndex(
                name: "IX_IstekAyrinti_IstekId",
                table: "IstekAyrinti",
                column: "IstekId");

            migrationBuilder.CreateIndex(
                name: "IX_istekler_BolumId",
                table: "istekler",
                column: "BolumId");

            migrationBuilder.CreateIndex(
                name: "IX_istekler_FirmaId",
                table: "istekler",
                column: "FirmaId");

            migrationBuilder.CreateIndex(
                name: "IX_istekler_TurNo",
                table: "istekler",
                column: "TurNo");

            migrationBuilder.CreateIndex(
                name: "IX_kesintiOran_TurId",
                table: "kesintiOran",
                column: "TurId");

            migrationBuilder.CreateIndex(
                name: "IX_kesintiOran_KesintiTipId_TurId",
                table: "kesintiOran",
                columns: new[] { "KesintiTipId", "TurId" },
                unique: true,
                filter: "[KesintiTipId] IS NOT NULL AND [TurId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_kesintitipleri_kesintiTipAyrintiId",
                table: "kesintitipleri",
                column: "kesintiTipAyrintiId");

            migrationBuilder.CreateIndex(
                name: "IX_personeller_bolumId",
                table: "personeller",
                column: "bolumId");

            migrationBuilder.CreateIndex(
                name: "IX_personeller_unvanId",
                table: "personeller",
                column: "unvanId");

            migrationBuilder.CreateIndex(
                name: "IX_personeller_yetkiId",
                table: "personeller",
                column: "yetkiId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "argepersonel");

            migrationBuilder.DropTable(
                name: "ayarlar");

            migrationBuilder.DropTable(
                name: "bolumgelir");

            migrationBuilder.DropTable(
                name: "duyurular");

            migrationBuilder.DropTable(
                name: "ekmekFirma");

            migrationBuilder.DropTable(
                name: "EkmekGelir");

            migrationBuilder.DropTable(
                name: "ekmekSiparis");

            migrationBuilder.DropTable(
                name: "isAyrinti");

            migrationBuilder.DropTable(
                name: "iskesintiler");

            migrationBuilder.DropTable(
                name: "istalepleri");

            migrationBuilder.DropTable(
                name: "IstekAyrinti");

            migrationBuilder.DropTable(
                name: "kesintiOran");

            migrationBuilder.DropTable(
                name: "raporlar");

            migrationBuilder.DropTable(
                name: "arge");

            migrationBuilder.DropTable(
                name: "ekmekTipleri");

            migrationBuilder.DropTable(
                name: "isler");

            migrationBuilder.DropTable(
                name: "istekler");

            migrationBuilder.DropTable(
                name: "kesintitipleri");

            migrationBuilder.DropTable(
                name: "faturaTipi");

            migrationBuilder.DropTable(
                name: "hesaplar");

            migrationBuilder.DropTable(
                name: "personeller");

            migrationBuilder.DropTable(
                name: "isTurleri");

            migrationBuilder.DropTable(
                name: "firmalar");

            migrationBuilder.DropTable(
                name: "istekTurleri");

            migrationBuilder.DropTable(
                name: "bolumler");

            migrationBuilder.DropTable(
                name: "unvanlar");

            migrationBuilder.DropTable(
                name: "yetkiler");

            migrationBuilder.DropTable(
                name: "KesintitipAyrinti");
        }
    }
}
