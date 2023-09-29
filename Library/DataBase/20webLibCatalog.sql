--drop proc [dbo].[webLibCatalog]

alter proc [dbo].[webLibCatalog]
(
@Accession dbo.accession readonly
,@Bookcatalog dbo.bookcatalog readonly,
@BookAuthor dbo.BookAuthor readonly
)
as
begin
declare @IsSuccess bit,@Mesg varchar(100)
,@cn1 int,@cn2 int
set @IsSuccess=1
select @cn1=COUNT(*) from @Accession
--select @cn2=COUNT(*) from @Bookcatalog
--select @cn2= len(booktitle) from @accession
set @mesg=cast(@cn1 as varchar)+':fname1-'+cast((select top 1  firstname1 from @BookAuthor) as varchar)

select @IsSuccess IsSuccess,@Mesg Mesg



end
