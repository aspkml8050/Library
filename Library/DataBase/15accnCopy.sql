alter proc insert_Accession_ExpressCP  
@AccessionNo varchar(30),  
@AccessionNoCp varchar(30),  
@Copynnumber int,  
@Ctrl_no int,  
@BookPrice decimal(20,2),  
@BookNumberCP nvarchar(30),  
@Item_type nvarchar(25),  
@AccnCpDate date,  
@BiilNo nvarchar(30),  
@BillDate date,  
@DeptId int,  
@ItemCategory int,  
@Vendor nvarchar(200),  
@LocId int,  
@userid varchar(50) ,
@IpAddress varchar(100),
@AppName varchar(50),
@TransNo int
as  
begin  
	declare @run bit
	set @run=1
    declare @ers nvarchar(100)  
	if not exists (select * from bookaccessionmaster where ctrl_no=@Ctrl_no)  
	begin  
	    set @run=0
		set @ers='Control No is not valid for Accn:'+@AccessionNo  
		RAISERROR ( @ers,16,1)  
	end  
	if exists (select * from bookaccessionmaster where accessionnumber=@AccessionNoCp)  
	begin  
	    set @run=0
			set @ers='Copy Accession Already exists:'+@AccessionNoCp  
		RAISERROR ( @ers,16,1)  
	end  
	if not exists (select * from bookaccessionmaster where accessionnumber=@AccessionNo)  
	begin  
	    set @run=0
		set @ers='Accession deos not exists:'+@AccessionNo  
		RAISERROR ( @ers,16,1)  
	end  
	if exists (select * from bookaccessionmaster where ctrl_no=@Ctrl_no and Copynumber=@Copynnumber)  
	begin  
	    set @run=0
		set @ers='Copy number for Accession Already exists:new-'+@AccessionNoCp  +';from-'+@AccessionNo+';ctrlno-'+CAST(@Ctrl_no as varchar)+';copy-'+cast(@Copynnumber as varchar)
		RAISERROR ( @ers,16,1)  
	end  
   if @run=0
   begin
     return
   end 
  declare @ordn varchar(10),@indentno varchar(40),@form varchar(50),@accid int,@btitle nvarchar(400),@srno int,@rel varchar(10),  
  @srnoold int,@status varchar(5),@releasedate date,@issstat varchar(5),@loadingdate date,@checkstat varchar(5),  
  @edyr varchar(5),@spprice decimal(20,2),@pubyear varchar(5),@catdate date,@orgprice decimal(20,2),@orgcur nvarchar(50),  
  @prgid int,@dsrno int,@deptNm nvarchar(300),@catnam nvarchar(100)  
  select @ordn=a.ordernumber,@indentno=a.indentnumber,@form=a.form,@btitle=a.booktitle,@srno=a.srno,@rel=a.released,  
  @srnoold=a.srNoOld,@status=a.Status,@releasedate=a.ReleaseDate,@issstat=a.IssueStatus,@loadingdate=a.LoadingDate,@checkstat=a.CheckStatus,  
  @prgid=a.program_id,@dsrno=a.DSrno,@deptNm=a.DeptName,@catnam=a.ItemCategory,@edyr=a.editionyear,@spprice=a.specialprice,@pubyear=a.pubYear,  
  @orgprice=a.OriginalPrice,@orgcur=a.OriginalCurrency  
  from bookaccessionmaster a where a.accessionnumber=@AccessionNo  
  set @issstat='Y'  
  set @checkstat='A'  
  select @accid=  max(a.accessionid)+1  from bookaccessionmaster a  
  insert into bookaccessionmaster   
  (accessionnumber,ordernumber,indentnumber,form,accessionid,accessioneddate,booktitle,srno,released,bookprice,  
  srNoOld,Status,ReleaseDate,IssueStatus,LoadingDate,CheckStatus,ctrl_no,editionyear,Copynumber,specialprice,pubYear,  
  biilNo,billDate,catalogdate,Item_type,OriginalPrice,OriginalCurrency,userid,vendor_source,program_id,DeptCode,  
  DSrno,DeptName,ItemCategoryCode,ItemCategory,Loc_id,RfidId,BookNumber,IpAddress,TransNo,AppName)  
  values  
  (@AccessionNoCp,@ordn,@indentno,@form,@accid,@AccnCpDate,@btitle,@srno,@rel,@BookPrice,@srnoold,@status,  
  @releasedate,@issstat,@loadingdate,@checkstat,@Ctrl_no,@edyr,@Copynnumber,@spprice,@pubyear,  
  @BiilNo,@BillDate,@AccnCpDate,@Item_type,@orgprice,@orgcur,@userid,@Vendor,@prgid,@DeptId,  
  @dsrno,@deptNm,@ItemCategory,@catnam,@LocId,'',@BookNumberCP,@IpAddress,@TransNo,@AppName)  
  
  
  
  
end  
  
  
--select top 10 * from bookaccessionmaster  