USE [MainLibrary]
GO

/****** Object:  Table [dbo].[BookSeries]    Script Date: 9/18/2023 6:15:50 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE type [dbo].[BookSeries]as table(
	[ctrl_no] [bigint] NOT NULL,
	[SeriesName] [nvarchar](100) NULL,
	[seriesNo] [nvarchar](50) NULL,
	[seriesPart] [nvarchar](50) NULL,
	[etal] [nvarchar](1) NULL,
	[Svolume] [int] NULL,
	[af1] [nvarchar](50) NULL,
	[am1] [nvarchar](50) NULL,
	[al1] [nvarchar](50) NULL,
	[af2] [nvarchar](50) NULL,
	[am2] [nvarchar](50) NULL,
	[al2] [nvarchar](50) NULL,
	[af3] [nvarchar](50) NULL,
	[am3] [nvarchar](50) NULL,
	[al3] [nvarchar](50) NULL,
	[SSeriesName] [nvarchar](100) NULL,
	[SseriesNo] [nvarchar](50) NULL,
	[SseriesPart] [nvarchar](50) NULL,
	[Setal] [nvarchar](1) NULL,
	[SSvolume] [int] NULL,
	[Saf1] [nvarchar](50) NULL,
	[sam1] [nvarchar](50) NULL,
	[Sal1] [nvarchar](50) NULL,
	[Saf2] [nvarchar](50) NULL,
	[Sam2] [nvarchar](50) NULL,
	[Sal2] [nvarchar](50) NULL,
	[Saf3] [nvarchar](50) NULL,
	[Sam3] [nvarchar](50) NULL,
	[Sal3] [nvarchar](50) NULL,
	[SeriesParallelTitle] [nvarchar](100) NULL,
	[SSeriesParallelTitle] [nvarchar](100) NULL,
	[SubSeriesName] [nvarchar](100) NULL,
	[SubseriesNo] [nvarchar](50) NULL,
	[SubSeriesPart] [nvarchar](50) NULL,
	[SubEtal] [nvarchar](1) NULL,
	[SubSvolume] [int] NULL,
	[Subaf1] [nvarchar](50) NULL,
	[Subam1] [nvarchar](50) NULL,
	[Subal1] [nvarchar](50) NULL,
	[Subaf2] [nvarchar](50) NULL,
	[Subam2] [nvarchar](50) NULL,
	[Subal2] [nvarchar](50) NULL,
	[Subaf3] [nvarchar](50) NULL,
	[Subam3] [nvarchar](50) NULL,
	[Subal3] [nvarchar](50) NULL,
	[SubSeriesParallelTitle] [nvarchar](100) NULL,
	[ISSNMain] [nvarchar](50) NULL,
	[ISSNSub] [nvarchar](50) NULL,
	[ISSNSecond] [nvarchar](50) NULL
)
