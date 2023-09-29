/****** Some object to create/update if not in database other than import query   ********/

if not exists (
select  * from sys.tables a join sys.columns b on a.object_id=b.object_id
and a.name='bookaccessionmaster' and b.name='VendorId')
begin
  alter table bookaccessionmaster add VendorId int;
  create table #vend
  (
  vid int,
  vendor_source nvarchar(200),
  vendorname nvarchar(200),
  city nvarchar(60)
  )
  insert into #vend
  select ROW_NUMBER() over(order by left(trim(vendor_source), len(trim(vendor_source)) - charindex(',', reverse(trim(vendor_source)) + ',')))
  , * from  
   (select distinct 
    vendor_source, 
    left(trim(vendor_source), len(trim(vendor_source)) - charindex(',', reverse(trim(vendor_source)) + ',')) vend,
    right(trim(vendor_source), charindex(',', reverse(trim(vendor_source)) + ',') - 1) city
    from bookaccessionmaster where len(vendor_source) - charindex(',', reverse(vendor_source) + ',')>0
	) vend
    update #vend set vendorname=trim(vendorname),city=trim(city)
	update #vend set vid=b.vendorid  from vendormaster b join #vend on #vend.vendorname=b.vendorname
	join AddressTable c on #vend.city=c.percity
    exec ('
    update bookaccessionmaster set VendorId= b.vid 
    from #vend b where    bookaccessionmaster.vendor_source=b.vendor_source')

    drop table  #vend
end
if  exists (
select * /*column_name, data_type, character_maximum_length    */
  from information_schema.columns  
  where table_name = 'vendormaster' and COLUMN_NAME='vendorname' and character_maximum_length <200 )
begin
   alter table vendormaster alter column vendorname nvarchar(200)
end
if not exists (select * from sys.tables where name='Control008')
begin
  create table Control008
  (
  ControlNo int primary key,
  InUse bit
  )
--delete from Control008
  declare @nos int,@start int
  set @start=1000002406
  set @nos = @start +100000
  while @start<@nos
  begin
    insert into Control008 values
   (@start,0)
   set @start +=1
  end
  alter table bookcatalog add Control008 varchar(20)
  create index uControl008 on Control008(ControlNo)
  create index uControl008InUse on Control008(inuse)
  create index uControl008Tot on Control008(ControlNo,inuse)
end

if not exists (
select  * from sys.tables a join sys.columns b on a.object_id=b.object_id
and a.name='BookCatalog' and b.name='Control008')
begin
  alter table BookCatalog add Control008 varchar(20)
end

alter table indentmaster alter column itemno int

alter table ordermaster alter column itemnumber int

if not exists (
select * from sys.indexes where name='addRess'
)
begin
  create index addRess on addresstable (addid,addrelation)
end

/*

if exists (
select * from sys.views where name='vwebItems'
)
begin
  exec ('delete view vwebItems')
end
--following has errors :
exec ('
create view vwebItems As
select  
accessionnumber,form,
f.title,f.OriginalPrice,f.OriginalCurrency,f.publishercode,f.PubSource,f.catalogdate1,
f.pages,f.booktype,f.edition,f.isbn,f.issn,
i.Language_Name,f.part,f.eBookURL,f.control008,
f.subject1,f.subject2,f.subject3,
h.firstname Publisher,j.percity PublCity,
g.Item_Type,
k.classnumber,k.booknumber,
l.Subtitle,

m.firstname1, m.middlename1,m.lastname1,
m.firstname2,m.middlename2,m.lastname2 
, m.firstname3,m.middlename3,m.lastname3
,m.PersonalName,m.DateAssociated,m.DateofWork,
released,
bookprice,Status,IssueStatus,LoadingDate,CheckStatus,editionyear,Copynumber,pubYear,
biilNo,billDate,catalogdate,program_id,DeptCode,
ItemCategoryCode,/*Loc_id, skipped */a.BookNumber AccnBookNumber,a.VendorId
,b.ItemStatus
,c.Category_LoadingStatus Category
,d.vendorname Vendor, e.percity VendorCity
from bookaccessionmaster a 
join ItemStatusMaster b on a.Status=b.ItemStatusID
join CategoryLoadingStatus c on a.ItemCategoryCode=c.Id
join vendormaster d on a.VendorId=d.vendorid
join AddressTable e on d.vendorcode=e.addid and e.addrelation=''''+ vendor+''''
join BookCatalog f on a.ctrl_no=f.ctrl_no
join Item_Type g on f.booktype=g.Id
join publishermaster h on f.publishercode=h.PublisherId
join Translation_Language i on f.language_id=i.Language_Id
join AddressTable j on h.PublisherId=j.addid and j.addrelation=''+publisher+''
join CatalogData k on f.ctrl_no=k.ctrl_no
join BookConference l on f.ctrl_no=l.ctrl_no
join BookAuthor m on a.ctrl_no=m.ctrl_no
')

*/
end

