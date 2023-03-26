/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2014 (12.0.2269)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Standard Edition
    Target Database Engine Type : Standalone SQL Server
*/
USE [master]
GO
/****** Object:  Database [donersermaye]    Script Date: 04.11.2019 11:38:48 ******/
CREATE DATABASE [donersermaye]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'donersermaye', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\donersermaye.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'donersermaye_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\donersermaye_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [donersermaye] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [donersermaye].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [donersermaye] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [donersermaye] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [donersermaye] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [donersermaye] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [donersermaye] SET ARITHABORT OFF 
GO
ALTER DATABASE [donersermaye] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [donersermaye] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [donersermaye] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [donersermaye] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [donersermaye] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [donersermaye] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [donersermaye] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [donersermaye] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [donersermaye] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [donersermaye] SET  DISABLE_BROKER 
GO
ALTER DATABASE [donersermaye] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [donersermaye] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [donersermaye] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [donersermaye] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [donersermaye] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [donersermaye] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [donersermaye] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [donersermaye] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [donersermaye] SET  MULTI_USER 
GO
ALTER DATABASE [donersermaye] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [donersermaye] SET DB_CHAINING OFF 
GO
ALTER DATABASE [donersermaye] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [donersermaye] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [donersermaye] SET DELAYED_DURABILITY = DISABLED 
GO
USE [donersermaye]
GO
/****** Object:  Table [dbo].[asim]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[asim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Asim] [money] NULL,
 CONSTRAINT [PK_asim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[bolumler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[bolumler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bolum] [varchar](50) NULL,
 CONSTRAINT [PK_bolumler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[dtarihler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dtarihler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[dtar] [varchar](50) NULL,
	[onay] [int] NULL,
	[bolum] [int] NULL,
 CONSTRAINT [PK_dtarihler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[duyurular]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[duyurular](
	[Duyuru_No] [int] IDENTITY(1,1) NOT NULL,
	[Baslik] [varchar](50) NULL,
	[Duyuru] [text] NULL,
	[Tarih] [datetime] NULL,
	[Gonderen] [int] NULL,
 CONSTRAINT [PK_duyurular] PRIMARY KEY CLUSTERED 
(
	[Duyuru_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ekmekFirma]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ekmekFirma](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Firma] [varchar](50) NULL,
	[KullaniciAdi] [varchar](50) NULL,
	[Sifre] [varchar](50) NULL,
	[yetkiId] [int] NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ekmekSiparis]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ekmekSiparis](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[SiparisTarihi] [datetime2](7) NULL,
	[EkmekTipi] [int] NULL,
	[Adet] [int] NULL,
	[ToplamFiyat] [float] NULL,
	[TeslimTarihi] [date] NULL,
	[FirmaId] [int] NULL,
 CONSTRAINT [PK_ekmekSiparis] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ekmekTipleri]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ekmekTipleri](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EkmekTipi] [varchar](50) NULL,
	[BirimFiyat] [float] NULL,
	[Kdv] [float] NULL,
 CONSTRAINT [PK_ekmekTipleri] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[faturaTipi]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[faturaTipi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FaturaTipi] [varchar](50) NULL,
 CONSTRAINT [PK_faturaTipi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[firmalar]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[firmalar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[firmaAdi] [varchar](50) NULL,
	[vergiNo] [varchar](25) NULL,
	[tcKimlikNo] [varchar](11) NULL,
 CONSTRAINT [PK_firmalar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[hesaplar]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[hesaplar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[hesap] [nchar](10) NULL,
 CONSTRAINT [PK_hesaplar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[isAyrinti]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[isAyrinti](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ısId] [int] NOT NULL,
	[personelId] [int] NULL,
	[Tutar] [money] NULL,
	[bolumId] [int] NULL,
	[dtar] [int] NULL,
 CONSTRAINT [PK_isAyrinti] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[iskesintiler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[iskesintiler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[isId] [int] NULL,
	[kesintiId] [int] NULL,
 CONSTRAINT [PK_iskesintiler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[isler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[isler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bolumId] [int] NULL,
	[firmaId] [int] NULL,
	[turId] [int] NULL,
	[tutar] [money] NULL,
	[kdv] [int] NULL,
	[bolumOnay] [int] NULL,
	[faturaTipi] [int] NULL,
	[faturaTarihi] [date] NULL,
	[faturaNo] [varchar](50) NULL,
	[durum] [int] NULL,
	[hesapId] [int] NULL,
	[dagitim] [int] NULL,
	[lab] [int] NULL,
	[gonderen] [int] NULL,
	[dtar] [int] NULL,
 CONSTRAINT [PK_isler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[istekler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[istekler](
	[IstekNo] [int] IDENTITY(1,1) NOT NULL,
	[Konu] [varchar](150) NULL,
	[Ozet] [varchar](250) NULL,
	[TurNo] [int] NULL,
	[OnayNo] [int] NULL,
	[BolumId] [int] NULL,
	[Onay] [int] NULL,
	[FaturaTarihi] [smalldatetime] NULL,
	[FaturaNo] [varchar](50) NULL,
	[Fiyat] [money] NULL,
	[FirmaId] [int] NULL,
 CONSTRAINT [PK_istekler] PRIMARY KEY CLUSTERED 
(
	[IstekNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[istekTurleri]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[istekTurleri](
	[TurNo] [int] IDENTITY(1,1) NOT NULL,
	[Tur] [varchar](50) NULL,
 CONSTRAINT [PK_istekTurleri] PRIMARY KEY CLUSTERED 
(
	[TurNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[isTurleri]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[isTurleri](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[tur] [nchar](40) NULL,
 CONSTRAINT [PK_isTurleri] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kesintiler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kesintiler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Kesinti] [varchar](50) NULL,
	[KesintiTipId] [int] NULL,
	[TurId] [int] NULL,
 CONSTRAINT [PK_kesintiler_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kesintiOran]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kesintiOran](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KesintiId] [int] NOT NULL,
	[oran] [decimal](18, 0) NULL,
 CONSTRAINT [PK_kesintiler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[kesintitipleri]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[kesintitipleri](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KesintiTipi] [varchar](50) NULL,
 CONSTRAINT [PK_kesintitipleri] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[labkalan]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[labkalan](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BolumId] [int] NULL,
	[Tutar] [money] NULL,
	[dtar] [int] NULL,
 CONSTRAINT [PK_bolumgelir] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personeller]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personeller](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[bolumId] [int] NULL,
	[unvanId] [int] NULL,
	[yetkiId] [int] NULL,
	[tcKimlikNo] [varchar](11) NULL,
	[ad] [varchar](50) NULL,
	[soyad] [varchar](50) NULL,
	[iban] [varchar](50) NULL,
	[sifre] [varchar](50) NULL,
	[personelTipi] [int] NULL,
 CONSTRAINT [PK_personeller] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[personelTipi]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[personelTipi](
	[Id] [int] NOT NULL,
	[PersonelTipi] [varchar](50) NULL,
 CONSTRAINT [PK_personelTipi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[raporlar]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[raporlar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Rapor] [varchar](50) NULL,
 CONSTRAINT [PK_raporlar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[unvanlar]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[unvanlar](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[unvan] [varchar](50) NULL,
 CONSTRAINT [PK_unvanlar] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yetkiler]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yetkiler](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[yetki] [varchar](25) NULL,
 CONSTRAINT [PK_yetkiler] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yonetim]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yonetim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Personel] [varchar](50) NULL,
	[GorevId] [int] NULL,
 CONSTRAINT [PK_yonetim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yonetimGorev]    Script Date: 04.11.2019 11:38:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yonetimGorev](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Gorev] [varchar](50) NULL,
 CONSTRAINT [PK_yonetimGorev] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[asim] ON 

INSERT [dbo].[asim] ([Id], [Asim]) VALUES (1, 4500.0000)
SET IDENTITY_INSERT [dbo].[asim] OFF
SET IDENTITY_INSERT [dbo].[bolumler] ON 

INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (1, N'BİLGİSAYAR MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (2, N'MAKİNE MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (6, N'BİYOMÜHENDİSLİK')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (7, N'DERİ MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (8, N'ELEKTRİK-ELEKTRONİK MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (9, N'GIDA MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (10, N'İNŞAAT MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (11, N'KİMYA MÜHENDİSLİĞİ')
INSERT [dbo].[bolumler] ([Id], [bolum]) VALUES (12, N'TEKSTİL MÜHENDİSLİĞİ')
SET IDENTITY_INSERT [dbo].[bolumler] OFF
SET IDENTITY_INSERT [dbo].[duyurular] ON 

INSERT [dbo].[duyurular] ([Duyuru_No], [Baslik], [Duyuru], [Tarih], [Gonderen]) VALUES (1, N'Habaş Ödemesi', N'Habaş ile yapılan analiz işlemine ait ödeme 1 Haziran itibarıyla yapılmıştır.', CAST(N'2016-06-01T00:00:00.000' AS DateTime), 12)
INSERT [dbo].[duyurular] ([Duyuru_No], [Baslik], [Duyuru], [Tarih], [Gonderen]) VALUES (3, N'wwwww', N'ewewewewewe', CAST(N'2018-06-01T00:00:00.000' AS DateTime), 12)
INSERT [dbo].[duyurular] ([Duyuru_No], [Baslik], [Duyuru], [Tarih], [Gonderen]) VALUES (4, N'pppppppppppp', N'klklkkkkkkkkkklkl', CAST(N'2018-06-01T15:24:33.720' AS DateTime), 12)
INSERT [dbo].[duyurular] ([Duyuru_No], [Baslik], [Duyuru], [Tarih], [Gonderen]) VALUES (6, N'Ekim ayı ödemesi', N'ödemeler yapıldı', CAST(N'2018-10-22T13:56:38.447' AS DateTime), 12)
SET IDENTITY_INSERT [dbo].[duyurular] OFF
SET IDENTITY_INSERT [dbo].[ekmekSiparis] ON 

INSERT [dbo].[ekmekSiparis] ([Id], [SiparisTarihi], [EkmekTipi], [Adet], [ToplamFiyat], [TeslimTarihi], [FirmaId]) VALUES (19, CAST(N'2018-12-03T13:52:28.2661280' AS DateTime2), 1, 28310, 25479, CAST(N'2018-12-04' AS Date), 21)
INSERT [dbo].[ekmekSiparis] ([Id], [SiparisTarihi], [EkmekTipi], [Adet], [ToplamFiyat], [TeslimTarihi], [FirmaId]) VALUES (20, CAST(N'2018-12-03T13:52:32.4303662' AS DateTime2), 3, 56800, 23856, CAST(N'2018-12-04' AS Date), 21)
INSERT [dbo].[ekmekSiparis] ([Id], [SiparisTarihi], [EkmekTipi], [Adet], [ToplamFiyat], [TeslimTarihi], [FirmaId]) VALUES (21, CAST(N'2018-12-03T13:52:34.1764661' AS DateTime2), 4, 51000, 17340, CAST(N'2018-12-04' AS Date), 21)
INSERT [dbo].[ekmekSiparis] ([Id], [SiparisTarihi], [EkmekTipi], [Adet], [ToplamFiyat], [TeslimTarihi], [FirmaId]) VALUES (22, CAST(N'2018-12-03T15:02:15.6306316' AS DateTime2), 1, 500, 450, CAST(N'2018-12-10' AS Date), 21)
INSERT [dbo].[ekmekSiparis] ([Id], [SiparisTarihi], [EkmekTipi], [Adet], [ToplamFiyat], [TeslimTarihi], [FirmaId]) VALUES (23, CAST(N'2018-12-24T15:00:34.5076173' AS DateTime2), 1, 1000, 900, CAST(N'2018-12-03' AS Date), 21)
INSERT [dbo].[ekmekSiparis] ([Id], [SiparisTarihi], [EkmekTipi], [Adet], [ToplamFiyat], [TeslimTarihi], [FirmaId]) VALUES (24, CAST(N'2018-12-24T15:00:34.8196179' AS DateTime2), 3, 200, 84, CAST(N'2018-12-03' AS Date), 21)
SET IDENTITY_INSERT [dbo].[ekmekSiparis] OFF
SET IDENTITY_INSERT [dbo].[ekmekTipleri] ON 

INSERT [dbo].[ekmekTipleri] ([Id], [EkmekTipi], [BirimFiyat], [Kdv]) VALUES (1, N'Normal Ekmek (300 gr)', 0.9, 2)
INSERT [dbo].[ekmekTipleri] ([Id], [EkmekTipi], [BirimFiyat], [Kdv]) VALUES (2, N'Kepekli Ekmek (300 gr)', 0.9, 2)
INSERT [dbo].[ekmekTipleri] ([Id], [EkmekTipi], [BirimFiyat], [Kdv]) VALUES (3, N'Sandviç Ekmeği (75 gr)', 0.42, 2)
INSERT [dbo].[ekmekTipleri] ([Id], [EkmekTipi], [BirimFiyat], [Kdv]) VALUES (4, N'Tuzsuz Diyet Ekmeği(50 gr)', 0.34, 2)
SET IDENTITY_INSERT [dbo].[ekmekTipleri] OFF
SET IDENTITY_INSERT [dbo].[faturaTipi] ON 

INSERT [dbo].[faturaTipi] ([Id], [FaturaTipi]) VALUES (1, N'Peşin Fatura')
INSERT [dbo].[faturaTipi] ([Id], [FaturaTipi]) VALUES (2, N'Borçlar Faturası')
SET IDENTITY_INSERT [dbo].[faturaTipi] OFF
SET IDENTITY_INSERT [dbo].[firmalar] ON 

INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (2, N'İzmir Demir Çelik3', N'343434477', N'35664564565')
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (3, N'Ege Metal', N'123456789', N'12121343434')
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (4, N'Habaş', N'44561374665', N'13794461235')
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (5, N'Dirinler Makine', N'1524695874', NULL)
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (6, N'Vestel AŞ', N'1254785695', N'12354895678')
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (7, N'Bosch AŞ', N'1212121212', NULL)
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (8, N'Çınar Mayacılık', N'2510084365', NULL)
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (10, N'İğne İplik A.Ş', N'1212122222', NULL)
INSERT [dbo].[firmalar] ([Id], [firmaAdi], [vergiNo], [tcKimlikNo]) VALUES (11, N'KMSBilişim', N'78598652', N'12547856984')
SET IDENTITY_INSERT [dbo].[firmalar] OFF
SET IDENTITY_INSERT [dbo].[hesaplar] ON 

INSERT [dbo].[hesaplar] ([Id], [hesap]) VALUES (1049, N'ŞUBAT-2019')
INSERT [dbo].[hesaplar] ([Id], [hesap]) VALUES (1050, N'ssssss    ')
SET IDENTITY_INSERT [dbo].[hesaplar] OFF
SET IDENTITY_INSERT [dbo].[isAyrinti] ON 

INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4175, 3241, 2, 800.0000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4176, 3241, 22, 96.0000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4177, 3242, 2, 500.0000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4178, 3242, 12, 420.0000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4179, 3242, 14, 1000.0000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4180, 3242, 22, 70.9100, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4181, 3243, 2, 1257.6000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4182, 3244, 15, 1572.0000, NULL, NULL)
INSERT [dbo].[isAyrinti] ([Id], [ısId], [personelId], [Tutar], [bolumId], [dtar]) VALUES (4183, 3245, 2, 3930.0000, NULL, NULL)
SET IDENTITY_INSERT [dbo].[isAyrinti] OFF
SET IDENTITY_INSERT [dbo].[iskesintiler] ON 

INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3711, 3241, 1007)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3712, 3241, 1008)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3713, 3242, 1007)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3714, 3242, 1008)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3715, 3243, 1)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3716, 3243, 2)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3717, 3243, 3)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3718, 3243, 4)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3719, 3243, 5)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3720, 3244, 1)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3721, 3244, 2)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3722, 3244, 3)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3723, 3244, 4)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3724, 3244, 5)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3725, 3245, 1)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3726, 3245, 2)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3727, 3245, 3)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3728, 3245, 4)
INSERT [dbo].[iskesintiler] ([Id], [isId], [kesintiId]) VALUES (3729, 3245, 5)
SET IDENTITY_INSERT [dbo].[iskesintiler] OFF
SET IDENTITY_INSERT [dbo].[isler] ON 

INSERT [dbo].[isler] ([Id], [bolumId], [firmaId], [turId], [tutar], [kdv], [bolumOnay], [faturaTipi], [faturaTarihi], [faturaNo], [durum], [hesapId], [dagitim], [lab], [gonderen], [dtar]) VALUES (3241, 1, 2, 1, 1000.0000, 18, 1, 1, CAST(N'2019-04-13' AS Date), N'2323234', 1, 1049, 1, 0, 13, NULL)
INSERT [dbo].[isler] ([Id], [bolumId], [firmaId], [turId], [tutar], [kdv], [bolumOnay], [faturaTipi], [faturaTarihi], [faturaNo], [durum], [hesapId], [dagitim], [lab], [gonderen], [dtar]) VALUES (3242, 1, 4, 1, 2222.0000, 18, 1, 1, CAST(N'2019-04-17' AS Date), N'458956', 1, 1050, 1, 0, 13, NULL)
INSERT [dbo].[isler] ([Id], [bolumId], [firmaId], [turId], [tutar], [kdv], [bolumOnay], [faturaTipi], [faturaTarihi], [faturaNo], [durum], [hesapId], [dagitim], [lab], [gonderen], [dtar]) VALUES (3243, 1, 4, 4, 1600.0000, 18, 1, 1, CAST(N'2019-04-11' AS Date), N'343234', 1, NULL, NULL, 0, 13, NULL)
INSERT [dbo].[isler] ([Id], [bolumId], [firmaId], [turId], [tutar], [kdv], [bolumOnay], [faturaTipi], [faturaTarihi], [faturaNo], [durum], [hesapId], [dagitim], [lab], [gonderen], [dtar]) VALUES (3244, 2, 5, 4, 2000.8000, 18, 1, 1, CAST(N'2019-04-19' AS Date), N'0988987', 1, NULL, NULL, 0, 13, NULL)
INSERT [dbo].[isler] ([Id], [bolumId], [firmaId], [turId], [tutar], [kdv], [bolumOnay], [faturaTipi], [faturaTarihi], [faturaNo], [durum], [hesapId], [dagitim], [lab], [gonderen], [dtar]) VALUES (3245, 1, 2, 4, 5000.0000, 18, 1, 1, CAST(N'2019-04-19' AS Date), N'4758745', 1, NULL, NULL, 0, 13, NULL)
SET IDENTITY_INSERT [dbo].[isler] OFF
SET IDENTITY_INSERT [dbo].[istekler] ON 

INSERT [dbo].[istekler] ([IstekNo], [Konu], [Ozet], [TurNo], [OnayNo], [BolumId], [Onay], [FaturaTarihi], [FaturaNo], [Fiyat], [FirmaId]) VALUES (3015, N'dekan harcama', N'tadilat çalışmaları', 1, NULL, NULL, NULL, CAST(N'2019-12-13T00:00:00' AS SmallDateTime), N'45785698', 340.0000, 11)
SET IDENTITY_INSERT [dbo].[istekler] OFF
SET IDENTITY_INSERT [dbo].[istekTurleri] ON 

INSERT [dbo].[istekTurleri] ([TurNo], [Tur]) VALUES (1, N'Malzeme Alımı')
INSERT [dbo].[istekTurleri] ([TurNo], [Tur]) VALUES (2, N'Hizmet Alımı')
SET IDENTITY_INSERT [dbo].[istekTurleri] OFF
SET IDENTITY_INSERT [dbo].[isTurleri] ON 

INSERT [dbo].[isTurleri] ([Id], [tur]) VALUES (1, N'Analiz                                  ')
INSERT [dbo].[isTurleri] ([Id], [tur]) VALUES (2, N'Rapor                                   ')
INSERT [dbo].[isTurleri] ([Id], [tur]) VALUES (4, N'Danışmanlık                             ')
INSERT [dbo].[isTurleri] ([Id], [tur]) VALUES (5, N'Eğitim                                  ')
INSERT [dbo].[isTurleri] ([Id], [tur]) VALUES (6, N'Arge Geliri                             ')
INSERT [dbo].[isTurleri] ([Id], [tur]) VALUES (7, N'deneme                                  ')
SET IDENTITY_INSERT [dbo].[isTurleri] OFF
SET IDENTITY_INSERT [dbo].[kesintiler] ON 

INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (1, N'Merkez Döner Sermaye Payı (%0,4)', 1, 4)
INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (2, N'Peşin Gelir Vergisi(Maliye Payı %1)', 4, 4)
INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (3, N'Bilimsel Projeler Araş. Payı(%5)', 4, 4)
INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (4, N'Bölüm Malzeme Payı(%12)', 3, 4)
INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (5, N'Dekanlık Payı(%3)', 2, 4)
INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (1007, N'Merkez Döner Sermaye Payı (%0,4)', 1, 1)
INSERT [dbo].[kesintiler] ([Id], [Kesinti], [KesintiTipId], [TurId]) VALUES (1008, N'Bölüm Payı %12', 3, 1)
SET IDENTITY_INSERT [dbo].[kesintiler] OFF
SET IDENTITY_INSERT [dbo].[kesintiOran] ON 

INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (1, 1, CAST(0 AS Decimal(18, 0)))
INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (5, 2, CAST(1 AS Decimal(18, 0)))
INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (6, 3, CAST(5 AS Decimal(18, 0)))
INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (7, 4, CAST(12 AS Decimal(18, 0)))
INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (8, 5, CAST(3 AS Decimal(18, 0)))
INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (1011, 1008, CAST(10 AS Decimal(18, 0)))
INSERT [dbo].[kesintiOran] ([Id], [KesintiId], [oran]) VALUES (1012, 1007, CAST(0 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[kesintiOran] OFF
SET IDENTITY_INSERT [dbo].[kesintitipleri] ON 

INSERT [dbo].[kesintitipleri] ([Id], [KesintiTipi]) VALUES (1, N'')
INSERT [dbo].[kesintitipleri] ([Id], [KesintiTipi]) VALUES (2, N'Dekanlık Payı')
INSERT [dbo].[kesintitipleri] ([Id], [KesintiTipi]) VALUES (3, N'Bölüm Payı')
INSERT [dbo].[kesintitipleri] ([Id], [KesintiTipi]) VALUES (4, N'Genel Kesinti')
SET IDENTITY_INSERT [dbo].[kesintitipleri] OFF
SET IDENTITY_INSERT [dbo].[personeller] ON 

INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (2, 1, 1, 2, N'13799467602', N'Osman', N'GÜNGÖR', N'TR23454345453345345345335472388', N'1234', 1)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (12, 1, 6, 3, N'123123123', N'Merve', N'GÜNGÖR', N'TR3432423445454545454545478', N'123456', 1)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (13, 1, 1, 1, N'11111111111', N'Ahmet', N'Ayhan', N'TR5625625624555457812547854', N'11111', 1)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (14, 1, 5, 2, N'47866334528', N'Adnan', N'BAŞTÜRK', N'TR2345434545334534534533512', N'000000', 1)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (15, 2, 2, 2, N'12345678910', N'Fatma', N'AVCI GEÇGEL', N'TR1245214523652145784587', N'12345', 1)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (16, 2, 1, 1, N'10987654321', N'Ali', N'GÜL', N'TR2356897485963256987452', N'12345', 1)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (21, NULL, NULL, 4, N'90909090', N'Ege Üniversitesi', N'Hastanesi', NULL, N'90909090', NULL)
INSERT [dbo].[personeller] ([Id], [bolumId], [unvanId], [yetkiId], [tcKimlikNo], [ad], [soyad], [iban], [sifre], [personelTipi]) VALUES (22, 1, NULL, NULL, NULL, N'Bölüm Malzeme ', N'Payı', NULL, NULL, 2)
SET IDENTITY_INSERT [dbo].[personeller] OFF
INSERT [dbo].[personelTipi] ([Id], [PersonelTipi]) VALUES (1, N'Personel')
INSERT [dbo].[personelTipi] ([Id], [PersonelTipi]) VALUES (2, N'Bölüm')
SET IDENTITY_INSERT [dbo].[raporlar] ON 

INSERT [dbo].[raporlar] ([Id], [Rapor]) VALUES (1, N'Giden Evrak')
INSERT [dbo].[raporlar] ([Id], [Rapor]) VALUES (2, N'Giden Evrak ( Arge)')
INSERT [dbo].[raporlar] ([Id], [Rapor]) VALUES (3, N'Kalan Evrak')
INSERT [dbo].[raporlar] ([Id], [Rapor]) VALUES (4, N'Kalan Evrak ( Arge )')
SET IDENTITY_INSERT [dbo].[raporlar] OFF
SET IDENTITY_INSERT [dbo].[unvanlar] ON 

INSERT [dbo].[unvanlar] ([Id], [unvan]) VALUES (1, N'Profesör')
INSERT [dbo].[unvanlar] ([Id], [unvan]) VALUES (2, N'Doçent')
INSERT [dbo].[unvanlar] ([Id], [unvan]) VALUES (5, N'Öğretim Elemanı')
INSERT [dbo].[unvanlar] ([Id], [unvan]) VALUES (6, N'Döner Sermaye Personeli')
INSERT [dbo].[unvanlar] ([Id], [unvan]) VALUES (7, N'Araştırma Görevlisi')
INSERT [dbo].[unvanlar] ([Id], [unvan]) VALUES (8, N'Doktor Öğretim Üyesi')
SET IDENTITY_INSERT [dbo].[unvanlar] OFF
SET IDENTITY_INSERT [dbo].[yetkiler] ON 

INSERT [dbo].[yetkiler] ([Id], [yetki]) VALUES (1, N'Bölüm Başkanı')
INSERT [dbo].[yetkiler] ([Id], [yetki]) VALUES (2, N'Öğretim Görevlisi')
INSERT [dbo].[yetkiler] ([Id], [yetki]) VALUES (3, N'Döner Sermaye Personeli')
INSERT [dbo].[yetkiler] ([Id], [yetki]) VALUES (4, N'Ekmek Firma')
SET IDENTITY_INSERT [dbo].[yetkiler] OFF
SET IDENTITY_INSERT [dbo].[yonetim] ON 

INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (1, N'Prof. Dr. Hasan YILDIZ', 1)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (2, N'Prof. Dr. Arzu MARMARALI', 2)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (3, N'Prof. Dr. Şerife Şeref HELVACI', 2)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (4, N'Prof. Dr. Meltem SERDAROĞLU', 2)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (5, N'Prof. Dr. Gökhan ZENGİN', 2)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (6, N'Prof. Dr. Hasan BULUT', 2)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (7, N'Dr. Öğr. Üyesi E. Zeki ENGİN', 2)
INSERT [dbo].[yonetim] ([Id], [Personel], [GorevId]) VALUES (8, N'Mehtap GÜRBÜZ', 3)
SET IDENTITY_INSERT [dbo].[yonetim] OFF
SET IDENTITY_INSERT [dbo].[yonetimGorev] ON 

INSERT [dbo].[yonetimGorev] ([Id], [Gorev]) VALUES (1, N'Dekan')
INSERT [dbo].[yonetimGorev] ([Id], [Gorev]) VALUES (2, N'Üye')
INSERT [dbo].[yonetimGorev] ([Id], [Gorev]) VALUES (3, N'Fakülte Sekreteri')
SET IDENTITY_INSERT [dbo].[yonetimGorev] OFF
/****** Object:  Index [IX_kesintiOran]    Script Date: 04.11.2019 11:38:49 ******/
ALTER TABLE [dbo].[kesintiOran] ADD  CONSTRAINT [IX_kesintiOran] UNIQUE NONCLUSTERED 
(
	[KesintiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[isler] ADD  CONSTRAINT [DF_isler_bolumOnay]  DEFAULT ((0)) FOR [bolumOnay]
GO
ALTER TABLE [dbo].[isler] ADD  CONSTRAINT [DF_isler_durum]  DEFAULT ((0)) FOR [durum]
GO
ALTER TABLE [dbo].[isler] ADD  CONSTRAINT [DF_isler_dagitim]  DEFAULT ((0)) FOR [dagitim]
GO
ALTER TABLE [dbo].[isler] ADD  CONSTRAINT [DF_isler_lab]  DEFAULT ((0)) FOR [lab]
GO
ALTER TABLE [dbo].[duyurular]  WITH CHECK ADD  CONSTRAINT [FK_duyurular_personeller] FOREIGN KEY([Gonderen])
REFERENCES [dbo].[personeller] ([Id])
GO
ALTER TABLE [dbo].[duyurular] CHECK CONSTRAINT [FK_duyurular_personeller]
GO
ALTER TABLE [dbo].[ekmekSiparis]  WITH CHECK ADD  CONSTRAINT [FK_ekmekSiparis_ekmekTipleri] FOREIGN KEY([EkmekTipi])
REFERENCES [dbo].[ekmekTipleri] ([Id])
GO
ALTER TABLE [dbo].[ekmekSiparis] CHECK CONSTRAINT [FK_ekmekSiparis_ekmekTipleri]
GO
ALTER TABLE [dbo].[ekmekSiparis]  WITH CHECK ADD  CONSTRAINT [FK_ekmekSiparis_personeller] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[personeller] ([Id])
GO
ALTER TABLE [dbo].[ekmekSiparis] CHECK CONSTRAINT [FK_ekmekSiparis_personeller]
GO
ALTER TABLE [dbo].[isAyrinti]  WITH CHECK ADD  CONSTRAINT [FK_isAyrinti_dtarihler] FOREIGN KEY([dtar])
REFERENCES [dbo].[dtarihler] ([Id])
GO
ALTER TABLE [dbo].[isAyrinti] CHECK CONSTRAINT [FK_isAyrinti_dtarihler]
GO
ALTER TABLE [dbo].[isAyrinti]  WITH CHECK ADD  CONSTRAINT [FK_isAyrinti_personeller] FOREIGN KEY([personelId])
REFERENCES [dbo].[personeller] ([Id])
GO
ALTER TABLE [dbo].[isAyrinti] CHECK CONSTRAINT [FK_isAyrinti_personeller]
GO
ALTER TABLE [dbo].[iskesintiler]  WITH CHECK ADD  CONSTRAINT [FK_iskesintiler_isler] FOREIGN KEY([isId])
REFERENCES [dbo].[isler] ([Id])
GO
ALTER TABLE [dbo].[iskesintiler] CHECK CONSTRAINT [FK_iskesintiler_isler]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_bolumler] FOREIGN KEY([bolumId])
REFERENCES [dbo].[bolumler] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_bolumler]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_dtarihler] FOREIGN KEY([dtar])
REFERENCES [dbo].[dtarihler] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_dtarihler]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_faturaTipi] FOREIGN KEY([faturaTipi])
REFERENCES [dbo].[faturaTipi] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_faturaTipi]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_firmalar] FOREIGN KEY([firmaId])
REFERENCES [dbo].[firmalar] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_firmalar]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_hesaplar] FOREIGN KEY([hesapId])
REFERENCES [dbo].[hesaplar] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_hesaplar]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_isTurleri] FOREIGN KEY([turId])
REFERENCES [dbo].[isTurleri] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_isTurleri]
GO
ALTER TABLE [dbo].[isler]  WITH CHECK ADD  CONSTRAINT [FK_isler_personeller] FOREIGN KEY([gonderen])
REFERENCES [dbo].[personeller] ([Id])
GO
ALTER TABLE [dbo].[isler] CHECK CONSTRAINT [FK_isler_personeller]
GO
ALTER TABLE [dbo].[istekler]  WITH CHECK ADD  CONSTRAINT [FK_istekler_bolumler] FOREIGN KEY([BolumId])
REFERENCES [dbo].[bolumler] ([Id])
GO
ALTER TABLE [dbo].[istekler] CHECK CONSTRAINT [FK_istekler_bolumler]
GO
ALTER TABLE [dbo].[istekler]  WITH CHECK ADD  CONSTRAINT [FK_istekler_firmalar] FOREIGN KEY([FirmaId])
REFERENCES [dbo].[firmalar] ([Id])
GO
ALTER TABLE [dbo].[istekler] CHECK CONSTRAINT [FK_istekler_firmalar]
GO
ALTER TABLE [dbo].[istekler]  WITH CHECK ADD  CONSTRAINT [FK_istekler_istekTurleri] FOREIGN KEY([TurNo])
REFERENCES [dbo].[istekTurleri] ([TurNo])
GO
ALTER TABLE [dbo].[istekler] CHECK CONSTRAINT [FK_istekler_istekTurleri]
GO
ALTER TABLE [dbo].[kesintiler]  WITH CHECK ADD  CONSTRAINT [FK_kesintiler_isTurleri] FOREIGN KEY([TurId])
REFERENCES [dbo].[isTurleri] ([Id])
GO
ALTER TABLE [dbo].[kesintiler] CHECK CONSTRAINT [FK_kesintiler_isTurleri]
GO
ALTER TABLE [dbo].[kesintiler]  WITH CHECK ADD  CONSTRAINT [FK_kesintiler_kesintitipleri] FOREIGN KEY([KesintiTipId])
REFERENCES [dbo].[kesintitipleri] ([Id])
GO
ALTER TABLE [dbo].[kesintiler] CHECK CONSTRAINT [FK_kesintiler_kesintitipleri]
GO
ALTER TABLE [dbo].[kesintiOran]  WITH CHECK ADD  CONSTRAINT [FK_kesintiOran_kesintiler] FOREIGN KEY([KesintiId])
REFERENCES [dbo].[kesintiler] ([Id])
GO
ALTER TABLE [dbo].[kesintiOran] CHECK CONSTRAINT [FK_kesintiOran_kesintiler]
GO
ALTER TABLE [dbo].[labkalan]  WITH CHECK ADD  CONSTRAINT [FK_labkalan_dtarihler] FOREIGN KEY([dtar])
REFERENCES [dbo].[dtarihler] ([Id])
GO
ALTER TABLE [dbo].[labkalan] CHECK CONSTRAINT [FK_labkalan_dtarihler]
GO
ALTER TABLE [dbo].[personeller]  WITH CHECK ADD  CONSTRAINT [FK_personeller_bolumler] FOREIGN KEY([bolumId])
REFERENCES [dbo].[bolumler] ([Id])
GO
ALTER TABLE [dbo].[personeller] CHECK CONSTRAINT [FK_personeller_bolumler]
GO
ALTER TABLE [dbo].[personeller]  WITH CHECK ADD  CONSTRAINT [FK_personeller_personelTipi] FOREIGN KEY([personelTipi])
REFERENCES [dbo].[personelTipi] ([Id])
GO
ALTER TABLE [dbo].[personeller] CHECK CONSTRAINT [FK_personeller_personelTipi]
GO
ALTER TABLE [dbo].[personeller]  WITH CHECK ADD  CONSTRAINT [FK_personeller_unvanlar] FOREIGN KEY([unvanId])
REFERENCES [dbo].[unvanlar] ([Id])
GO
ALTER TABLE [dbo].[personeller] CHECK CONSTRAINT [FK_personeller_unvanlar]
GO
ALTER TABLE [dbo].[personeller]  WITH CHECK ADD  CONSTRAINT [FK_personeller_yetkiler] FOREIGN KEY([yetkiId])
REFERENCES [dbo].[yetkiler] ([Id])
GO
ALTER TABLE [dbo].[personeller] CHECK CONSTRAINT [FK_personeller_yetkiler]
GO
ALTER TABLE [dbo].[yonetim]  WITH CHECK ADD  CONSTRAINT [FK_yonetim_yonetimGorev] FOREIGN KEY([GorevId])
REFERENCES [dbo].[yonetimGorev] ([Id])
GO
ALTER TABLE [dbo].[yonetim] CHECK CONSTRAINT [FK_yonetim_yonetimGorev]
GO
USE [master]
GO
ALTER DATABASE [donersermaye] SET  READ_WRITE 
GO
