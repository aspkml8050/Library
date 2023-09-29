
CREATE type [dbo].[CatalogData]as table(
	[ctrl_no] [bigint] NOT NULL,
	[classnumber] [nvarchar](25) NULL,
	[booknumber] [nvarchar](12) NULL,
	[classnumber_l1] [nvarchar](25) NULL,
	[booknumber_l1] [nvarchar](12) NULL,
	[classnumber_l2] [nvarchar](25) NULL,
	[booknumber_l2] [nvarchar](12) NULL,
	[transno] [int] NULL
)
