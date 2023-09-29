CREATE TYPE [dbo].[BookAuthor] AS TABLE (
    [ctrl_no]              BIGINT          NOT NULL,
    [firstname1]           NVARCHAR (100)  NULL,
    [middlename1]          NVARCHAR (100)  NULL,
    [lastname1]            NVARCHAR (100)  NULL,
    [firstname2]           NVARCHAR (100)  NULL,
    [middlename2]          NVARCHAR (100)  NULL,
    [lastname2]            NVARCHAR (100)  NULL,
    [firstname3]           NVARCHAR (100)  NULL,
    [middlename3]          NVARCHAR (100)  NULL,
    [lastname3]            NVARCHAR (100)  NULL,
    [PersonalName]         NVARCHAR (1000) NULL,
    [CorporateName]        NVARCHAR (1000) NULL,
    [DateAssociated]       NVARCHAR (30)   NULL
    )
