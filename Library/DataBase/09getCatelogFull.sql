  create proc getCatelogFull   
  (  
  @Accn varchar(25)  null,  
  @Ctrl_no int null  
  )  
as  
 begin  
declare @IsSuccess bit,@Messg varchar(100)  
if @Accn =''
begin
 set @Accn=null
end 
if @Accn is null and @Ctrl_no is null  
begin  
  set @IsSuccess=0  
  set @Messg='Supply either of @Accn @Ctrl_no'  
  select @IsSuccess IsSuccess,@Messg Messg  
  return  
end  
if @Accn is not null  
begin  
--select b.name+',' from sys.tables a join sys.columns b on a.object_id=b.object_id and a.name='BookImage'  
  select accessionnumber,ordernumber,indentnumber,form,accessionid,accessioneddate,booktitle,  
srno,released,bookprice,srNoOld,Status,ReleaseDate,IssueStatus,LoadingDate,CheckStatus,ctrl_no,editionyear  
,Copynumber,specialprice,pubYear,biilNo,billDate,catalogdate,Item_type,OriginalPrice,OriginalCurrency,  
userid,vendor_source,program_id,DeptCode,DSrno,DeptName,ItemCategoryCode,ItemCategory,Loc_id,RfidId,  
BookNumber,SetOFbooks,SearchText,IpAddress,TransNo,AppName,VendorId ,(select .dbo.locdecode2(loc_id))  
from bookaccessionmaster where accessionnumber=@Accn  
  select ctrl_no,firstname1,middlename1,lastname1,firstname2,middlename2,lastname2,firstname3,middlename3,  
lastname3,PersonalName,DateAssociated,RelatorTermP,CorporateName,RelatorTermC,UniFormTitle,DateofWork,  
LanguageofWork,stmtofResponsibility,AddedPersonalName,TransNo from BookAuthor where   
ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
  
  select ctrl_no,catalogdate1,booktype,volumenumber,initpages,pages,parts,leaves,boundind,title,publishercode,edition,  
isbn,subject1,subject2,subject3,Booksize,LCCN,Volumepages,biblioPages,bookindex,illustration,variouspaging,maps,  
ETalEditor,ETalCompiler,ETalIllus,ETalTrans,ETalAuthor,accmaterialhistory,MaterialDesignation,issn,Volume,  
dept,language_id,part,eBookURL,FixedData,cat_Source,Identifier,firstname,percity,perstate,percountry,peraddress,  
departmentname,Btype,language_name,PublisherNo,PubSource,SysCtrlNo,NLMCN,GeoArea,PhyExtent,PhyOther,pubDate,BookCost,  
latestTransDate,ItemCategory,OriginalCurrency,OriginalPrice,SearchText,TransNo,Control008   
from BookCatalog where ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
  
  select ctrl_no,Subtitle,Paralleltype,ConfName,ConfYear,BNNote,CNNote,GNNotes,VNNotes,SNNotes,ANNotes,Course,  
AdFname1,AdMname1,AdLname1,AdFname2,AdMname2,AdLname2,AdFname3,AdMname3,AdLName3,Abstract,Program_name,  
ConfPlace,loccallno,transno   
from BookConference where ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
  
  select ctrl_no,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,  
editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,  
CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,  
illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,  
TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3  
from BookRelators where   
 ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
   
 select ctrl_no,SeriesName,seriesNo,seriesPart,etal,Svolume,af1,am1,al1,af2,am2,al2,af3,am3,al3,SSeriesName,  
SseriesNo,SseriesPart,Setal,SSvolume,Saf1,sam1,Sal1,Saf2,Sam2,Sal2,Saf3,Sam3,Sal3,SeriesParallelTitle,  
SSeriesParallelTitle,SubSeriesName,SubseriesNo,SubSeriesPart,SubEtal,SubSvolume,Subaf1,Subam1,Subal1,  
Subaf2,Subam2,Subal2,Subaf3,Subam3,Subal3,SubSeriesParallelTitle,ISSNMain,ISSNSub,ISSNSecond   
from BookSeries where ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
  
  select ctrl_no,classnumber,booknumber,classnumber_l1,booknumber_l1,classnumber_l2,booknumber_l2,transno   
  from CatalogData where ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
    
  select ctrl_no,CoverPage from BookImage where ctrl_no in ( select ctrl_no from bookaccessionmaster where accessionnumber=@Accn)  
  set @IsSuccess=1  
  set @Messg=''  
  select @IsSuccess IsSuccess,@Messg Messg  
  return  
