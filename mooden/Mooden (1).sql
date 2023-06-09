USE [Mooden]
GO
/****** Object:  Table [dbo].[Ilceler]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ilceler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[sehir_id] [int] NULL,
	[ilce_adi] [nvarchar](150) NULL,
	[aktif_pasif] [int] NULL,
 CONSTRAINT [PK_Ilceler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanicilar]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanicilar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[adi] [nvarchar](150) NULL,
	[soyadi] [nvarchar](150) NULL,
	[kullanici_adi] [nvarchar](50) NULL,
	[sifre] [nvarchar](50) NULL,
	[aktif_pasif] [int] NULL,
 CONSTRAINT [PK_Kullanicilar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Musteriler]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Musteriler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[kullanici_id] [int] NULL,
	[firma_adi] [nvarchar](150) NULL,
	[musteri_adi] [nvarchar](150) NULL,
	[musteri_soyadi] [nvarchar](150) NULL,
	[telefon] [nvarchar](50) NULL,
	[mail] [nvarchar](250) NULL,
	[adres] [nvarchar](250) NULL,
	[sehir_id] [nvarchar](100) NULL,
	[ilce_id] [nvarchar](100) NULL,
	[aktif_pasif] [int] NULL,
 CONSTRAINT [PK_Musteriler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sehirler]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sehirler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[sehir_adi] [nvarchar](150) NULL,
	[aktif_pasif] [int] NULL,
 CONSTRAINT [PK_Sehirler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SiparisAdimlari]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SiparisAdimlari](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[kullanici_id] [int] NULL,
	[siparis_id] [int] NULL,
	[durum_id] [int] NULL,
	[durum] [nvarchar](150) NULL,
	[aciklama] [nvarchar](250) NULL,
	[aktif_pasif] [int] NOT NULL,
 CONSTRAINT [PK_SiparisDurumlari] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Siparisler]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Siparisler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[kullanici_id] [int] NULL,
	[siparis_no] [nvarchar](50) NULL,
	[musteri_id] [int] NULL,
	[urunturu_id] [int] NULL,
	[urun_id] [int] NULL,
	[adet] [int] NULL,
	[fiyat] [decimal](18, 2) NULL,
	[siparis_tarihi] [datetime2](7) NULL,
	[aktif_pasif] [int] NOT NULL,
 CONSTRAINT [PK_Siparisler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Urunler]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Urunler](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[kullanici_id] [int] NULL,
	[urunturu_id] [int] NULL,
	[urun_adi] [nvarchar](150) NULL,
	[adet] [int] NULL,
	[fiyat] [decimal](18, 2) NULL,
	[aktif_pasif] [int] NULL,
 CONSTRAINT [PK_Urunler] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunTuru]    Script Date: 19.11.2022 00:29:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunTuru](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[olusturma_tarihi] [datetime2](7) NULL,
	[guncelleme_tarihi] [datetime2](7) NULL,
	[silme_tarihi] [datetime2](7) NULL,
	[kullanici_id] [int] NULL,
	[tur_adi] [nvarchar](150) NULL,
	[aktif_pasif] [int] NULL,
 CONSTRAINT [PK_UrunTuru] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Ilceler] ADD  CONSTRAINT [DF_Ilceler_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[Ilceler] ADD  CONSTRAINT [DF_Ilceler_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[Kullanicilar] ADD  CONSTRAINT [DF_Kullanicilar_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[Kullanicilar] ADD  CONSTRAINT [DF_Kullanicilar_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[Musteriler] ADD  CONSTRAINT [DF_Musteriler_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[Musteriler] ADD  CONSTRAINT [DF_Musteriler_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[Sehirler] ADD  CONSTRAINT [DF_Sehirler_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[Sehirler] ADD  CONSTRAINT [DF_Sehirler_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[SiparisAdimlari] ADD  CONSTRAINT [DF_SiparisDurumlari_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[SiparisAdimlari] ADD  CONSTRAINT [DF_SiparisDurumlari_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[Siparisler] ADD  CONSTRAINT [DF_Siparisler_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[Siparisler] ADD  CONSTRAINT [DF_Siparisler_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[Urunler] ADD  CONSTRAINT [DF_Urunler_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[Urunler] ADD  CONSTRAINT [DF_Urunler_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
ALTER TABLE [dbo].[UrunTuru] ADD  CONSTRAINT [DF_UrunTuru_olusturma_tarihi]  DEFAULT (getdate()) FOR [olusturma_tarihi]
GO
ALTER TABLE [dbo].[UrunTuru] ADD  CONSTRAINT [DF_UrunTuru_aktif_pasif]  DEFAULT ((1)) FOR [aktif_pasif]
GO
