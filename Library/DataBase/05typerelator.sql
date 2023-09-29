USE [MainLibrary]
GO

/****** Object:  Table [dbo].[BookRelators]    Script Date: 9/18/2023 6:14:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE type [dbo].[BookRelators]as table(
	[ctrl_no] [bigint] NOT NULL,
	[editorFname1] [nvarchar](50) NULL,
	[editorMname1] [nvarchar](50) NULL,
	[editorLname1] [nvarchar](50) NULL,
	[editorFname2] [nvarchar](50) NULL,
	[editorMname2] [nvarchar](50) NULL,
	[editorLname2] [nvarchar](50) NULL,
	[editorFname3] [nvarchar](50) NULL,
	[editorMname3] [nvarchar](50) NULL,
	[editorLname3] [nvarchar](50) NULL,
	[CompilerFname1] [nvarchar](50) NULL,
	[CompilerMname1] [nvarchar](50) NULL,
	[CompilerLname1] [nvarchar](50) NULL,
	[CompilerFname2] [nvarchar](50) NULL,
	[CompilerMname2] [nvarchar](50) NULL,
	[CompilerLname2] [nvarchar](50) NULL,
	[CompilerFname3] [nvarchar](50) NULL,
	[CompilerMname3] [nvarchar](50) NULL,
	[CompilerLname3] [nvarchar](50) NULL,
	[illusFname1] [nvarchar](50) NULL,
	[illusMname1] [nvarchar](50) NULL,
	[illusLname1] [nvarchar](50) NULL,
	[illusFname2] [nvarchar](50) NULL,
	[illusMname2] [nvarchar](50) NULL,
	[illusrLname2] [nvarchar](50) NULL,
	[illusFname3] [nvarchar](50) NULL,
	[illusMname3] [nvarchar](50) NULL,
	[illusLname3] [nvarchar](50) NULL,
	[TranslatorFname1] [nvarchar](50) NULL,
	[TranslatorMname11] [nvarchar](50) NULL,
	[TranslatorLname1] [nvarchar](50) NULL,
	[TranslatorFname2] [nvarchar](50) NULL,
	[TranslatorMname2] [nvarchar](50) NULL,
	[TranslatorLname2] [nvarchar](50) NULL,
	[TranslatorFname3] [nvarchar](50) NULL,
	[TranslatorMname3] [nvarchar](50) NULL,
	[TranslatorLname3] [nvarchar](50) NULL
)