end  
if @Ctrl_no is not null  
begin  
    select accessionnumber,ordernumber,indentnumber,form,accessionid,accessioneddate,booktitle,  
srno,released,bookprice,srNoOld,Status,ReleaseDate,IssueStatus,LoadingDate,CheckStatus,ctrl_no,editionyear  
,Copynumber,specialprice,pubYear,biilNo,billDate,catalogdate,Item_type,OriginalPrice,OriginalCurrency,  
userid,vendor_source,program_id,DeptCode,DSrno,DeptName,ItemCategoryCode,ItemCategory,Loc_id,RfidId,  
BookNumber,SetOFbooks,SearchText,IpAddress,TransNo,AppName,VendorId from bookaccessionmaster  
 where ctrl_no=@Ctrl_no  
  
    select ctrl_no,firstname1,middlename1,lastname1,firstname2,middlename2,lastname2,firstname3,middlename3,  
lastname3,PersonalName,DateAssociated,RelatorTermP,CorporateName,RelatorTermC,UniFormTitle,DateofWork,  
LanguageofWork,stmtofResponsibility,AddedPersonalName,TransNo from BookAuthor   
where ctrl_no =@Ctrl_no  
  
  select ctrl_no,catalogdate1,booktype,volumenumber,initpages,pages,parts,leaves,boundind,title,publishercode,edition,  
isbn,subject1,subject2,subject3,Booksize,LCCN,Volumepages,biblioPages,bookindex,illustration,variouspaging,maps,  
ETalEditor,ETalCompiler,ETalIllus,ETalTrans,ETalAuthor,accmaterialhistory,MaterialDesignation,issn,Volume,  
dept,language_id,part,eBookURL,FixedData,cat_Source,Identifier,firstname,percity,perstate,percountry,peraddress,  
departmentname,Btype,language_name,PublisherNo,PubSource,SysCtrlNo,NLMCN,GeoArea,PhyExtent,PhyOther,pubDate,BookCost,  
latestTransDate,ItemCategory,OriginalCurrency,OriginalPrice,SearchText,TransNo,Control008   
from BookCatalog where   ctrl_no =@Ctrl_no  
  
  select ctrl_no,Subtitle,Paralleltype,ConfName,ConfYear,BNNote,CNNote,GNNotes,VNNotes,SNNotes,ANNotes,Course,  
AdFname1,AdMname1,AdLname1,AdFname2,AdMname2,AdLname2,AdFname3,AdMname3,AdLName3,Abstract,Program_name,  
ConfPlace,loccallno,transno   
from BookConference where ctrl_no =@Ctrl_no  
  
  select ctrl_no,editorFname1,editorMname1,editorLname1,editorFname2,editorMname2,editorLname2,editorFname3,  
editorMname3,editorLname3,CompilerFname1,CompilerMname1,CompilerLname1,CompilerFname2,CompilerMname2,CompilerLname2,  
CompilerFname3,CompilerMname3,CompilerLname3,illusFname1,illusMname1,illusLname1,illusFname2,illusMname2,  
illusrLname2,illusFname3,illusMname3,illusLname3,TranslatorFname1,TranslatorMname11,TranslatorLname1,  
TranslatorFname2,TranslatorMname2,TranslatorLname2,TranslatorFname3,TranslatorMname3,TranslatorLname3  
from BookRelators where ctrl_no =@Ctrl_no  
  select ctrl_no,SeriesName,seriesNo,seriesPart,etal,Svolume,af1,am1,al1,af2,am2,al2,af3,am3,al3,SSeriesName,  
SseriesNo,SseriesPart,Setal,SSvolume,Saf1,sam1,Sal1,Saf2,Sam2,Sal2,Saf3,Sam3,Sal3,SeriesParallelTitle,  
SSeriesParallelTitle,SubSeriesName,SubseriesNo,SubSeriesPart,SubEtal,SubSvolume,Subaf1,Subam1,Subal1,  
Subaf2,Subam2,Subal2,Subaf3,Subam3,Subal3,SubSeriesParallelTitle,ISSNMain,ISSNSub,ISSNSecond   
from BookSeries where ctrl_no =@Ctrl_no  
  
    select ctrl_no,classnumber,booknumber,classnumber_l1,booknumber_l1,classnumber_l2,booknumber_l2,transno   
  from CatalogData where ctrl_no =@Ctrl_no  
  select * from BookImage where ctrl_no =@Ctrl_no  
  set @IsSuccess=1  
  set @Messg=''  
  select @IsSuccess IsSuccess,@Messg Messg  
  return  
end  
  
end  