create proc Starter
as
begin
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


end

