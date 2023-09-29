--drop proc [dbo].[usp_InsertOrUpdateBookAccession]

create PROCEDURE [dbo].[usp_InsertOrUpdateBookAccession]
@BookAccessionMaster  dbo.accession readonly
AS
BEGIN
    MERGE INTO [dbo].[BookAccessionMaster] AS target
    USING @BookAccessionMaster AS source
    ON (target.[AccessionNumber] = source.[AccessionNumber])
    WHEN MATCHED THEN
        UPDATE SET
--            target.[BookID] = source.[BookID],
	target.[ordernumber] =source.[ordernumber],
	target.[indentnumber]  =source.[indentnumber],
	target.[form]  =source.[form], 
	target.[accessionid] =source.[accessionid],
	target.[accessioneddate] =source.[accessioneddate],
	target.[booktitle] =source.[booktitle],
	target.[srno] =source.[srno],
	target.[released]=source.[released],
	target.[bookprice]=source.[bookprice],
	target.[srNoOld] =source.[srNoOld],
	target.[Status]=source.[Status], 
	target.[ReleaseDate] =source.[ReleaseDate] ,
	target.[IssueStatus]=source.[IssueStatus], 
	target.[LoadingDate]=source.[LoadingDate], 
	target.[CheckStatus]=source.[CheckStatus] ,
	target.[ctrl_no]=source.[ctrl_no], 
	target.[editionyear]=source.[editionyear],
	target.[Copynumber]=source.[Copynumber], 
	target.[specialprice]=source.[specialprice], 
	target.[pubYear] =source.[pubYear] ,
	target.[biilNo] =source.[biilNo] ,
	target.[billDate]=source.[billDate],
	target.[catalogdate]=source.[catalogdate],
	target.[Item_type] =source.[Item_type] ,
	target.[OriginalPrice] =source.[OriginalPrice] ,
	target.[OriginalCurrency] =source.[OriginalCurrency] ,
	target.[userid]=source.[userid], 
	target.[vendor_source] =source.[vendor_source] ,
	target.[program_id] =source.[program_id] ,
	target.[DeptCode]=source.[DeptCode], 
	target.[DSrno]=source.[DSrno], 
	target.[DeptName]=source.[DeptName], 
	target.[ItemCategoryCode] =source.[ItemCategoryCode] ,
	target.[ItemCategory]=source.[ItemCategory], 
	target.[Loc_id] =source.[Loc_id] ,
	target.[RfidId] =source.[RfidId] ,
	target.[BookNumber]=source.[BookNumber],
	target.[SetOFbooks]=source.[SetOFbooks],
	target.[SearchText]=source.[SearchText],
	target.[IpAddress] =source.[IpAddress] ,
	target.[TransNo] =source.[TransNo] ,
	target.[AppName] =source.[AppName] 
           
    WHEN NOT MATCHED BY TARGET THEN
        INSERT (
            [AccessionNumber],	[ordernumber],	[indentnumber],	[form] ,	[accessionid],
	[accessioneddate],	[booktitle] ,	[srno] ,	[released],	[bookprice],
	[srNoOld] ,	[Status] ,	[ReleaseDate] ,	[IssueStatus] ,	[LoadingDate] ,
	[CheckStatus] ,	[ctrl_no] ,	[editionyear],	[Copynumber] ,	[specialprice] ,	[pubYear] ,
	[biilNo] ,	[billDate],	[catalogdate],	[Item_type] ,	[OriginalPrice] ,
	[OriginalCurrency] ,	[userid] ,	[vendor_source] ,		[program_id] ,	[DeptCode] ,	[DSrno] ,
	[DeptName] ,	[ItemCategoryCode] ,	[ItemCategory] ,	[Loc_id] ,	[RfidId] ,
	[BookNumber],	[SetOFbooks],	[SearchText],	[IpAddress] ,	[TransNo] ,
	[AppName]         )
        VALUES (
		            source.[AccessionNumber],	source.[ordernumber],	source.[indentnumber],	source.[form] ,	source.[accessionid],
	source.[accessioneddate],	source.[booktitle] ,	source.[srno] ,	source.[released],	source.[bookprice],
	source.[srNoOld] ,	source.[Status] ,	source.[ReleaseDate] ,	source.[IssueStatus] ,	source.[LoadingDate] ,
	source.[CheckStatus] ,	source.[ctrl_no] ,	source.[editionyear],	source.[Copynumber] ,	source.[specialprice] ,	source.[pubYear] ,
	source.[biilNo] ,	source.[billDate],	source.[catalogdate],	source.[Item_type] ,	source.[OriginalPrice] ,
	source.[OriginalCurrency] ,	source.[userid] ,	source.[vendor_source] ,		source.[program_id] ,	source.[DeptCode] ,	source.[DSrno] ,
	source.[DeptName] ,	source.[ItemCategoryCode] ,	source.[ItemCategory] ,	source.[Loc_id] ,	source.[RfidId] ,
	source.[BookNumber],	source.[SetOFbooks],	source.[SearchText],	source.[IpAddress] ,	source.[TransNo] ,
	source.[AppName]
        );
END